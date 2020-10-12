using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Models;
using SEDC.NotesApp.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetAllUsers();
        UserModel GetUserById(int id);

        void AddUser(UserModel userModel);
        void DeleteUser(int id);
        void UpdateUser(UserModel userModel);
        string GetMostUsedTag();
    }
}
