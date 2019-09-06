using System.Linq;
using System;
using System.Collections.Generic;
using NoteStore.Model;
using System.Threading.Tasks;

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

        public Note Create(Note note)
        {
            _noteList.Add(note);
            return note;
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

        public List<Note> GetNotes(Guid userId)
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

        public bool UserOwnsNoteAsync(string noteId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}