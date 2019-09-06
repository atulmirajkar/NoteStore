namespace NoteStore.Options
{
    public interface INoteStoreSettings
    {
          string ConnectionString{get;set;}

         string DatabaseName{get;set;}

         string CollectionName{get;set;}

        string RefreshTokenCollectionName { get; set; }
    }
}