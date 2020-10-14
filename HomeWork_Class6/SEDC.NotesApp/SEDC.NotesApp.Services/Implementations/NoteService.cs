using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Mappers;
using SEDC.NotesApp.Models;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Exceptions;

namespace SEDC.NotesApp.Services.Implementations
{
    public class NoteService : INoteService
    {
        private INoteRepository _noteRepository;
        private IRepository<User> _userRepository;
        private IRepository<Tag> _tagRepository;
        public NoteService(INoteRepository noteRepository, IRepository<User> userRepository, IRepository<Tag> tagRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
            _tagRepository = tagRepository;
        }

        public List<NoteModel> GetAllNotes()
        {
            List<Note> notesDb = _noteRepository.GetAll();
            List<NoteModel> noteModels = new List<NoteModel>();
            foreach (Note note in notesDb)
            {
                noteModels.Add(note.ToNoteModel());
            }

            return noteModels;
        }

        public NoteModel GetNoteById(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                //log
                throw new NotFoundException($"Note with id {id} was not found!");
                //throw new NotFoundException(id);
            }

            return noteDb.ToNoteModel();
        }

        public void AddNote(NoteModel noteModel)
        {
            User userDb = ValidateNoteModel(noteModel);
            Tag tag = _tagRepository.GetById(noteModel.TagId);
            Note noteForDb = noteModel.ToNote();
            noteForDb.User = userDb;
            noteForDb.Tag = tag;
            _noteRepository.Add(noteForDb);
        }

        public void UpdateNote(NoteModel noteModel)
        {
            Note noteDb = _noteRepository.GetById(noteModel.Id);
            if (noteDb == null)
            {
                throw new NotFoundException(noteModel.Id);
            }
            User userDb = ValidateNoteModel(noteModel);
            Tag tag = _tagRepository.GetById(noteModel.TagId);
            noteDb.Text = noteModel.Text;
            noteDb.Color = noteModel.Color;
            noteDb.TagId = noteModel.TagId;
            noteDb.Tag = tag;
            noteDb.UserId = noteModel.UserId;
            noteDb.User = userDb;
            _noteRepository.Update(noteDb);
        }

        public void DeleteNote(int id)
        {
            Note noteDb = _noteRepository.GetById(id);
            if (noteDb == null)
            {
                throw new NotFoundException(id);
            }
            _noteRepository.Delete(noteDb);
        }



        public string GetMostUsedTag()
        {
            Tag tag = _noteRepository.FindMostUsedTag();
            if (tag == null)
            {
                throw new Exception("There are no tags yet!");
            }
            return tag.Type;
        }



        private User ValidateNoteModel(NoteModel noteModel)
        {
            User userDb = _userRepository.GetById(noteModel.UserId);
            if (userDb == null)
            {
                //log
                throw new NoteException($"The user with id {noteModel.UserId} was not found!");
            }

            //if (noteModel.Id != 0)
            //{
            //    throw new NoteException("Id must not be set!");
            //}
            if (string.IsNullOrEmpty(noteModel.Text))
            {
                throw new NoteException("The property Text for note is required");
            }
            if (noteModel.Text.Length > 100)
            {
                throw new NoteException("The property Text can not contain more than 100 characters");
            }
            if (!string.IsNullOrEmpty(noteModel.Color) && noteModel.Color.Length > 30)
            {
                throw new NoteException("The property Color can not contain more than 30 characters");
            }
            if (!_tagRepository.GetAll().Select(x => x.Id).Contains(noteModel.TagId))
            {
                throw new NotFoundException($"Tag with id {noteModel.TagId} was not found!");
            }

            return userDb;
        }
    }
}
