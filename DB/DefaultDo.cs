using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MH.Common.Model;

namespace MH.Common.DB
{
    /// <summary>
    /// DataOperation默认类
    /// </summary>
    public class DefaultDo<TEntity, TContext> : IDo<TEntity>
        where TEntity : BaseModel
        where TContext : BaseDbContext, new()
    {
        private static class Locker<TEntity>
        {
            public static object Mutex = new object();
        }

        private static DefaultDo<TEntity, TContext> _instance = null;

        public static DefaultDo<TEntity, TContext> CreateDb()
        {
            return _instance ?? (_instance = new DefaultDo<TEntity, TContext>());
        }

        public virtual bool Create(TEntity entity)
        {
            return this.Create(new TEntity[] {entity});
        }

        public virtual bool Create(params TEntity[] entities)
        {
            foreach (var t in entities)
            {
                if (t.CreatedTime < DateTime.Parse("1900-01-01"))
                    t.CreatedTime = DateTime.Now;
                if (t.LastModifiedTime < DateTime.Parse("1900-01-01"))
                    t.LastModifiedTime = DateTime.Now;
            }
            using (var ctx = new TContext())
            {    
                foreach (var entity in entities)
                {
                    ctx.Set<TEntity>().Add(entity);
                }
               return ctx.SaveChanges()>0;                  
            }
        }

        public virtual bool Update(TEntity entity)
        {
            return this.Update(new TEntity[] {entity});
        }

        public virtual bool Update(params TEntity[] entities)
        {
            foreach (var t in entities)
            {
                if (t.LastModifiedTime < DateTime.Parse("1900-01-01"))
                {
                    t.LastModifiedTime = DateTime.Now;
                }
            }
            using (var ctx = new TContext())
            {
                foreach (var entity in entities)
                {
                    ctx.Entry(entity).State = EntityState.Modified;
                }
               return ctx.SaveChanges()>0;
            }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        /// <returns></returns>
        public virtual int GetTotal(Expression<Func<TEntity, bool>> where)
        {
            using (var ctx = new TContext())
            {
                return ctx.Set<TEntity>().Where(where).Count();
            }
        }

        public virtual TEntity Get(long ID)
        {
            using (var ctx = new TContext())
            {
                return ctx.Set<TEntity>().Find(ID);
            }
        }

        public virtual IList<TEntity> GetByIDs(long[] ids)
        {
            using (var ctx = new TContext())
            {
                var lst = from t in ctx.Set<TEntity>()
                          where ids.Contains(t.ID)
                          select t;
                return lst.ToList();
            }
        }

        public virtual IList<TEntity> GetAll()
        {
            using (var ctx = new TContext())
            {
                return ctx.Set<TEntity>().ToList();
            }
        }

        public virtual IList<TEntity> Get(Expression<Func<TEntity, bool>> where)
        {
            using (var ctx = new TContext())
            {
                return ctx.Set<TEntity>().Where(where).ToList();
            }
        }

        public virtual IList<TEntity> Get(Query<TEntity> q)
        {
            using (var ctx = new TContext())
            {
                IQueryable<TEntity> query = ctx.Set<TEntity>();
                if (q.Where != null)
                {
                    query = query.Where(q.Where);
                }
                if (q.Include != null)
                {
                    var includes = q.Include.Split(',');
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }               
                }
                if (!string.IsNullOrEmpty(q.Key))
                {
                    query = q.OrderDirection == OrderDirection.Desc
                                ? DynamicQueryable.OrderBy(query, q.Key + "  desc")
                                : DynamicQueryable.OrderBy(query, q.Key + " asc");
                }
                else
                {
                    //默认根据ID顺序排序
                    query = DynamicQueryable.OrderBy(query, "ID"+ " asc");
                }
                q.Count = query.Count();
                if (q.PageIndex > 0 && q.PageSize >= 0)
                {
                    query = query.Skip((q.PageIndex - 1)*q.PageSize).Take(q.PageSize) as IQueryable<TEntity>;
                }
                return query != null ? query.ToList() : new List<TEntity>();
            }
        }

        public virtual bool Remove(TEntity entity)
        {
            return this.Remove(new TEntity[] {entity});
        }

        public virtual bool Remove(TEntity[] entities)
        {
            using (var ctx = new TContext())
            {
                foreach (var entity in entities)
                {
                    ctx.Entry<TEntity>(entity).State = EntityState.Deleted;
                }
                return ctx.SaveChanges()>0;
            }
        }

        public virtual bool Remove(long ID)
        {
            using (var ctx = new TContext())
            {
                var entity = ctx.Set<TEntity>().Find(ID);
                if (entity != null)
                {
                   return this.Remove(entity);
                }
                return false;
            }
        }

        public virtual bool Remove(long[] IDs)
        {
            using (var ctx = new TContext())
            {
                var entities = from t in ctx.Set<TEntity>()
                               where IDs.Contains(t.ID)
                               select t;
                if (entities.Any())
                {
                   return this.Remove(entities.ToArray());
                }
                return false;
            }
        }
    }
}
