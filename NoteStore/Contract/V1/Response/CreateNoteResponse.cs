namespace NoteStore.Contract.V1.Response
{
    public class CreateNoteResponse
    {
        public string Id { get; set; }

        public string Tag{get; set;}

        public string Title{get;set;}

        public string Post{get;set;}
    }
}