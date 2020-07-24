using System.Collections.Generic;
using ETT_Backend.Models;
using ETT_Backend.Models.Constants;
using static ETT_Backend.Models.Constants.Constants;

namespace ETT_Backend.Interfaces 
{
    public interface ITableService
    {
        int InsertRows(string table, List<dynamic> rows);
    }
}