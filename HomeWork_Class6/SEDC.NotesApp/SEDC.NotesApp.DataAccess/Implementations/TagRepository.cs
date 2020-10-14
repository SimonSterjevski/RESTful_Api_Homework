using SEDC.NotesApp.Domain;
using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SEDC.NotesApp.DataAccess.Implementations
{
    public class TagRepository : IRepository<Tag>
    {
        private NotesAppDbContext _notesAppDbContext;

        public TagRepository(NotesAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
        }
        public void Add(Tag entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Tag entity)
        {
            throw new NotImplementedException();
        }

        public List<Tag> GetAll()
        {
            return _notesAppDbContext.Tags.ToList();
        }

        public Tag GetById(int id)
        {
            return _notesAppDbContext.Tags.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Tag entity)
        {
            throw new NotImplementedException();
        }
    }
}
