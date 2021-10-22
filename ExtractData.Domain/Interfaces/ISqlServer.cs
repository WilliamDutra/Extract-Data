using ExtractData.Entities.ValueObjects.MySqlServer;
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

    }
}
