using job_app_management_system.api.Data;
using job_app_management_system.api.Models;
using job_app_management_system.api.Models.Dto;
using job_app_management_system.api.Models.DTOs;
using job_app_management_system.api.Models.job_app_management_system.api.Models;
using job_app_management_system.api.Result;
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

        private static ApplicationDto ApplictionToApplicationDto(Application application)
        {
            return new ApplicationDto
            {
                JobId = application.Id,
                JobName = application.JobName,
                Location = application.Location,
                PublishDate = application.PublishDate,
                Deadline = application.Deadline,
                Salary = application.Salary,
                IsNegotiable = application.IsNegotiable,
                Description = application.Description,
                Requirements = application.Requirements?.Select(r => r.Description).ToList() ?? new List<string>(),
                Responsibilities = application.Responsibilities?.Select(r => r.Description).ToList() ?? new List<string>(),
                MaximumApplication = application.MaximumApplication,
                AcceptingResponse = application.AcceptingResponse,
                AlreadyApplied = application.JobApplications.Count,
                JobApplications = application.JobApplications?.Select(ja => new JobApplicationDto
                {
                    Id = ja.Id,
                    ApplicationId = ja.ApplicationId,
                    Name = ja.Name,
                    Email = ja.Email,
                    Phone = ja.Phone,
                    Dob = ja.Dob,
                    IsAiubian = ja.IsAiubian,
                    IsBscCompleted = ja.IsBscCompleted,
                    IsMscCompleted = ja.IsMscCompleted,
                    AiubId = ja.AiubId,
                    BscUniversity = ja.BscUniversity,
                    BscDepartment = ja.BscDepartment,
                    BscCGPA = ja.BscCGPA,
                    BscAdmissionYear = ja.BscAdmissionYear,
                    BscGraduationYear = ja.BscGraduationYear,
                    MscUniversity = ja.MscUniversity,
                    MscDepartment = ja.MscDepartment,
                    MscCGPA = ja.MscCGPA,
                    MscAdmissionYear = ja.MscAdmissionYear,
                    MscGraduationYear = ja.MscGraduationYear,
                    Skills = ja.Skills?.Select(s => s.Name).ToList() ?? new List<string>()
                }).ToList() ?? new List<JobApplicationDto>()
            };
        }

        public Result<bool> Add(ApplicationDto entity)
        {
            try
            {
                var application = new Application
                {
                    JobName = entity.JobName,
                    PublishDate = entity.PublishDate,
                    Deadline = entity.Deadline,
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
                    AcceptingResponse = true,
                    JobApplications = new List<JobApplication>()
                };
                   

                dbContext.Applications.Add(application);
                dbContext.SaveChanges();

                return new Result<bool> { IsError = false, Messages = new List<string> { "added"}, Data = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Result<bool> { IsError = false, Messages = new List<string> { "failed" }, Data = false }; ;
            }
        }


        public Result<List<ApplicationDto>> GetAll()
        {
            return new Result<List<ApplicationDto>>
            {
                IsError = false,
                Messages = new List<string> { "all appication" },
                Data = dbContext.Applications
                .Include(a => a.Requirements)
                .Include(a => a.Responsibilities)
                .Include(a => a.JobApplications)
                    .ThenInclude(ja => ja.Skills)
                .Select(application => ApplictionToApplicationDto(application))
                .ToList()
            };
        }



        public Result<ApplicationDto> GetByID(long id)
        {
            var application = dbContext.Applications
                .Include(a => a.Requirements)
                .Include(a => a.Responsibilities)
                .Include(a => a.JobApplications)
                    .ThenInclude(ja => ja.Skills)
                .FirstOrDefault(a => a.Id == id);

            if (application == null)
            {
                return null;
            }

            return new Result<ApplicationDto>
            {
                IsError = false,
                Messages = new List<string> { "application of id: " + id },
                Data = ApplictionToApplicationDto(application)
            };
        }



        public Result<ApplicationDto> Remove(long id)
        {
            var application = dbContext.Applications
                .Include(a => a.Requirements)
                .Include(a => a.Responsibilities)
                .Include(a => a.JobApplications)
                    .ThenInclude(ja => ja.Skills)
                .FirstOrDefault(a => a.Id == id);

            if(application == null) return new Result<ApplicationDto>() { IsError = true, Messages = new List<string> { "not found" }, Data = null };
            if(application.JobApplications.Count > 0) return new Result<ApplicationDto>() { IsError = true, Messages = new List<string> { "application exist" }, Data = null };

            this.dbContext.Remove(application);
            this.dbContext.SaveChanges();

            return new Result<ApplicationDto>() { IsError = false, Messages = new List<string> { "adelted" }, Data = ApplictionToApplicationDto(application) };
        }

        public Result<bool> RemoveAll()
        {
            throw new NotImplementedException();
        }

        public Result<ApplicationDto> Update(ApplicationDto entity)
        {
            var existingApplication = dbContext.Applications
                                .Include(a => a.Requirements)
                                .Include(a => a.Responsibilities)
                                .Include(a => a.JobApplications)
                                .FirstOrDefault(a => a.Id == entity.JobId);

            if (existingApplication == null)
            {
                return null;
            }

            if(entity.MaximumApplication < existingApplication.JobApplications.Count)
            {
                return new Result<ApplicationDto>
                {
                    IsError = true,
                    Messages = new List<string> { "Cannot set lower value than applicant count" },
                    Data = null
                };
            }

            try
            {
                existingApplication.JobName = entity.JobName;
                existingApplication.PublishDate = entity.PublishDate;
                existingApplication.Location = entity.Location;
                existingApplication.Salary = entity.Salary;
                existingApplication.IsNegotiable = entity.IsNegotiable;
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

                return new Result<ApplicationDto>
                {
                    IsError = false,
                    Messages = new List<string> { "updated" },
                    Data = entity
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
