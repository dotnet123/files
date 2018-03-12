using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebApplication1.Pages.Part
{
    public class DBModel : PageModelBase
    {
        public Dictionary<string, IList<Tuple<string, object>>> collection = new Dictionary<string, IList<Tuple<string, object>>>();
        public void OnGet()
        {
            //checklogin();
            dbshow();
        }

        public void dbshow()
        {
            using (var db = new LiteDatabase(@"MyData.db"))
            {
               var names= db.GetCollectionNames();
               
                foreach (var name in names)
                {
                   var kk= db.GetCollection(name);
                    var kvLst = new List<Tuple<string, object>>();
                    IEnumerable<BsonDocument> all = kk.FindAll();
                    foreach (var a in all)
                    {
                       var keys= a.Keys;
                        foreach (var k in keys)
                        {
                            BsonValue v = a[k];
                            var v2 = v.RawValue;
                            if(v.IsArray)  continue;
                            kvLst.Add(new Tuple<string, object>(k,v2));
                        }
                    }
                    collection.Add(name, kvLst);
                   
                }
               // var tt = JsonConvert.SerializeObject(dict);
            }
        }
    }
}