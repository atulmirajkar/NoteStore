using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;
using NoteStore.Model;
using MongoDB.Driver;
using NoteStore.Options;

namespace NoteStore.Services
{
    public class MongoNoteService : INoteService
    {
        private IMongoCollection<MongoNote> _noteList;
        
        public MongoNoteService(INoteStoreSettings noteStoreSettings)
        {
            var client = new MongoClient(noteStoreSettings.ConnectionString);
            var database = client.GetDatabase(noteStoreSettings.DatabaseName);
            _noteList=database.GetCollection<MongoNote>(noteStoreSettings.CollectionName);
        }
        public bool DeleteNote(string noteID)
        {
            _noteList.DeleteOne(note => note.Id==noteID);
            return true;
        }

        public  Note GetNoteById(string Id)
        {
            var mongoNote= _noteList.Find(note => note.Id==Id ).FirstOrDefault();
            return new Note{
                Id = mongoNote.Id,
                Title=mongoNote.Title,
                Tag=mongoNote.Tag,
                Post=mongoNote.Post
            };
        }

        public List<Note> GetNotes()
        {
           var mongoNoteList= _noteList.Find(note => true).ToList();
           var noteList = new List<Note>();
           mongoNoteList.ForEach(x => noteList.Add(new Note{
                Id = x.Id,
                Title=x.Title,
                Tag=x.Tag,
                Post=x.Post
           }));
           return noteList;
        }

        public bool UpdateNote(Note noteToUpdate)
        {
            var mongoNote = new MongoNote{
                Title=noteToUpdate.Title,
                Tag=noteToUpdate.Tag,
                Post=noteToUpdate.Post
            };
            _noteList.ReplaceOne(note => note.Id == mongoNote.Id,mongoNote);
            return true;
        }

        public Note Create(Note note){
            var mongoNote=new MongoNote{
                 Id = note.Id,
                Title=note.Title,
                Tag=note.Tag,
                Post=note.Post
            };
            _noteList.InsertOne(mongoNote);
            return note;
        }
    }
}