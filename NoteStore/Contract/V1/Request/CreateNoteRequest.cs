namespace NoteStore.Contract.V1.Request{

    public class CreateNoteRequest
    {
        public CreateNoteRequest()
        {
        }

        public string Id { get; set; }

        public string Tag{get; set;}

        public string Title{get;set;}

        public string Post{get;set;}
    }

}