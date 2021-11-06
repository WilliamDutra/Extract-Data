using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ExtractData.Domain.Interfaces
{
    public interface ISQL
    {
        public MySqlConnection CreateMySqlServerConnection(string strConnection);

        public SqlConnection CreateSqlServerConnection(string strConnection);

        public SqlCommand CreateSqlServerCommand(string Command, CommandType CommandType);

        public MySqlCommand CreateMySqlServerCommand(string Command, CommandType CommandType);

        public T ExecuteSqlServerCommand<T>(SqlConnection SqlConnection, SqlCommand Command);

        public List<T> ExecuteMySqlServerCommandList<T>(MySqlConnection SqlConnection, MySqlCommand Command);

        public T ExecuteMySqlServerCommand<T>(MySqlConnection SqlConnection, MySqlCommand Command);

        public List<T> ExecuteSqlServerCommandList<T>(SqlConnection SqlConnection, SqlCommand Command);

        public IDataReader ExecuteMySqlServerCommand(MySqlConnection SqlConnection, MySqlCommand Command);

        public IDataReader ExecuteSqlServerCommand(SqlConnection SqlConnection, SqlCommand Command);

    }
}
