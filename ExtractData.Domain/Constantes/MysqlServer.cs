using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractData.Domain.Constantes
{
    public static class MySqlServer
    {
        public static string ShowDatabases => SHOW_DATABASES;

        public static string ShowTables => SHOW_TABLES;

        public static string ShowColumns => SHOW_COLUMNS;
           
        private static string SHOW_DATABASES = "SHOW DATABASES;";

        private static string SHOW_TABLES = "SELECT TABLE_NAME AS 'TABLE' FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{0}';";

        private static string SHOW_COLUMNS = "SHOW COLUMNS FROM {0} IN {1};";

    }
}
