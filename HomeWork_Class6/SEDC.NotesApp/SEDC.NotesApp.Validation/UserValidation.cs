using SEDC.NotesApp.Models;
using SEDC.NotesApp.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Validation
{
    public static class UserValidation
    {
        public static void ValidateUserModel(UserModel userModel)
        {

            if (string.IsNullOrEmpty(userModel.UserName))
            {
                throw new NoteException("The property UserName for user is required");
            }
            if ((!string.IsNullOrEmpty(userModel.FirstName) && userModel.FirstName.Length > 50) || 
                (!string.IsNullOrEmpty(userModel.LastName) && userModel.LastName.Length > 50) || userModel.UserName.Length > 50)
            {
                throw new NoteException("The properties FirstName, LastName and UserName can not contain more than 50 characters");
            }
            if (!string.IsNullOrEmpty(userModel.Address) && userModel.Address.Length > 150)
            {
                throw new NoteException("The property Address can not contain more than 150 characters");
            }
        }
    }
}
