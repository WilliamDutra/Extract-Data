using ExtractData.Entities.ValueObjects.MySqlServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExtractData.Domain.Interfaces
{
    public interface IMySql
    {
        List<ShowDatabase> ShowDatabase();

        
    }
}
