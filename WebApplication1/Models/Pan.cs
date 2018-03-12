using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Biz;

namespace WebApplication1.Models
{
    public class Pan
    {
        public long Id { get; set; }


        [LiteDB.BsonIgnore]
        public string UserIdX
        {
            get
            {
                if (this.UserId <= 0) return "";
                var userLst = DbHelper.GetFirst<User>(P => P.Id == this.UserId);

                return userLst?.Name??"";
            }
        }

        public long UserId { get; set; }

        public long ParentId { get; set; }


        public long[] Roles1 { get; set; }
        public long[] Roles2 { get; set; }
        public long[] Roles3 { get; set; }
        [LiteDB.BsonIgnore]
        public string Roles11
        { get
         {
            if (this.Roles1 == null) return "";
               var userLst = DbHelper.GetAll<User>().OrderByDescending(p => p.SeqNo).ToList();
                var names = new List<string>();
                foreach (var r in this.Roles1)
                {
                    var n = userLst.FirstOrDefault(p => p.Id == r);
                    if (n!=null) { names.Add(n.Name); }
                }
                return string.Join(",", names);
            }
        }
        [LiteDB.BsonIgnore]
        public string Roles22
        {
            get
            {
                if (this.Roles2 == null) return "";
                var userLst = DbHelper.GetAll<User>().OrderByDescending(p => p.SeqNo).ToList();
                var names = new List<string>();
                foreach (var r in this.Roles2)
                {
                    var n = userLst.FirstOrDefault(p => p.Id == r);
                    if (n != null) { names.Add(n.Name); }
                }
                return string.Join(",", names);
            }
        }
        [LiteDB.BsonIgnore]
        public string Roles33
        {
            get
            {
                if (this.Roles3 == null) return "";
                var userLst = DbHelper.GetAll<User>().OrderByDescending(p => p.SeqNo).ToList();
                var names = new List<string>();
                foreach (var r in this.Roles3)
                {
                    var n = userLst.FirstOrDefault(p => p.Id == r);
                    if (n != null) { names.Add(n.Name); }
                }
                return string.Join(",", names);
            }
        }
        [LiteDB.BsonField("name")]
        public string Name { get; set; }

        public string Url { get; set; }
        [LiteDB.BsonField("seqno")]
        public int SeqNo { get; set; }
      
        [LiteDB.BsonField("status")]
        public int Status { get; set; }

       
        public string Ext { get; set; }

        public long Size { get; set; }
        public DateTime LastTime { get; set; }
    }
}
