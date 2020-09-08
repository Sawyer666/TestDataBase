using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataBase.Model
{
    public class DbDapperAccess : DbBase
    {
        public override List<DbRecord> GetRecords()
        {
            List<DbRecord> records = new List<DbRecord>();
            NpgsqlConnection con = new NpgsqlConnection(connection);
            try
            {
                con.Open();
                records = con.Query<DbRecord>("SELECT Messages.Id,Messages.Message,LogMessages.CurrentTime from Messages JOIN LogMessages ON Messages.Id=LogMessages.MesasgeId").ToList();
            }
            catch (Exception) { }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return records;
        }

        public override int InsertRow(DbRecord record)
        {
            NpgsqlConnection con = new NpgsqlConnection(connection);
            //    NpgsqlTransaction tranc = con.BeginTransaction();
            try
            {
                con.Open();
                //   for (int r = 0; r < 50000; r++)
                //  {
                con.Execute("INSERT INTO messages(Message) VALUES(@Message)", record);
                //   }
                //   tranc.Commit();
            }
            catch (Exception) { return -1; }
            finally
            {

                con.Dispose();
                //      tranc.Dispose();
            }
            return 0;
        }

        public override bool UpdateRow(DbRecord record)
        {
            NpgsqlConnection con = null;
            try
            {
                con = new NpgsqlConnection(connection);
                con.Open();
                con.Execute("UPDATE messages SET Message=@Message WHERE Id= @Id", record);
            }
            catch (NpgsqlException)
            {
                return false;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return true;
        }
    }
}
