using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using ETT_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
