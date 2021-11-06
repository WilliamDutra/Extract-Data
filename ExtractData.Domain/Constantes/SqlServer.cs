using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractData.Domain.Constantes
{
    public static class SqlServer
    {
        public static string ShowDatabases => SHOW_DATABASES;

        public static string ShowTables => SHOW_TABLES;

        public static string ShowColumns => SHOW_COLUMNS;


        private static string SHOW_DATABASES = "SELECT name AS 'Database' FROM master.dbo.sysdatabases";

        private static string SHOW_TABLES = "SELECT TABLE_NAME AS 'Table' FROM {0}.INFORMATION_SCHEMA.TABLES";

        private static string SHOW_COLUMNS = "SELECT C.NAME AS 'Field', TYPE_NAME(C.USER_TYPE_ID) AS 'Type' FROM SYS.COLUMNS C JOIN SYS.TYPES T ON C.USER_TYPE_ID= T.USER_TYPE_ID WHERE C.OBJECT_ID=OBJECT_ID('{0}')";

    }
}
