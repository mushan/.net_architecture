using System;
using System.ComponentModel;
using System.Linq.Expressions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MH.Common.Model
{
    /// <summary>
    /// 查询对象
    /// </summary>
    public class Query<TEntity> where TEntity :BaseModel
    {
        /// <summary>
        /// 查询条件
        /// </summary>
        public Expression<Func<TEntity, bool>> Where { get; set; }

        /// <summary>
        /// 包含的子对象
        /// </summary>
        public string Include { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set;}

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 排序关键字
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 排序枚举
        /// </summary>
        public OrderDirection OrderDirection { get; set; }

    }

    public enum OrderDirection
    {
         [Description("顺序")]
         Asc=0,

         [Description("倒序")]
         Desc=1
    }
}
