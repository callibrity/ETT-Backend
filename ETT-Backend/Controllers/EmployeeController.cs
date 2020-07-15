using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace ETT_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _EmployeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _EmployeeService = employeeService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<EmployeeResponse> Get()
        {
            var email = User.FindFirst(x => x.Type == "email").Value;
            var response = _EmployeeService.RetrieveEmployeeInfo(email);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(JsonConvert.SerializeObject(response));
        }
    }
}
