using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ExtractData.Domain.Interfaces
{
    public interface ISQL
    {
        public SqlConnection CreateConnection(string strConnection);

        public SqlCommand CreateCommand(string Command, CommandType CommandType);

    }
}
