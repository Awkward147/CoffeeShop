using Core;
using Refit;
using System.Threading.Tasks;

namespace CoffeeShopFront.APIInterfaces
{
    public interface CoffeeAPI
    {
        [Post("/api/account/register")]
        Task<ApiResponse<string>> Register([Body] UserRegisterModel model);

        [Post("/api/account/login")]
        Task<ApiResponse<string>> Login([Body] UserLoginModel model);
    }
}
