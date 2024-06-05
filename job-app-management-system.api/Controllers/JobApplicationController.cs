using job_app_management_system.api.Data;
using job_app_management_system.api.Models.Dto;
using job_app_management_system.api.Result;
using job_app_management_system.api.Services;
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
        public Result<List<JobApplicationDto>> GetAllApplication()
        {
            return new Result<List<JobApplicationDto>>(false, new List<string> { "All job application" }, this.jobApplicationService.GetAll());
        }
    }
}
