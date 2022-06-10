using System.Threading.Tasks;

namespace Vapour.Services
{
    public interface IUserService
    {
        Task<bool> UpdateWalletBalance(int userId, decimal value);
    }
}