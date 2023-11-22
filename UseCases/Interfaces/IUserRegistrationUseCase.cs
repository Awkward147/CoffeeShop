using Core;
using System.Threading.Tasks;

namespace UseCases.Interfaces
{
    public interface IUserRegistrationUseCase
    {
        Task<UseCaseResult> RegisterUserAsync(UserRegisterModel user);
        Task<UseCaseResult> LoginUserAsync(UserLoginModel user);
    }
}