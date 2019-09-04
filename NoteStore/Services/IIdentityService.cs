using NoteStore.Model;
using System.Threading.Tasks;

namespace NoteStore.Services.V1
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}