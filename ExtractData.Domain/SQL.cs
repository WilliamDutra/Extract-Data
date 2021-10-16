using Dapper;
using ExtractData.Domain.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExtractData.Domain
{
    public class SQL : ISQL
    {

        public SqlConnection CreateSqlServerConnection(string strConnection)
        {
            try
            {
                SqlConnection connection = new SqlConnection(strConnection);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public MySqlConnection CreateMySqlServerConnection(string strConnection)
        {
            try
            {
                MySqlConnection connection = new MySqlConnection(strConnection);
                connection.Open();
                return connection;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public SqlCommand CreateSqlServerCommand(string Command, CommandType CommandType)
        {
            try
            {
                SqlCommand command = new SqlCommand();
                command.CommandText = Command;
                command.CommandType = CommandType;

                return command;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T ExecuteSqlServerCommand<T>(SqlConnection SqlConnection, SqlCommand Command)
        {
            try
            {

                return (T)SqlConnection.Query<T>(Command.CommandText, commandType: Command.CommandType);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> ExecuteSqlServerCommandList<T>(SqlConnection SqlConnection, SqlCommand Command)
        {
            try
            {
                return SqlConnection.Query<T>(Command.CommandText, commandType: Command.CommandType).AsList<T>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public MySqlCommand CreateMySqlServerCommand(string Command, CommandType CommandType)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                command.CommandText = Command;
                command.CommandType = CommandType;

                return command;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<T> ExecuteMySqlServerCommandList<T>(MySqlConnection SqlConnection, MySqlCommand Command)
        {
            try
            {

                return SqlConnection.Query<T>(Command.CommandText, commandType: Command.CommandType).AsList<T>();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public T ExecuteMySqlServerCommand<T>(MySqlConnection SqlConnection, MySqlCommand Command)
        {
            try
            {

                return (T)SqlConnection.Query<T>(Command.CommandText, commandType: Command.CommandType);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
