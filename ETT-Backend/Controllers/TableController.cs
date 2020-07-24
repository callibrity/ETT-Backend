using ETT_Backend.Interfaces;
using ETT_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using ETT_Backend.Models.Constants;
using static ETT_Backend.Models.Constants.Constants;

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
        [Authorize]
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
