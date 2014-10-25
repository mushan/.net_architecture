using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MH.Common.Model;

namespace MH.Common.DB
{
    /// <summary>
    /// DataOperation接口
    /// </summary>
    public interface IDo<TEntity> where TEntity:BaseModel
    {
        bool Create(TEntity entity);

        bool Create(params TEntity[] entities);

        bool Update(TEntity entity);

        bool Update(params TEntity[] entities);

        TEntity Get(long ID);

        int GetTotal(Expression<Func<TEntity, bool>> where);

        IList<TEntity> GetByIDs(long[] ids);

        IList<TEntity> GetAll();

        IList<TEntity> Get(Expression<Func<TEntity, bool>> where);

        IList<TEntity> Get(Query<TEntity> q);

        bool Remove(TEntity entity);

        bool Remove(TEntity[] entities);

        bool Remove(long ID);

        bool Remove(long[] IDs);
    }
}
