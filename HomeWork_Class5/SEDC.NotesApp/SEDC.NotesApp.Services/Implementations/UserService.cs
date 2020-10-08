using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UserService: UserIService
    {
        private IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
