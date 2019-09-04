using System.Collections.Generic;
using System.Threading.Tasks;
using NoteStore.Model;
namespace NoteStore.Services
{
    public interface INoteService
    {
         List<Note> GetNotes(string userId);
         Note GetNoteById(string Id);

         bool UpdateNote(Note noteToUpdate);

         bool DeleteNote(string noteID);

         Note Create(Note note);
         bool UserOwnsNoteAsync(string noteId, string userId);
    }
}