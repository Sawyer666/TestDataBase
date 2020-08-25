using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDataBase.Command
{
    public abstract class DbCommand<TResult>
    {
        public abstract TResult Execute();
    }
}
