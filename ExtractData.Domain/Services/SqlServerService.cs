using ExtractData.Domain.Constantes;
using ExtractData.Domain.Interfaces;
using ExtractData.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ExtractData.Domain.Services
{
    public class SqlServerService : ISqlServer
    {
        private string _StrConnection;

        private ISQL _SQL;

        public SqlServerService(ISQL SQL)
        {
            _SQL = SQL;
        }

        public void SetConnectionStrings(string StrConnection)
        {
            _StrConnection = StrConnection;
        }

        public List<ShowColumn> ShowColumn(string Database, string Table)
        {
            try
            {
                var Connection = _SQL.CreateSqlServerConnection(_StrConnection);
                var Command = _SQL.CreateSqlServerCommand(string.Format(SqlServer.ShowColumns, Table), CommandType.Text);
                return _SQL.ExecuteSqlServerCommandList<ShowColumn>(Connection, Command);
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
                var Connection = _SQL.CreateSqlServerConnection(_StrConnection);
                var Command = _SQL.CreateSqlServerCommand(SqlServer.ShowDatabases, CommandType.Text);
                return _SQL.ExecuteSqlServerCommandList<ShowDatabase>(Connection, Command);
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
                var Connection = _SQL.CreateSqlServerConnection(_StrConnection);
                var Command = _SQL.CreateSqlServerCommand(string.Format(SqlServer.ShowTables, Database), CommandType.Text);
                return _SQL.ExecuteSqlServerCommandList<ShowTable>(Connection, Command);
            }
            catch (Exception ex)
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
            sql += $" FROM {Database}..{Table}";

            var SqlResult = ExecuteQuery(sql);

            var insert = string.Empty;

            while (SqlResult.Read())
            {
                insert += $"INSERT INTO { Database}..{ Table} VALUES (";

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

        /// <summary>
        /// Método que executa os comandos SQL dentro do servidor
        /// </summary>
        /// <param name="Query">Query SQL</param>
        /// <returns>Retorna um DataReader com o resultado da query</returns>
        public IDataReader ExecuteQuery(string Query)
        {
            var Connection = _SQL.CreateSqlServerConnection(_StrConnection);
            var Command = _SQL.CreateSqlServerCommand(Query, CommandType.Text);
            var Reader = _SQL.ExecuteSqlServerCommand(Connection, Command);
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
