using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataBase.Model
{
    public class DbAccess : DbBase
    {
        /// <summary>
        /// get messages from database
        /// </summary>
        /// <returns></returns>
        public List<DbRecord> GetRecords()
        {
            List<DbRecord> records = new List<DbRecord>();
            NpgsqlConnection con = new NpgsqlConnection(connection);
            try
            {
                con.Open();
                var rdr = new NpgsqlCommand("SELECT Messages.Id, Messages.Message,LogMessages.CurrentTime from Messages JOIN LogMessages ON Messages.Id = LogMessages.MesasgeId;", con).ExecuteReader();
                //     var t = con.Query<DbRecord>("SELECT Messages.Id, Messages.Message,LogMessages.CurrentTime from Messages JOIN LogMessages ON Messages.Id = LogMessages.MesasgeId").ToList();
                while (rdr.Read())
                {
                    records.Add(new DbRecord(Convert.ToInt32(rdr[0].ToString()), rdr[1].ToString(), Convert.ToDateTime(rdr[2].ToString())));
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                con = null;
            }
            return records;
        }

        public override int InsertRow(DbRecord record)
        {
            NpgsqlConnection con = null;
            try
            {
                con = new NpgsqlConnection(connection);
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO messages(Message) VALUES(@Message)", con);
                cmd.Parameters.AddWithValue("Message", record.Message);
                cmd.ExecuteNonQuery();
            }
            catch (NpgsqlException)
            {
                return -1;
            }
            finally
            {
                if (con != null)
                    con.Dispose();
            }
            return 0;

        }

        public override bool UpdateRow(int id, string text)
        {
            NpgsqlConnection con = null;
            try
            {
                con = new NpgsqlConnection(connection);
                con.Open();
                NpgsqlCommand cmd = new NpgsqlCommand("UPDATE messages SET Message=@Message WHERE Id =@Id", con);
                cmd.Parameters.AddWithValue("Id", id);
                cmd.Parameters.AddWithValue("Message", text);
                cmd.ExecuteNonQuery();
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
