using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.RepositoryInterfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class AccountRegistration : IAccountRegistration
    {
      
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        public AccountRegistration(UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.userManager = userManager;
        }

        public async Task<(bool, string)> CreateAccount(UserRegisterModel model)
        {
            var userExists = await userManager.FindByEmailAsync(model.Email);
            if(userExists != null)
            {
                return (false, "Email already exists.");
            }

            var identityUser = new IdentityUser()
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await  userManager.CreateAsync(identityUser, model.Password);

            if (!result.Succeeded)
            {
                return (false, "User creation failed!");
            }

            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if( await roleManager.RoleExistsAsync("admin"))
            {
                await userManager.AddToRoleAsync(identityUser, "admin");
            }

            return (result.Succeeded, "User created.");
        }

        public async Task<(bool, string)> Login(UserLoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if(user == null)
            {
                return (false, "Invalid username");
            }
            if(!await userManager.CheckPasswordAsync(user, model.Password))
            {
                return (false, "Invalid password");
            }

            var userRoles = await userManager.GetRolesAsync(user);
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach(var role in userRoles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            string token = GenerateToken(userClaims);
            return (true, token);
        }

       

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTKey:Secret"]));
            var _TokenExpiryTimeInHour = Convert.ToInt64(configuration["JWTKey:TokenExpiryTimeInHour"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = configuration["JWTKey:ValidIssuer"],
                Audience = configuration["JWTKey:ValidAudience"],
                Expires = DateTime.UtcNow.AddHours(_TokenExpiryTimeInHour),
               // Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
