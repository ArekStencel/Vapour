using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Vapour.Model;

namespace Vapour.State
{
    public interface IAuthenticator
    {
        User CurrentUser { get; }
        bool IsLoggedIn { get; }
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, string description, int roleId, decimal walletBalance);
        Task<bool> Login(string email, string password);
        void Logout();
    }
}