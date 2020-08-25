using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataBase.Model
{
    public abstract class DbBase
    {
        /// <summary>
        /// connection string
        /// </summary>
        protected string connection => "Server=127.0.0.1; Port=5432; User Id=postgres; Password=greenday; Database=MessageDb;";
        /// <summary>
        /// add new message
        /// </summary>
        /// <returns></returns>
        public abstract int InsertRow(DbRecord record);
        /// <summary>
        /// update mesasge
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public abstract bool UpdateRow(int id, string text);
    }
}
