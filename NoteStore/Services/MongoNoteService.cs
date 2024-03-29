using System.Threading.Tasks.Dataflow;
using System.Collections.Generic;
using NoteStore.Model;
using MongoDB.Driver;
using NoteStore.Options;
using System.Threading.Tasks;
using MongoDB.Bson;
using System;

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
            if (mongoNote == null)
                return null;
            return new Note{
                Id = mongoNote.Id,
                Title=mongoNote.Title,
                Tag=mongoNote.Tag,
                Post=mongoNote.Post,
                UserId=mongoNote.UserId
            };
        }

        public List<Note> GetNotes(Guid userId)
        {
            //todo add where clause for user
           var filter = Builders<MongoNote>.Filter.Eq("UserId", userId);
           var mongoNoteList = _noteList.Find(filter).ToList();
           var noteList = new List<Note>();
           mongoNoteList.ForEach(x => noteList.Add(new Note{
                Id = x.Id,
                Title=x.Title,
                Tag=x.Tag,
                Post=x.Post,
                UserId=x.UserId
           }));
           return noteList;
        }

        public bool UpdateNote(Note noteToUpdate)
        {
            var mongoNote = new MongoNote{
                Id=noteToUpdate.Id,
                Title=noteToUpdate.Title,
                Tag=noteToUpdate.Tag,
                Post=noteToUpdate.Post,
                UserId=noteToUpdate.UserId
            };
            _noteList.ReplaceOne(note => note.Id == mongoNote.Id,mongoNote);
            return true;
        }

        public Note Create(Note note){
            var mongoNote=new MongoNote{
                 Id = note.Id,
                Title=note.Title,
                Tag=note.Tag,
                Post=note.Post,
                UserId=note.UserId
            };
            _noteList.InsertOne(mongoNote);
            return note;
        }

        public bool UserOwnsNoteAsync(string noteId, Guid userId)
        {
            var mongoNote = _noteList.Find(note => note.Id == noteId).FirstOrDefault();
            if (mongoNote == null)
            {
                return false;
            }

            if(mongoNote.UserId != userId)
            {
                return false;
            }
            return true;
        }
    }
}