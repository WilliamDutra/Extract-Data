using ExtractData.Domain.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ExtractData.Domain
{
    public class SQL : ISQL
    {

        public SqlConnection CreateConnection(string strConnection)
        {
            try
            {
                SqlConnection connection = new SqlConnection(strConnection);
                connection.Open();
                return connection;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public SqlCommand CreateCommand(string Command, CommandType CommandType)
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

    }
}
