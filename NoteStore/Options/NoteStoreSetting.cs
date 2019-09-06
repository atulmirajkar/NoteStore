namespace NoteStore.Options
{
    public class NoteStoreSetting:INoteStoreSettings
    {
        public string ConnectionString{get;set;}

        public string DatabaseName{get;set;}

        public string CollectionName{get;set;}
        public string RefreshTokenCollectionName { get; set; }
    }
}