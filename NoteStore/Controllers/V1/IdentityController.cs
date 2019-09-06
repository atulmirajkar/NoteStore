using Microsoft.AspNetCore.Mvc;
using NoteStore.Contract.V1;
using NoteStore.Contract.V1.Request;
using NoteStore.Contract.V1.Response;
using NoteStore.Controllers.V1.Request;
using NoteStore.Services.V1;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteStore.Controllers.V1
{
    public class IdentityController: Controller
    {
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService=identityService;
        }
        
        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach(var errorEntry in ModelState.Values)
                {
                    foreach(var singleError in errorEntry.Errors)
                    {
                        errors.Add(singleError.ErrorMessage);
                    }
                }
                return BadRequest(new AuthFailedResponse
                {
                    Errors = errors
                });
            }

            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors=authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse{
                Token= authResponse.Token,
                RefreshToken=authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }


        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }
            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}