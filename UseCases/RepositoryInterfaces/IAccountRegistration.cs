using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.RepositoryInterfaces
{
    public interface IAccountRegistration
    {
        Task<(bool, string)> CreateAccount(UserRegisterModel user);

        Task<(bool, string)> Login(UserLoginModel user);
    }
}
