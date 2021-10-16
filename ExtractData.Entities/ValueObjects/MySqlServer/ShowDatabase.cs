using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractData.Entities.ValueObjects.MySqlServer
{
    public class ShowDatabase
    {
        public string Database { get; set; }

        public string Information_Schema { get; set; }

    }
}
