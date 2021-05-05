using DaiPhatDat.Core.Kernel.Application.Utilities;
using System.Data.Entity;

namespace DaiPhatDat.Core.Kernel
{
    public class Context : DbContext, IContext
    {
        public Context(string db = CommonUtility.ConnectionSQLString) : base(db) { }
        public Context(string db, string schema) : base(db) { }
    }
}
