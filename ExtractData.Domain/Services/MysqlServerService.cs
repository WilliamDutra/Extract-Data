using ExtractData.Domain.Constantes;
using ExtractData.Domain.Interfaces;
using ExtractData.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ExtractData.Domain.Services
{
    public class MysqlServerService : IMySql
    {
        
        private string StrConnection;

        private ISQL _SQL;

        public MysqlServerService(ISQL SQL)
        {
            _SQL = SQL;
        }

        /// <summary>
        /// Seleciona a string de conexão
        /// </summary>
        /// <param name="StrConn">String de conexão</param>
        public void SetConnectionStrings(string StrConn)
        {
            StrConnection = StrConn;
        }

        /// <summary>
        /// Método que retorna as colunas da tabela selecionada
        /// </summary>
        /// <param name="Database">Banco selecionado</param>
        /// <param name="Table">Tabela selecionada</param>
        /// <returns>Retorna uma lista com as colunas da tabela selecionada</returns>
        public List<ShowColumn> ShowColumn(string Database, string Table)
        {
            try
            {
                var Connection = _SQL.CreateMySqlServerConnection(StrConnection);
                var Command = _SQL.CreateMySqlServerCommand(string.Format(MySqlServer.ShowColumns, Table, Database), CommandType.Text);
                return _SQL.ExecuteMySqlServerCommandList<ShowColumn>(Connection, Command);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que retorna os bancos criados na string de conexão enviada
        /// </summary>
        /// <returns>Retorna uma lista com todas as bases dentro da string de conexão</returns>
        public List<ShowDatabase> ShowDatabase()
        {
            try
            {
                var Connection = _SQL.CreateMySqlServerConnection(StrConnection);
                var Command = _SQL.CreateMySqlServerCommand(MySqlServer.ShowDatabases, CommandType.Text);
                return _SQL.ExecuteMySqlServerCommandList<ShowDatabase>(Connection, Command);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que retora as tabelas do banco selecionado
        /// </summary>
        /// <param name="Database">Banco selecionado</param>
        /// <returns>Retorna uma lista com as tabelas do banco selecionado</returns>
        public List<ShowTable> ShowTable(string Database)
        {
            try
            {
                var Connection = _SQL.CreateMySqlServerConnection(StrConnection);
                var Command = _SQL.CreateMySqlServerCommand(string.Format(MySqlServer.ShowTables, Database), CommandType.Text);
                return _SQL.ExecuteMySqlServerCommandList<ShowTable>(Connection, Command);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que gera os inserts da tabela selecionada
        /// </summary>
        /// <param name="columns">Colunas da tabela selecionada</param>
        /// <param name="Database">Banco selecionado</param>
        /// <param name="Table">Tabela selecionada</param>
        /// <returns>Retorna os scripts de INSERT com os dados da tabela selcionada</returns>
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

        /// <summary>
        /// Método que executa os comandos SQL dentro do servidor
        /// </summary>
        /// <param name="Query">Query SQL</param>
        /// <returns>Retorna um DataReader com o resultado da query</returns>
        public IDataReader ExecuteQuery(string Query)
        {
            var Connection = _SQL.CreateMySqlServerConnection(StrConnection);
            var Command = _SQL.CreateMySqlServerCommand(Query, CommandType.Text);
            var Reader = _SQL.ExecuteMySqlServerCommand(Connection, Command);
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
