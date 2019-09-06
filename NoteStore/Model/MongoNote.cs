using MongoDB.Bson.Serialization.Attributes;
using System;

namespace NoteStore.Model
{
    public class MongoNote
    {
      
        public Guid UserId{get; set;}

        [BsonId]
        public string Id { get; set; }

        public string Tag{get; set;}

        public string Title{get;set;}

        public string Post{get;set;}

        //todo add timestamp
    
    }
}