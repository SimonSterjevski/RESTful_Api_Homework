using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using UserApp.Models;

namespace UserApp
{
    public static class StaticDb
    {
        public static int IdUser = 2;
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Tom",
                LastName = "Cat"
            },
            new User
            {
                Id = 2,
                FirstName = "Jerry",
                LastName = "Mouse"
            }
        };

        public static List<string> UsersNames = new List<string>
        {
            "Tom Cat",
            "Jerry Mouse"
        };
       
    }
}
