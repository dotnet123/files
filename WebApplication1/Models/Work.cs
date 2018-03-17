using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    /// <summary>
    /// 工作日志
    /// </summary>
    public class Work
    {
        /// <summary>
        /// 主键
        /// </summary>
        [LiteDB.BsonId]
        public long Id { get; set; }

        /// <summary>
        /// 工作时间
        /// </summary>
        [Required]
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// 工作内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public long CreatedId { get; set; }

        public User Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}