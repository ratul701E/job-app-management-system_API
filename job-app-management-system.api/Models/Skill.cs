using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace job_app_management_system.api.Models
{
    public class Skill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("JobApplication")]
        public long JobApplicationId { get; set; }

        public JobApplication JobApplication { get; set; }
    }
}
