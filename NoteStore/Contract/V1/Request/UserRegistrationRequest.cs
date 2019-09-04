using System.ComponentModel.DataAnnotations;

namespace NoteStore.Contract.V1.Request
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}