using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.Implementations;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Mappers;
using SEDC.NotesApp.Models;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared;
using SEDC.NotesApp.Shared.Exceptions;
using SEDC.NotesApp.Validation;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;
        private IOptions<AppSettings> _options;

        public UserService(IRepository<User> userReposiory, IOptions<AppSettings> options)
        {
            _userRepository = userReposiory;
            _options = options;
        }
        public void AddUser(UserModel userModel)
        {
            UserValidation.ValidateUserModel(userModel);
            User user = userModel.ToUser();
            _userRepository.Add(user);
        }

        public void DeleteUser(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Bad Request!");
            }
            User user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new NotFoundException(id);
            }
            _userRepository.Delete(user);
        }

        public List<UserModel> GetAllUsers()
        {
            List<User> users = _userRepository.GetAll();
            return users.Select(x => x.ToUserModel()).ToList();
        }

        public UserModel GetUserById(int id)
        {
            if (id < 1)
            {
                throw new BadRequestException("Bad Request!");
            }
            User user = _userRepository.GetById(id);
            if (user == null)
            {
                throw new NotFoundException(id);
            }
            return user.ToUserModel();
        }

        public void UpdateUser(UserModel userModel)
        {
            User user = _userRepository.GetById(userModel.Id);
            if (user == null)
            {
                throw new NotFoundException(userModel.Id);
            }
            UserValidation.ValidateUserModel(userModel);
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.UserName = userModel.UserName;
            user.Address = userModel.Address;
            user.Age = userModel.Age;
            _userRepository.Update(user);
        }

        public void RegisterUser(RegisterModel registerModel)
        {
            if (string.IsNullOrEmpty(registerModel.UserName))
            {
                throw new UserException("Username is required field!");
            }
            if (registerModel.UserName.Length > 50)
            {
                throw new UserException("Username can not contain more than 50 characters!");
            }
            if (!ValidateUniqueUsername(registerModel.UserName))
            {
                throw new UserException("User with this username already exists!");
            }
           
            if (string.IsNullOrEmpty(registerModel.UserName))
            {
                throw new UserException("Password is required field");
            }
            if (registerModel.Password != registerModel.ConfirmedPassword)
            {
                throw new UserException("The passwords do not match");
            }
            if (!ValidatePassword(registerModel.Password))
            {
                throw new UserException("The password is weak!");
            }
            
            MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] md5Bytes = md5CryptoServiceProvider.ComputeHash(Encoding.ASCII.GetBytes(registerModel.Password));
            string hashedPassword = Encoding.ASCII.GetString(md5Bytes);

            User newUser = new User
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.UserName,
                Password = hashedPassword
            };
            _userRepository.Add(newUser);
        }

        public string LoginUser(LoginModel loginModel)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] md5data = md5.ComputeHash(Encoding.ASCII.GetBytes(loginModel.Password));
            string hashedPassword = Encoding.ASCII.GetString(md5data);

            User userDb = _userRepository.GetAll().FirstOrDefault(x => x.UserName == loginModel.UserName
                                                                       && x.Password == hashedPassword);
            if (userDb == null)
                throw new NotFoundException($"The user with username {loginModel.UserName} was not found! ");

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);
           
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                    {
                            new Claim(ClaimTypes.Name, userDb.UserName),
                            new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                            new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),
                    }
                )
            };
            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }


        private bool ValidateUniqueUsername(string username)
        {
            return _userRepository.GetAll().Any(x => x.UserName == username) == false;
        }

        private bool ValidatePassword(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            Match passwordMatch = passwordRegex.Match(password);
            return passwordMatch.Success;
        }





        //public string GetMostUsedTag()
        //{

        //    var tag = _userRepository.GetAll().SelectMany(x => x.Notes).GroupBy(x => x.Tag.Type).OrderByDescending(x => x.Count()).FirstOrDefault(); 
        //    return $"{tag.Key} - {tag.Count()}";
        //}


    }
}
