using ExtractData.Domain.Constantes;
using ExtractData.Domain.Interfaces;
using ExtractData.Entities.ValueObjects.MySqlServer;
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
    }
}
