using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDataBase.Model;

namespace TestDataBase.Command
{
    public class UpdateMessageCommand : DbCommand<bool>
    {
        DbBase dbAccess;
        DbRecord rec = null;

        public UpdateMessageCommand(DbBase db, DbRecord record)
        {
            dbAccess = db;
            rec = record;
        }
        public override bool Execute()
        {
            if (dbAccess != null)
                return dbAccess.UpdateRow(rec);
            return false;
        }
    }
}
