using ExtractData.Domain.Constantes;
using ExtractData.Domain.Interfaces;
using ExtractData.Entities.ValueObjects.MySqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ExtractData.Domain.Services
{
    public class MysqlServerService : IMySql
    {
        private string _StrConnection;

        public MysqlServerService(string StrConnection)
        {
            _StrConnection = StrConnection;
        }

        public List<ShowColumn> ShowColumn(string Database, string Table)
        {
            try
            {
                var Sql = new SQL();
                var Connection = Sql.CreateMySqlServerConnection(_StrConnection);
                var Command = Sql.CreateMySqlServerCommand(string.Format(MySqlServer.ShowColumns, Table, Database), CommandType.Text);

                return Sql.ExecuteMySqlServerCommandList<ShowColumn>(Connection, Command);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ShowDatabase> ShowDatabase()
        {
            try
            {
                var Sql = new SQL();
                var Connection = Sql.CreateMySqlServerConnection(_StrConnection);
                var Command = Sql.CreateMySqlServerCommand(MySqlServer.ShowDatabases, CommandType.Text);

                return Sql.ExecuteMySqlServerCommandList<ShowDatabase>(Connection, Command);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public List<ShowTable> ShowTable(string Database)
        {
            try
            {
                var Sql = new SQL();
                var Connection = Sql.CreateMySqlServerConnection(_StrConnection);
                var Command = Sql.CreateMySqlServerCommand(string.Format(MySqlServer.ShowTables, Database), CommandType.Text);
                return Sql.ExecuteMySqlServerCommandList<ShowTable>(Connection, Command);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateScriptsSql(List<ShowColumn> columns, string Database, string Table)
        {
            string sql = "SELECT ";

            foreach (var column in columns)
            {
                sql += $"{column.Field},";
            }

            sql = sql.Remove(sql.Length - 1, 1);
            sql += $" FROM {Database}.{Table}";

            var SqlResult = ExecuteQuery(sql);

            var insert = string.Empty;
           
            while (SqlResult.Read())
            {
                insert += $"INSERT INTO { Database}.{ Table} VALUES (";

                foreach (var column in columns)
                {
                    var campo = SqlResult[column.Field];
                    insert += $" {ToType(campo, column.Type)},";
                }

                insert = insert.Remove(insert.Length - 1, 1);
                insert += ");\n";

            }

            return insert;
        }

        public IDataReader ExecuteQuery(string Query)
        {
            var Sql = new SQL();
            var Connection = Sql.CreateMySqlServerConnection(_StrConnection);
            var Command = Sql.CreateMySqlServerCommand(Query, CommandType.Text);
            var Reader = Sql.ExecuteMySqlServerCommand(Connection, Command);
            return Reader;
        }

        private string ToType(object text, string type)
        {
            if (type.Contains("varchar"))
                return $"'{text}'";

            return text.ToString();
        }

    }
}
