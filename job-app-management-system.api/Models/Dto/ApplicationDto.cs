using job_app_management_system.api.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace job_app_management_system.api.Models.DTOs
{
    public class ApplicationDto
    {
        public long JobId { get; set; }
        [Required]
        public string JobName { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public double Salary { get; set; }

        public string  PublishDate { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public List<string> Requirements { get; set; }

        public List<string> Responsibilities { get; set; }

        public List<JobApplicationDto> JobApplications { get; set; }

        [Required]
        public int MaximumApplication { get; set; }

        public bool AcceptingResponse {  get; set; }
    }

}
