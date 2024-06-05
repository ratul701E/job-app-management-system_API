using job_app_management_system.api.Models;
using job_app_management_system.api.Models.job_app_management_system.api.Models;
using Microsoft.EntityFrameworkCore;

namespace job_app_management_system.api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Responsibility> Responsibilities { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<Skill> Skills { get; set; }
    }
}
