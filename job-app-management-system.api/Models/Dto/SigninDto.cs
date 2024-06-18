using System.ComponentModel.DataAnnotations;

namespace job_app_management_system.api.Models.Dto
{
    public class SigninDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string? Token { get; set; }
        public bool? TokenExist { get; set; }
    }
}
