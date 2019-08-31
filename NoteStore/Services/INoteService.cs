using System.Collections.Generic;
using NoteStore.Model;
namespace NoteStore.Services
{
    public interface INoteService
    {
         List<Note> GetNotes();
         Note GetNoteById(string Id);

         bool UpdateNote(Note noteToUpdate);

         bool DeleteNote(string noteID);

         Note Create(Note note);
    }
}