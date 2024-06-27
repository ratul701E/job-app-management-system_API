using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace job_app_management_system.api.Models.Dto
{
    public class JobApplicationDto
    {
        public long? Id { get; set; }

        public long ApplicationId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string? Dob { get; set; }

        public bool IsAiubian { get; set; }
        public bool IsBscCompleted { get; set; }
        public bool IsMscCompleted { get; set; }

        public string AiubId { get; set; }

        public string BscUniversity { get; set; }
        public string BscDepartment { get; set; }
        public double? BscCGPA { get; set; }
        public int? BscAdmissionYear { get; set; }
        public int? BscGraduationYear { get; set; }

        public string MscUniversity { get; set; }
        public string MscDepartment { get; set; }
        public double? MscCGPA { get; set; }
        public int? MscAdmissionYear { get; set; }
        public int? MscGraduationYear { get; set; }

        public List<string> Skills { get; set; }

        public double ExpectedSalary { get; set; }

        public IFormFile CV { get; set; }
        public IFormFile CoverLetter { get; set; }
    }
}
