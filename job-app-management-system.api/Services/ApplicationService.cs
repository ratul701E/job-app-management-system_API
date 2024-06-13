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
                    MaximumApplication = entity.MaximumApplication,
                    AcceptingResponse = true
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
                            JobId = application.Id,
                            JobName = application.JobName,
                            Location = application.Location,
                            PublishDate = application.PublishDate,
                            Salary = application.Salary,
                            Description = application.Description,
                            Requirements = application.Requirements != null ? application.Requirements.Select(r => r.Description).ToList() : new List<string>(),
                            Responsibilities = application.Responsibilities != null ? application.Responsibilities.Select(r => r.Description).ToList() : new List<string>(),
                            MaximumApplication = application.MaximumApplication,
                            AcceptingResponse = application.AcceptingResponse
                        })
                        .ToList();
        }


        public ApplicationDto GetByID(long id)
        {
            var application = dbContext.Applications
                               .Include(a => a.Requirements)
                               .Include(a => a.Responsibilities)
                               .FirstOrDefault(a => a.Id == id);


            if (application == null)
            {
                return null;
            }

            var applicationDto = new ApplicationDto
            {
                JobId = application.Id,
                JobName = application.JobName,
                Location = application.Location,
                PublishDate = application.PublishDate,
                Salary = application.Salary,
                Description = application.Description,
                Requirements = application.Requirements != null ? application.Requirements.Select(r => r.Description).ToList() : new List<string>(),
                Responsibilities = application.Responsibilities != null ? application.Responsibilities.Select(r => r.Description).ToList() : new List<string>(),
                MaximumApplication = application.MaximumApplication,
                AcceptingResponse = application.AcceptingResponse
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
            var existingApplication = dbContext.Applications
                                .Include(a => a.Requirements)
                                .Include(a => a.Responsibilities)
                                .FirstOrDefault(a => a.Id == entity.JobId);

            if (existingApplication == null)
            {
                return null;
            }

            try
            {
                existingApplication.JobName = entity.JobName;
                existingApplication.PublishDate = entity.PublishDate;
                existingApplication.Location = entity.Location;
                existingApplication.Salary = entity.Salary;
                existingApplication.Description = entity.Description;
                existingApplication.Requirements.Clear();
                existingApplication.Responsibilities.Clear();

                foreach (var requirement in entity.Requirements)
                {
                    existingApplication.Requirements.Add(new Requirement { Description = requirement });
                }

                foreach (var responsibility in entity.Responsibilities)
                {
                    existingApplication.Responsibilities.Add(new Responsibility { Description = responsibility });
                }

                existingApplication.MaximumApplication = entity.MaximumApplication;
                existingApplication.AcceptingResponse = entity.AcceptingResponse;

                dbContext.SaveChanges();

                return entity;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
