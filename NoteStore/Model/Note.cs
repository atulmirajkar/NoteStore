using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteStore.Model
{
    public class Note
    {
        public Guid UserId{get; set;}

        public string Id { get; set; }

        public string Tag{get; set;}

        public string Title{get;set;}

        public string Post{get;set;}

        //todo add timestamp
    }
}
