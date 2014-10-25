using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MH.Common.DB
{
    /// <summary>
    /// Context基类
    /// </summary>
    public class BaseDbContext:DbContext
    {
        public BaseDbContext(string connectionString)
            : base(connectionString)
        {
            this.Database.Connection.ConnectionString = connectionString;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.UseDatabaseNullSemantics = false;
            this.Configuration.ValidateOnSaveEnabled = false;
        }
    }
}
