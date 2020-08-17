using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

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
    public ActionResult<EttEmployee> GetEmployeeInfo()
    {
      var email = User.FindFirst(x => x.Type == "email").Value;
      var response = _EmployeeService.RetrieveEmployeeInfo(email);
      if (response == null)
      {
        return NotFound();
      }
      return Ok(JsonConvert.SerializeObject(response));
    }

    [HttpGet]
    [Authorize]
    [Route("hours")]
    public ActionResult<EmployeeResponse> GetEmployeeMetrics()
    {
      var email = User.FindFirst(x => x.Type == "email").Value;
      var response = _EmployeeService.RetrieveEmployeeMetrics(email);
      if (response == null)
      {
        return NotFound();
      }
      return Ok(JsonConvert.SerializeObject(response));
    }

    [HttpGet]
    [Authorize]
    [Route("hours/all")]
    public ActionResult<List<EmployeeResponse>> GetAllEmployeeMetrics()
    {
      var response = _EmployeeService.RetrieveAllEmployeeMetrics();
      if (response == null)
      {
        return NotFound();
      }
      return Ok(JsonConvert.SerializeObject(response));
    }
  }
}
