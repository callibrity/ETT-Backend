using ETT_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ETT_Backend.Configuration.Security.basic;
using Microsoft.AspNetCore.Authorization;

namespace ETT_Backend.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private ITableService _TableService;

        public TableController(ITableService tableService)
        {
            _TableService = tableService;
        }

        [HttpPost]
        [ApiAuth]
        [Route("{table}")]
        public ActionResult InsertData(string table, List<dynamic> rows)
        {
            var response = _TableService.InsertRows(table, rows);
            if (response < 0)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
