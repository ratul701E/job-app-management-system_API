using job_app_management_system.api.Data;
using job_app_management_system.api.Models;
using job_app_management_system.api.Models.Dto;
using job_app_management_system.api.Result;
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

        public Result<SigninDto> Signin(SigninDto signinDto)
        {
            var user = applicationDbContext.Users.FirstOrDefault(u => u.Email == signinDto.Email && u.Password == signinDto.Password);

            if (user != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                signinDto.TokenExist = true;
                signinDto.Token = createToken(claims);
                signinDto.Password = "removed :D";

                return new Result<SigninDto>
                {
                    IsError = false,
                    Messages = new List<string> { "Token generated" },
                    Data = signinDto
                };
            }

            signinDto.TokenExist = false;

            return new Result<SigninDto>
            {
                IsError = true,
                Messages = new List<string> { "Invalid email or password" },
                Data = signinDto
            };
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
