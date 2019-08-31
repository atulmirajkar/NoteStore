using System.Linq;
using System;
using System.Collections.Generic;
using NoteStore.Model;

namespace NoteStore.Services
{
    public class NoteService : INoteService
    {
        private List<Note> _noteList;
 
        public NoteService()
        {
            _noteList = new List<Note>();
            _noteList.Add(new Note { Id = Guid.NewGuid().ToString() });
            _noteList.Add(new Note { Id = Guid.NewGuid().ToString() });
        }

        public bool DeleteNote(string noteId)
        {
            var exists = GetNoteById(noteId)!=null;
            if(!exists)
                return false;

            var index = _noteList.FindIndex(x=>x.Id==noteId);
            _noteList.RemoveAt(index);

            return true;
        }

        public Note GetNoteById(string Id)
        {
            return _noteList.SingleOrDefault(x=>x.Id==Id);
        }

        public List<Note> GetNotes()
        {
            return _noteList;
        }

        public bool UpdateNote(Note noteToUpdate)
        {
            var exists = GetNoteById(noteToUpdate.Id)!=null;
            if(!exists)
                return false;

            var index = _noteList.FindIndex(x=>x.Id==noteToUpdate.Id);
            _noteList[index]=noteToUpdate;

            return true;
        }
    }
}