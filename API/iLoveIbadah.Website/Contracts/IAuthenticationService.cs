using iLoveIbadah.Website.Models;

namespace iLoveIbadah.Website.Contracts
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string email, string password);
        Task<bool> Register(RegisterVM registration);
        Task Logout();
    }
}
