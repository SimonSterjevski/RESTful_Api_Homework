﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Models
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}
