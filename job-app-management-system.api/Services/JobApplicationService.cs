using job_app_management_system.api.Data;
using job_app_management_system.api.Models;
using job_app_management_system.api.Models.Dto;
using job_app_management_system.api.Result;
using job_app_management_system.api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace job_app_management_system.api.Services
{
    public class JobApplicationService : IService<JobApplicationDto>
    {
        private ApplicationDbContext dbContext;
        private readonly IWebHostEnvironment environment;
        public JobApplicationService(ApplicationDbContext dbContext, IWebHostEnvironment environment)
        {
            this.dbContext = dbContext;
            this.environment = environment;
        }

        private static JobApplicationDto JobApplicationToJobApplicationDto(JobApplication jobApplication)
        {
            return new JobApplicationDto
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
                Skills = jobApplication.Skills != null ? jobApplication.Skills.Select(s => s.Name).ToList() : new List<string>(),
                ExpectedSalary = jobApplication.ExpectedSalary
            };
        }

        public Result<bool> Add(JobApplicationDto entity)
        {
            try
            {
                var application = dbContext.Applications
                    .Include(a => a.JobApplications)
                    .FirstOrDefault(a => a.Id == entity.ApplicationId);

                if (application == null)
                {
                    return new Result<bool> { IsError = true, Messages = new List<string> { "Invalid ApplicationId" }, Data = false };
                }

                if (application.JobApplications.Any(ja => ja.Email == entity.Email))
                {
                    return new Result<bool> { IsError = true, Messages = new List<string> { "Email Already Exists" }, Data = false };
                }
                if (application.MaximumApplication == application.JobApplications.Count)
                {
                    return new Result<bool> { IsError = true, Messages = new List<string> { "Limit Reached" }, Data = false };
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
                    ExpectedSalary = entity.ExpectedSalary,
                };

                dbContext.JobApplications.Add(jobApplication);
                dbContext.SaveChanges();


                this.SaveFile(entity.CV, "CV_" + entity.Email);
                this.SaveFile(entity.CoverLetter, "CoverLetter_" + entity.Email);

                return new Result<bool> { IsError = false, Messages = new List<string> { "Added successfully" }, Data = true };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new Result<bool> { IsError = true, Messages = new List<string> { "Failed to add job application" }, Data = false };
            }
        }




        public Result<List<JobApplicationDto>> GetAll()
        {
            return new Result<List<JobApplicationDto>>
            {
                IsError = false,
                Messages = new List<string> { "All jobs" },
                Data = this.dbContext.JobApplications
                        .Select(jobApp => JobApplicationToJobApplicationDto(jobApp))
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
                Data = JobApplicationToJobApplicationDto(jobApplication)
            };
        }

        public Result<JobApplicationDto> Remove(long id)
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

        /**/

        private string? SaveFile(IFormFile file, string name)
        {
            if (file == null || file.Length == 0) return null;

            var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles");

            if (!Directory.Exists(uploadsPath)) Directory.CreateDirectory(uploadsPath);

            var fileName = name + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }
    }
}


/**/
