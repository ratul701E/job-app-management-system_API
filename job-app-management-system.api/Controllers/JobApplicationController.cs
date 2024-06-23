using job_app_management_system.api.Data;
using job_app_management_system.api.Models;
using job_app_management_system.api.Models.Dto;
using job_app_management_system.api.Result;
using job_app_management_system.api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace job_app_management_system.api.Controllers
{
    [Route("api/application")]
    [ApiController]
    public class JobApplicationController : ControllerBase
    {
        private JobApplicationService jobApplicationService;
        public JobApplicationController(ApplicationDbContext dbContext)
        {
            this.jobApplicationService = new JobApplicationService(dbContext);
        }

        [HttpGet]
        [Authorize]
        public Result<List<JobApplicationDto>> GetAllApplication()
        {
            return this.jobApplicationService.GetAll();
        }

        [HttpPost]
        public Result<bool> AddApplication([FromBody] JobApplicationDto jobApplication)
        {
           return this.jobApplicationService.Add(jobApplication);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public Result<JobApplicationDto> GetApplicationById(int id)
        {
            return this.jobApplicationService.GetByID(id);
        }
    }
}
