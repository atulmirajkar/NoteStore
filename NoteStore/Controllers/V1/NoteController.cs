using Microsoft.AspNetCore.Mvc;
using NoteStore.Contract.V1;
using NoteStore.Contract.V1.Request;
using NoteStore.Contract.V1.Response;
using NoteStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteStore.Controllers.V1
{
    public class NoteController : Controller
    {
        private List<Note> _noteList;

        public NoteController()
        {
            _noteList = new List<Note>();
            _noteList.Add(new Note { Id = Guid.NewGuid().ToString() });
            _noteList.Add(new Note { Id = Guid.NewGuid().ToString() });
        }

        [HttpGet(ApiRoutes.Notes.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(_noteList);
        }

        [HttpPost(ApiRoutes.Notes.Create)]
        public IActionResult Create([FromBody] CreateNoteRequest noteRequest){
            var note = new Note {Id=noteRequest.Id, Tag=noteRequest.Tag, Title=noteRequest.Title, Post=noteRequest.Post};

            if(string.IsNullOrEmpty(noteRequest.Id)){
                noteRequest.Id=Guid.NewGuid().ToString();
            }
            _noteList.Add(note);

            var baseUrl=HttpContext.Request.Scheme +"://"+ HttpContext.Request.Host.ToUriComponent();
            var locationUri=baseUrl +"/"+ ApiRoutes.Notes.Get.Replace("{noteId}",noteRequest.Id);

            var response=new CreateNoteResponse{Id=note.Id, Tag=note.Tag, Title=note.Title, Post=note.Post};

            return Created(locationUri,response);
        }

        [HttpGet(ApiRoutes.Notes.Get)]
        public IActionResult Get([FromRoute] string noteId){
            var note=_noteList.SingleOrDefault(x => x.Id==noteId);
            
            return Ok(note);
        }
    }

}
