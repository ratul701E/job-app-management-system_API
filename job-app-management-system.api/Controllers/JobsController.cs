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
            return this.applicationService.GetAll();
        }

        [HttpGet("{appId:int}")]
        public Result<ApplicationDto> GetApplicationByID(int appId)
        {
            var result = this.applicationService.GetByID(appId);
            return result;
        }

        [HttpPatch("{appId:int}")]
        [Authorize]
        public Result<ApplicationDto> UpdateApplication(int appId, [FromBody] ApplicationDto application)
        {
            return this.applicationService.Update(application);
        }

        [HttpPost]
        [Authorize]
        public Result<bool> PostApplication([FromBody] ApplicationDto applicationDto)
        {
            return this.applicationService.Add(applicationDto);
        }

        [HttpDelete("{appId:int}")]
        [Authorize]
        public Result<ApplicationDto> DeleteApplication(int appId)
        {
            return this.applicationService.Remove(appId);
        }
    }
}
