using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Models
{
    public class UserModel
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public List<NoteModel> Notes { get; set; }
    }
}
