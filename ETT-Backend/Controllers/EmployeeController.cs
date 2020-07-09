using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ETT_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _EmployeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _EmployeeService = employeeService;
        }

        [HttpGet("{email}")]
        [Authorize]
        public ActionResult<EmployeeResponse> Get(string email)
        {
            var response = _EmployeeService.RetrieveEmployeeInfo(email);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}
