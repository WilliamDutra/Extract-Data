using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractData.Domain.Constantes
{
    public static class SqlServer
    {
        public static string ShowDatabases => SHOW_DATABASES;

        public static string ShowTables => SHOW_TABLES;


        private static string SHOW_DATABASES = "SELECT name AS 'Database' FROM master.dbo.sysdatabases";

        private static string SHOW_TABLES = "SELECT TABLE_NAME AS 'Table' FROM {0}.INFORMATION_SCHEMA.TABLES";

    }
}
