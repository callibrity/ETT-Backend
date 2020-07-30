using ETT_Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ETT_Backend.Configuration.Security;
using ETT_Backend.Configuration.Security.hmac;

namespace ETT_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private ITableService _TableService;

        public TableController(ITableService tableService)
        {
            _TableService = tableService;
        }

        [HttpPost]
        [AuthTest]
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
