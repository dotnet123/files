using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using WebApplication1.Biz;

namespace WebApplication1.Models
{
    public class BuMen
    {
        public long Id { get; set; }
        [LiteDB.BsonField("name")]
        public string Name { get; set; }
        [LiteDB.BsonField("seqno")]
        public int SeqNo { get; set; }
        [LiteDB.BsonField("status")]
        public int Status { get; set; }

        [LiteDB.BsonField("remark")]
        public string remark { get; set; }
    }
    public class User
    {
        public long Id { get; set; }

        [LiteDB.BsonField("userid")]
        public string UserId { get; set; }




        [LiteDB.BsonField("pwd")]
        public string PWD { get; set; }
        [LiteDB.BsonField("name")]
        public string Name { get; set; }
        [LiteDB.BsonField("tel")]
        public string Tel { get; set; }
        [LiteDB.BsonIgnore]
        public int SeqNoX { get; set; }

        [LiteDB.BsonField("seqno")]
        public int SeqNo { get; set; }
        [LiteDB.BsonField("bumenid")]
        public long BuMenId { get; set; }
        [LiteDB.BsonField("status")]
        public int Status { get; set; }
    }


    public class Notice {
        public long Id { get; set; }

      
        public string title { get; set; }

    
        public string content { get; set; }
    
        public DateTime ctime { get; set; }

        [LiteDB.BsonField("userid")]
        public long UserId { get; set; }
        [LiteDB.BsonField("seqno")]
        public int SeqNo { get; set; }
     
        [LiteDB.BsonField("status")]
        public int Status { get; set; }
    }
}
