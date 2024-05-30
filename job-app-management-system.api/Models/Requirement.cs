namespace job_app_management_system.api.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace job_app_management_system.api.Models
    {
        public class Requirement
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public long Id { get; set; }

            [Required]
            public string Description { get; set; }

            [ForeignKey("Application")]
            public long ApplicationId { get; set; }

            public Application Application { get; set; }
        }
    }

}
