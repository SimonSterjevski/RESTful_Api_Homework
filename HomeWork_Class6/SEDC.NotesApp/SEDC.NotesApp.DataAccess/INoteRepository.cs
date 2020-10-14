using SEDC.NotesApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.DataAccess
{
    public interface INoteRepository
    {
        List<Note> GetAll();
        Note GetById(int id);
        void Add(Note entity);
        void Delete(Note entity);
        void Update(Note entity);
        Tag FindMostUsedTag();
    }
}
