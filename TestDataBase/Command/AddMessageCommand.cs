using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataBase.Model;

namespace TestDataBase.Command
{
    public class AddMessageCommand : DbCommand<int>
    {
        DbAccess dbAccess;
        DbRecord record = null;
        public AddMessageCommand(DbAccess db, DbRecord rc)
        {
            dbAccess = db;
            record = rc;
        }
        public override int Execute()
        {
            if (dbAccess != null)
                return dbAccess.InsertRow(record);
            return -1;
        }
    }
}
