using job_app_management_system.api.Models.job_app_management_system.api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace job_app_management_system.api.Models
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        
        public string JobName { get; set; }

        
        public string PublishDate {  get; set; }
        public string Deadline {  get; set; }

        
        public string Location { get; set; }

        
        public double Salary { get; set; }

        public string Description { get; set; }

        public ICollection<Requirement> Requirements { get; set; }
        public ICollection<Responsibility> Responsibilities { get; set; }
        public ICollection<JobApplication> JobApplications { get; set; }

        public int MaximumApplication { get; set; }
        public bool AcceptingResponse {  get; set; }
        public bool IsNegotiable { get; set; }
    }
}
