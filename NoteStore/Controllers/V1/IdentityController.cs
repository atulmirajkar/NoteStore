using Microsoft.AspNetCore.Mvc;
using NoteStore.Services.V1;

namespace NoteStore.Controllers.V1
{
    public class IdentityController: Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService=identityService;
        }
        
           
    }
}