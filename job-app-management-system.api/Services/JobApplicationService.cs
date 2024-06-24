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
    public class JobApplicationService : IService<JobApplicationDto>
    {
        private ApplicationDbContext dbContext;
        public JobApplicationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Result<bool> Add(JobApplicationDto entity)
        {
            try
            {
                // Check if the ApplicationId exists in the Applications table
                var applicationExists = dbContext.Applications.Any(a => a.Id == entity.ApplicationId);
                if (!applicationExists)
                {
                    return new Result<bool> { IsError = true, Messages = new List<string> { "Invalid ApplicationId" }, Data = false };
                }

                var jobApplication = new JobApplication
                {
                    ApplicationId = entity.ApplicationId,
                    Name = entity.Name,
                    Email = entity.Email,
                    Phone = entity.Phone,
                    Dob = entity.Dob,
                    IsAiubian = entity.IsAiubian,
                    IsBscCompleted = entity.IsBscCompleted,
                    IsMscCompleted = entity.IsMscCompleted,
                    AiubId = entity.AiubId,
                    BscUniversity = entity.BscUniversity,
                    BscDepartment = entity.BscDepartment,
                    BscCGPA = entity.BscCGPA,
                    BscAdmissionYear = entity.BscAdmissionYear,
                    BscGraduationYear = entity.BscGraduationYear,
                    MscUniversity = entity.MscUniversity,
                    MscDepartment = entity.MscDepartment,
                    MscAdmissionYear = entity.MscAdmissionYear,
                    MscGraduationYear = entity.MscGraduationYear,
                    MscCGPA = entity.MscCGPA,
                    Skills = entity.Skills.Select(r => new Skill
                    {
                        Name = r
                    }).ToList(),
                };

                dbContext.JobApplications.Add(jobApplication);
                dbContext.SaveChanges();

                return new Result<bool> { IsError = false, Messages = new List<string> { "Added successfully" }, Data = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Result<bool> { IsError = true, Messages = new List<string> { "Failed to add job application" }, Data = false };
            }
        }



        public Result<List<JobApplicationDto>> GetAll()
        {
            return new Result<List<JobApplicationDto>> { IsError = false, Messages = new List<string>{ "All jobs"},
                Data = this.dbContext.JobApplications
                        .Select(jobApp => new JobApplicationDto
                        {
                            Id = jobApp.Id,
                            Name = jobApp.Name,
                            Email = jobApp.Email,
                            Phone = jobApp.Phone,
                            Dob = jobApp.Dob,
                            IsAiubian = jobApp.IsAiubian,
                            IsBscCompleted = jobApp.IsBscCompleted,
                            IsMscCompleted = jobApp.IsMscCompleted,
                            AiubId = jobApp.AiubId,
                            BscUniversity = jobApp.BscUniversity,
                            BscDepartment = jobApp.BscDepartment,
                            BscCGPA = jobApp.BscCGPA,
                            BscAdmissionYear = jobApp.BscAdmissionYear,
                            BscGraduationYear = jobApp.BscGraduationYear,
                            MscUniversity = jobApp.MscUniversity,
                            MscDepartment = jobApp.MscDepartment,
                            MscAdmissionYear = jobApp.MscAdmissionYear,
                            MscGraduationYear = jobApp.MscGraduationYear,
                            MscCGPA = jobApp.MscCGPA,
                            Skills = jobApp.Skills.Select(s => s.Name).ToList()
                        })
                        .ToList()
            };
        }



        public Result<JobApplicationDto> GetByID(long id)
        {
            var jobApplication = dbContext.JobApplications
                                          .Include(j => j.Skills)
                                          .FirstOrDefault(j => j.Id == id);

            if (jobApplication == null)
            {
                return null;
            }

            return new Result<JobApplicationDto>
            {
                IsError = false,
                Messages = new List<string> { "job of id " + id },
                Data = new JobApplicationDto
                {
                    Id = jobApplication.Id,
                    Name = jobApplication.Name,
                    Email = jobApplication.Email,
                    Phone = jobApplication.Phone,
                    Dob = jobApplication.Dob,
                    IsAiubian = jobApplication.IsAiubian,
                    IsBscCompleted = jobApplication.IsBscCompleted,
                    IsMscCompleted = jobApplication.IsMscCompleted,
                    AiubId = jobApplication.AiubId,
                    BscUniversity = jobApplication.BscUniversity,
                    BscDepartment = jobApplication.BscDepartment,
                    BscCGPA = jobApplication.BscCGPA,
                    BscAdmissionYear = jobApplication.BscAdmissionYear,
                    BscGraduationYear = jobApplication.BscGraduationYear,
                    MscUniversity = jobApplication.MscUniversity,
                    MscDepartment = jobApplication.MscDepartment,
                    MscAdmissionYear = jobApplication.MscAdmissionYear,
                    MscGraduationYear = jobApplication.MscGraduationYear,
                    MscCGPA = jobApplication.MscCGPA,
                    Skills = jobApplication.Skills != null ? jobApplication.Skills.Select(s => s.Name).ToList() : new List<string>()
                }
            };
        }


        public Result<JobApplicationDto> Remove(JobApplicationDto entity)
        {
            throw new NotImplementedException();
        }

        public Result<bool> RemoveAll()
        {
            throw new NotImplementedException();
        }

        public Result<JobApplicationDto> Update(JobApplicationDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
