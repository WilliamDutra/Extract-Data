using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractData.Domain.Constantes
{
    public static class MysqlServer
    {
        public static string ShowDatabases => SHOW_DATABASES;

        public static string ShowTables => SHOW_TABLES;

        private static string SHOW_DATABASES = "show databases;";

        private static string SHOW_TABLES = "show tables;";

    }
}
