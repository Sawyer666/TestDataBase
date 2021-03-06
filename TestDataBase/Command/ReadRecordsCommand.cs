﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataBase.Model;

namespace TestDataBase.Command
{
    class ReadRecordsCommand : DbCommand<List<DbRecord>>
    {
        DbBase dbAccess;

        public ReadRecordsCommand(DbBase db)
        {
            dbAccess = db;
        }
        public override List<DbRecord> Execute()
        {
            return dbAccess.GetRecords();
        }
    }
}
