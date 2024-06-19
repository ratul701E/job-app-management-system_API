using job_app_management_system.api.Data;
using job_app_management_system.api.Models;
using job_app_management_system.api.Models.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace job_app_management_system.api.Services
{
    public class AuthService
    {
        private IConfiguration configuration;
        private ApplicationDbContext applicationDbContext;

        public AuthService(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            this.configuration = configuration;
            this.applicationDbContext = applicationDbContext;
        }

        public SigninDto Signin(SigninDto signinDto)
        {
            //check user is correct

            List<User> users = this.applicationDbContext.Users
                        .Select(user => new User
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Password = user.Password,
                            Role = user.Role,
                        })
                        .ToList();
            foreach (var user in users) {
                if (user.Email.Equals(signinDto.Email))
                {
                    if(user.Password.Equals(signinDto.Password))
                    {
                        List<Claim> claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "Admin"),
                            new Claim(ClaimTypes.Email, signinDto.Email)
                        };

                        signinDto.TokenExist = true;
                        signinDto.Token = this.createToken(claims);
                        signinDto.Password = "removed :D";

                        return signinDto;
                    }
                }
            }
            signinDto.TokenExist = false;
            return signinDto;


            
        }

        private string createToken(List<Claim> claims)
        {

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("TokenConf:secret").Value!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddMinutes(20)
                );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
}
