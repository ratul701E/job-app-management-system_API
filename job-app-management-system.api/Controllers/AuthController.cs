using job_app_management_system.api.Data;
using job_app_management_system.api.Models.Dto;
using job_app_management_system.api.Result;
using job_app_management_system.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace job_app_management_system.api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService authService;
        public AuthController(IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
            authService = new AuthService(configuration, applicationDbContext);
        }
        [HttpPost("signin")]
        public Result<SigninDto>  SignIn([FromBody] SigninDto signinDto)
        {
            return this.authService.Signin(signinDto);
        }
    }
}
