using ExtractData.Domain.Constantes;
using ExtractData.Domain.Interfaces;
using ExtractData.Entities.ValueObjects.MySqlServer;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<ShowDatabase> ShowDatabase()
        {
            try
            {
                var Sql = new SQL();
                var Connection  = Sql.CreateMySqlServerConnection(_StrConnection);
                var Command = Sql.CreateMySqlServerCommand(MysqlServer.ShowDatabases, CommandType.Text);

                return Sql.ExecuteMySqlServerCommandList<ShowDatabase>(Connection, Command);
                
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
