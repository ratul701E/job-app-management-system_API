using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace job_app_management_system.api.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        [HttpGet]
        public bool CheckServerClientConnection()
        {
            return true;
        }
    }
}
