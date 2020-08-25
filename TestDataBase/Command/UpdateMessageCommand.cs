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
        DbAccess dbAccess;
        int id = 0;
        string message = String.Empty;

        public UpdateMessageCommand(DbAccess db, int searchParam, string newMessage)
        {
            dbAccess = db;
            id = searchParam;
            message = newMessage;
        }
        public override bool Execute()
        {
            if (dbAccess != null)
                return dbAccess.UpdateRow(id, message);
            return false;
        }
    }
}
