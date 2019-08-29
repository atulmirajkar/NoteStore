using Microsoft.AspNetCore.Mvc;
using NoteStore.Contract.V1;
using NoteStore.Domain;
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
    }

}
