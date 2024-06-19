using job_app_management_system.api.Data;
using job_app_management_system.api.Models;
using job_app_management_system.api.Models.DTOs;
using job_app_management_system.api.Result;
using job_app_management_system.api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace job_app_management_system.api.Controllers
{
    [Route("api/jobs")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private ApplicationService applicationService;
        public JobsController(ApplicationDbContext dbContext)
        {
            this.applicationService = new ApplicationService(dbContext);
        }

        [HttpGet]

        public Result<List<ApplicationDto>> GetAllApplication()
        {
            return new Result<List<ApplicationDto>>(false, new List<string> { "Return all applications" }, this.applicationService.GetAll());
        }

        [HttpGet("{appId:int}")]
        public Result<ApplicationDto> GetAllApplicationByID(int appId)
        {
            var result = this.applicationService.GetByID(appId);
            return result != null ? new Result<ApplicationDto>(false, new List<string> { "Here are the data" }, result) : new Result<ApplicationDto>(false, new List<string> { "No application found with id: " + appId }, null);
        }

        [HttpPatch("{appId:int}")]
        [Authorize]
        public Result<ApplicationDto> UpdateApplication(int appId, [FromBody] ApplicationDto application)
        {
            return new Result<ApplicationDto>(false, new List<string> { "Updated" }, this.applicationService.Update(application));
        }

        [HttpPost]
        [Authorize]
        public Result<ApplicationDto> PostApplication([FromBody] ApplicationDto applicationDto)
        {
            var result = this.applicationService.Add(applicationDto);
            return result ? new Result<ApplicationDto>(false, new List<string> { "Added Successfully" }, applicationDto) : new Result<ApplicationDto>(true, new List<string> { "Failed to add" }, null);
        }
    }
}
