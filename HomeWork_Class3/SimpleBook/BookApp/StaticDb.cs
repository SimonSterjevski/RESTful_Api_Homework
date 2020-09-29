using BookApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book()
            {
                Author = "avtor1",
                Title = "kniga1"
            },
            new Book()
            {
                Author = "avtor1",
                Title = "kniga2"
            },
            new Book()
            {
                Author = "avtor3",
                Title = "kniga3"
            }
        };
    }
}
