using ExtractData.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractData.Domain.Interfaces
{
    public interface ISqlServer
    {
        void SetConnectionStrings(string StrConnection);

        List<ShowDatabase> ShowDatabase();

        List<ShowTable> ShowTable(string Database);

        List<ShowColumn> ShowColumn(string Database, string Table);

        public string GenerateScriptsSql(List<ShowColumn> columns, string Database, string Table);
    }
}
