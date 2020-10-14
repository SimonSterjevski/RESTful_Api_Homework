using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.DataAccess.Implementations;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Mappers;
using SEDC.NotesApp.Models;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Enums;
using SEDC.NotesApp.Shared.Exceptions;
using SEDC.NotesApp.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userReposiory)
        {
            _userRepository = userReposiory;
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

        //public string GetMostUsedTag()
        //{

        //    var tag = _userRepository.GetAll().SelectMany(x => x.Notes).GroupBy(x => x.Tag.Type).OrderByDescending(x => x.Count()).FirstOrDefault(); 
        //    return $"{tag.Key} - {tag.Count()}";
        //}


    }
}
