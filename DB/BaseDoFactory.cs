using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MH.Common.Model;

namespace MH.Common.DB
{
    public abstract class BaseDoFactory<TContext>
        where TContext : BaseDbContext, new()
    {
        private Dictionary<string,Object> dic = new Dictionary<string, object>();

        public DefaultDo<T, TContext> GetDo<T>() where T : BaseModel
        {
            var key = typeof (T).Name;
            if (dic.ContainsKey(key))
            {
                Object db;
                dic.TryGetValue(key, out db);
                return (DefaultDo<T,TContext>)db;
            }
            else
            {
                var tmp = CreateDo<T>();
                dic.Add(key, tmp);
                return tmp ;
            }
        }

        protected virtual DefaultDo<T,TContext> CreateDo<T>() where T : BaseModel
        {
            return new DefaultDo<T, TContext>();
        }
    }
}