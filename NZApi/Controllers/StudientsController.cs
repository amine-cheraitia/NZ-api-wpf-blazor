using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudientsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents() 
        {
            string[] studentsNames = new string[] { "tahar", "mahmoud", "bakhra", "ok", "no", "la" };
            return Ok(studentsNames);
        }
    }
}
