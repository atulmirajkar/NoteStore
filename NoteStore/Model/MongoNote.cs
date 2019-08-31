using MongoDB.Bson.Serialization.Attributes;
namespace NoteStore.Model
{
    public class MongoNote
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string UserId{get; set;}
        public string Id { get; set; }

        public string Tag{get; set;}

        public string Title{get;set;}

        public string Post{get;set;}

        //todo add timestamp
    
    }
}