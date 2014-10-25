using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MH.Common.Model
{
    /// <summary>
    /// model的基类，包含公共属性
    /// </summary>
    public class BaseModel 
    {
        public long ID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// 创建人(管理员)
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifiedTime { get; set; }
        /// <summary>
        /// 最后修改人(管理员)
        /// </summary>
        public string LastModifier { get; set; }
    }
}
