using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace SEDC.NotesApp.Mappers
{
    public static class UserMapper
    {
        public static User ToUser(this UserModel userModel)
        {
            return new User
            {
                Id = userModel.Id,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                UserName = userModel.UserName,
                Address = userModel.Address,
                Age = userModel.Age
            };
    }

        public static UserModel ToUserModel(this User user)
        {
            return new UserModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FullName = $"{user.FirstName} {user.LastName}",
                UserName = user.UserName,
                Address = user.Address,
                Age = user.Age,
                Notes = user.Notes.Select(x => x.ToNoteModel()).ToList()
        };
        }
    }
}
