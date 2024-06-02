using job_app_management_system.api.Data;
using job_app_management_system.api.Models;
using job_app_management_system.api.Models.DTOs;
using job_app_management_system.api.Models.job_app_management_system.api.Models;
using job_app_management_system.api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace job_app_management_system.api.Services
{
    
    public class ApplicationService : IService<ApplicationDto>
    {
        private ApplicationDbContext dbContext;
        public ApplicationService(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public bool Add(ApplicationDto entity)
        {
            try
            {
                var application = new Application
                {
                    JobName = entity.JobName,
                    PublishDate = entity.PublishDate,
                    Location = entity.Location,
                    Salary = entity.Salary,
                    Description = entity.Description,
                    Requirements = entity.Requirements.Select(r => new Requirement
                    {
                        Description = r
                    }).ToList(),
                    Responsibilities = entity.Responsibilities.Select(r => new Responsibility
                    {
                        Description = r
                    }).ToList(),
                    MaximumApplication = entity.MaximumApplication
                };

                dbContext.Applications.Add(application);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        public List<ApplicationDto> GetAll()
        {
            return this.dbContext.Applications
                        .Select(application => new ApplicationDto
                        {
                            Id = application.Id,
                            JobName = application.JobName,
                            Location = application.Location,
                            PublishDate = application.PublishDate,
                            Salary = application.Salary,
                            Description = application.Description,
                            Requirements = application.Requirements != null ? application.Requirements.Select(r => r.Description).ToList() : new List<string>(),
                            Responsibilities = application.Responsibilities != null ? application.Responsibilities.Select(r => r.Description).ToList() : new List<string>(),
                            MaximumApplication = application.MaximumApplication
                        })
                        .ToList();
        }


        public ApplicationDto GetByID(int id)
        {
            var application = dbContext.Applications.FirstOrDefault(a => a.Id == id);

            if (application == null)
            {
                return null;
            }

            var applicationDto = new ApplicationDto
            {
                Id = application.Id,
                JobName = application.JobName,
                Location = application.Location,
                PublishDate = application.PublishDate,
                Salary = application.Salary,
                Description = application.Description,
                Requirements = application.Requirements != null ? application.Requirements.Select(r => r.Description).ToList() : new List<string>(),
                Responsibilities = application.Responsibilities != null ? application.Responsibilities.Select(r => r.Description).ToList() : new List<string>(),
                MaximumApplication = application.MaximumApplication
            };

            return applicationDto;
        }



        public ApplicationDto Remove(ApplicationDto entity)
        {
            throw new NotImplementedException();
        }

        public bool RemoveAll()
        {
            throw new NotImplementedException();
        }

        public ApplicationDto Update(ApplicationDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
