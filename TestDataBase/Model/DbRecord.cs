using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataBase.Model
{
    public class DbRecord
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime CurrentTime { get; set; }

        public bool Visible { get; set; }
        public DbRecord(string message)
        {
            Message = message;
            Visible = false;
        }
        public DbRecord(int id, string message, DateTime currentTime)
        {
            Id = id;
            Message = message;
            CurrentTime = currentTime;
            Visible = true;
        }
    }
}
