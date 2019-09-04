using System.Globalization;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using NoteStore.Contract.V1;
using NoteStore.Contract.V1.Request;
using NoteStore.Contract.V1.Response;
using NoteStore.Model;
using NoteStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using NoteStore.Extensions;

namespace NoteStore.Controllers.V1
{
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
          this._noteService=noteService;
        }

        [HttpGet(ApiRoutes.Notes.GetAll)]
        public IActionResult GetAll()
        {
            return Ok(this._noteService.GetNotes(HttpContext.GetUserId()));
        }

        [HttpPost(ApiRoutes.Notes.Create)]
        public IActionResult Create([FromBody] CreateNoteRequest noteRequest){
            if(noteRequest==null)
                return BadRequest();

            var note = new Note {Id=noteRequest.Id, Tag=noteRequest.Tag, Title=noteRequest.Title, Post=noteRequest.Post, UserId=HttpContext.GetUserId()};

            if(string.IsNullOrEmpty(noteRequest.Id)){
                noteRequest.Id=Guid.NewGuid().ToString();
            }
            //this._noteService.GetNotes().Add(note);
            this._noteService.Create(note);

            var baseUrl=HttpContext.Request.Scheme +"://"+ HttpContext.Request.Host.ToUriComponent();
            var locationUri=baseUrl +"/"+ ApiRoutes.Notes.Get.Replace("{noteId}",noteRequest.Id);

            var response=new CreateNoteResponse{Id=note.Id, Tag=note.Tag, Title=note.Title, Post=note.Post};

            return Created(locationUri,response);
        }

        [HttpGet(ApiRoutes.Notes.Get)]
        public IActionResult Get([FromRoute] string noteId){
            var note=this._noteService.GetNoteById(noteId);
            
            if(note==null)
            {
                return NotFound();
            }
            return Ok(note);
        }

   

        [HttpPut(ApiRoutes.Notes.Update)]
        public IActionResult Update([FromRoute] string noteId, [FromBody] UpdatePostRequest request){
            if(request==null)
                return BadRequest();
            var userOwnsPost = _noteService.UserOwnsNoteAsync(noteId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "Post not found" });
            }


            //var note=new Note{Id=noteId,Tag=request.Tag,Title=request.Title,Post=request.Post};
            var note = _noteService.GetNoteById(noteId);
            note.Tag = request.Tag;
            note.Title = request.Title;
            note.Post = request.Post;

            var updateSuccess=_noteService.UpdateNote(note);
            
            if(!updateSuccess)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpDelete(ApiRoutes.Notes.Delete)]
        public IActionResult Delete([FromRoute] string noteId){
            if(string.IsNullOrEmpty(noteId))
                return BadRequest();

            var userOwnsPost = _noteService.UserOwnsNoteAsync(noteId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(new { error = "Post not found" });
            }


            var deleteSuccess=_noteService.DeleteNote(noteId);
            if(!deleteSuccess)
                return NotFound();

            return NoContent();

            
        }
    }

}
