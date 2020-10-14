using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SEDC.NotesApp.Domain;
using System.Security.Cryptography.X509Certificates;

namespace SEDC.NotesApp.DataAccess.Implementations
{
    public class NoteRepository :INoteRepository
    {
        private NotesAppDbContext _notesAppDbContext;

        public NoteRepository(NotesAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
        }
        public List<Note> GetAll()
        {
            return _notesAppDbContext
                .Notes
                .Include(x => x.User) //join with table users
                .Include(x => x.Tag)
                .ToList();
        }

        public Note GetById(int id)
        {
            return _notesAppDbContext
                .Notes
                .Include(x => x.User)
                .Include(x => x.Tag)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Note entity)
        {
            _notesAppDbContext.Notes.Add(entity);
            _notesAppDbContext.SaveChanges(); //request to DB
        }

        public void Delete(Note entity)
        {
            _notesAppDbContext.Notes.Remove(entity);
            _notesAppDbContext.SaveChanges();
        }

        public void Update(Note entity)
        {
            _notesAppDbContext.Notes.Update(entity);
            _notesAppDbContext.SaveChanges();
        }

        public Tag FindMostUsedTag()
        {
            List<Tag> tags = _notesAppDbContext.Notes.Select(x => x.Tag).ToList();
            return tags.FirstOrDefault(x => x.Id == tags.GroupBy(y => y.Id).OrderByDescending(y => y.Count()).FirstOrDefault().Key);
        }

    }
}
