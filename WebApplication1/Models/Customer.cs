using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Biz;

namespace WebApplication1.Models
{
    public class Record
    {
        public long customerId { get; set; }
        public int type { get; set; }
        public DateTime date { get; set; }
        public string content { get; set; }


        public long  userid { get; set; }

        [LiteDB.BsonIgnore]
        public User User
        {
            get
            {

                var user = DbHelper.GetFirst<User>(P => P.Id == this.userid);

                return user ?? new User();
            }
        }


    }
    public class Customer
    {
        public long Id { get; set; }
        [LiteDB.BsonField("companyname")]
        public string CompanyName { get; set; }

        [LiteDB.BsonField("address")]
        public string Address { get; set; }

        [LiteDB.BsonField("contacts")]
        public string Contacts { get; set; }


        [LiteDB.BsonField("tel")]
        public string Tel { get; set; }

        [LiteDB.BsonField("contractamount")]
        public decimal ContractAmount { get; set; }


        [LiteDB.BsonField("huikuanstatus")]
        public int HuiKuanStatus { get; set; }


        [LiteDB.BsonField("visitrecordlist")]
        public List<Record> VisitRecordList { get; set; }

        [LiteDB.BsonField("huikuanlist")]
        public List<Record> HuiKuanList { get; set; }


        [LiteDB.BsonField("logtime")]
        public DateTime LogTime { get; set; }

        [LiteDB.BsonField("seqno")]
        public int SeqNo { get; set; }
        [LiteDB.BsonField("status")]
        public int Status { get; set; }


        [LiteDB.BsonField("userid")]
        public long UserId { get; set; }


        [LiteDB.BsonIgnore]
        public User User
        {
            get
            {
                
                var user = DbHelper.GetFirst<User>(P => P.Id == this.UserId);

                return user??new User();
            }
        }





    }
}
