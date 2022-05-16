using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vapour.Model;
using Vapour.State;

namespace Vapour.Services
{
    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword, int roleId = 2);
        Task<User> Login(string email, string password);
    }
}
