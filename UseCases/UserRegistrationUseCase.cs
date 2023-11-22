using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.RepositoryInterfaces;
using UseCases.Interfaces;

namespace UseCases
{
    public class UserRegistrationUseCase : IUserRegistrationUseCase
    {
        private readonly IAccountRegistration registration;

        public UserRegistrationUseCase(IAccountRegistration registration)
        {
            this.registration = registration;
        }
        public async Task<UseCaseResult> RegisterUserAsync(UserRegisterModel user)
        {
            var userCreated = await registration.CreateAccount(user);

            if (userCreated.Item1)
            {
                return UseCaseResult.Success(userCreated.Item2);
            }
            return UseCaseResult.Failure(userCreated.Item2);
        }

        public async Task<UseCaseResult> LoginUserAsync(UserLoginModel user)
        {
            var loggedin = await registration.Login(user);

            if(loggedin.Item1)
            {
                return UseCaseResult.Success(loggedin.Item2);
            }

            return UseCaseResult.Failure(loggedin.Item2);
        }
    }
}
