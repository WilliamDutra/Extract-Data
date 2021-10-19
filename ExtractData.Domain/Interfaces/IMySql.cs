using ExtractData.Entities.ValueObjects.MySqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ExtractData.Domain.Interfaces
{
    public interface IMySql
    {
        List<ShowDatabase> ShowDatabase();

        List<ShowTable> ShowTable(string Database);

        List<ShowColumn> ShowColumn(string Database, string Table);

        string GenerateScriptsSql(List<ShowColumn> columns, string Database, string Table);

        IDataReader ExecuteQuery(string Query);

    }
}
