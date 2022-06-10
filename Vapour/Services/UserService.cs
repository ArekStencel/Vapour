using System.Linq;
using System.Threading.Tasks;
using Vapour.Model;
using Vapour.State;

namespace Vapour.Services
{
    public class UserService : IUserService
    {
        private readonly VapourDatabaseEntities _dataContext;
        private readonly IAuthenticator _authenticator;

        public UserService(VapourDatabaseEntities dataContext, IAuthenticator authenticator)
        {
            _dataContext = dataContext;
            _authenticator = authenticator;
        }

        public async Task<bool> UpdateWalletBalance(int userId, decimal value)
        {
            _dataContext.Users
                .First(u => u.Id == userId)
                .WalletBalance = value;

            await _dataContext.SaveChangesAsync();

            return true;
        }
    }
}