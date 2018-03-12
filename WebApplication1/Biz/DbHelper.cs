using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using LiteDB;
using WebApplication1.Models;

namespace WebApplication1.Biz
{
    public static class ext
    {
        public static string GetMemberName<TSource, TKey>(Expression<Func<TSource, TKey>> keySelector)
        {
            string fieldName = null;
            var exp = keySelector.Body as UnaryExpression;
            if (exp == null)
            {
                var body = keySelector.Body as MemberExpression;
                fieldName = body.Member.Name;
            }
            else
            {
                fieldName = (exp.Operand as MemberExpression).Member.Name;
            }
            return fieldName;
        }
        public static IEnumerable<T> OrderBy2<T>(this IEnumerable<T> source, Expression<Func<T, dynamic>> keySelector) where T : class
        {
            Type type = typeof(T);
            var propertyName = GetMemberName(keySelector);
            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException("propertyName", "Not Exist");

            ParameterExpression param = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

            string methodName = 1==0 ? "OrderBy" : "OrderByDescending";

            var source2 = source.AsQueryable();
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source2.Expression, Expression.Quote(orderByExpression));

            return source2.Provider.CreateQuery<T>(resultExp);
        }
    }

    public class DbHelper
    {
        private const string DbName = @"MyData.db";


        public static void SetValue(string collectionName,dynamic id, string key, object value)
        {
            using (var db = new LiteDatabase(DbName))
            {
                var collection = db.GetCollection(collectionName.ToLower());
                long.TryParse(id.ToString(), out long _id);
                var k = new BsonValue(_id);
                var v= collection.FindById(k);
                BsonValue v1 = v[key];
                dynamic v3 = default(dynamic);
                switch (v1.Type)
                {
                    case BsonType.Null:
                        v3 = Convert.ToString(value);
                        break;
                    case BsonType.Int32:
                        v3 = Convert.ToInt32(value);
                        break;
                    case BsonType.Int64:
                        v3 = Convert.ToInt64(value);
                        break;
                    case BsonType.Double:
                        v3 = Convert.ToDouble(value);
                        break;
                    case BsonType.Decimal:
                        v3 = Convert.ToDecimal(value);
                        break;
                    case BsonType.String:
                        v3 = Convert.ToString(value);
                        break;
                    case BsonType.Array:
                        break;
                    case BsonType.Boolean:
                        v3 = Convert.ToBoolean(value);
                        break;
                    case BsonType.DateTime:
                        v3 = Convert.ToDateTime(value);
                        break;
                }
                v[key] = new BsonValue(v3);
                collection.Update(v);
            }
        }
        public static void Add<T>(T t)
        {
            using (var db = new LiteDatabase(DbName))
            {
                var collection = db.GetCollection<T>(typeof(T).Name.ToLower());
                collection.Insert(t);
            }
        }

        public static IEnumerable<T> Get<T>(Expression<Func<T, bool>> express)
        {
            Func<T, bool> lamada = express.Compile();
            using (var db = new LiteDatabase(DbName))
            {
                var collection = db.GetCollection<T>(typeof(T).Name.ToLower());
               var ret= collection.Find(express);
                return ret;
            }
        }

      
        public static IEnumerable<T> GetAll<T>()
        {
        

            using (var db = new LiteDatabase(DbName))
            {
                var collection = db.GetCollection<T>(typeof(T).Name.ToLower());
                var ret = collection.FindAll();

                return ret;

            }
        }
        public static T GetFirst<T>(Expression<Func<T, bool>> express)
        {
            Func<T, bool> lamada = express.Compile();
            using (var db = new LiteDatabase(DbName))
            {
                var collection = db.GetCollection<T>(typeof(T).Name.ToLower());
                var ret = collection.FindOne(express);
                return ret;
            }
        }
        public static void Update<T>(T t)
        {
          
            using (var db = new LiteDatabase(DbName))
            {
                var collection = db.GetCollection<T>(typeof(T).Name.ToLower());
                var ret = collection.Update(t);
              
            }
        }
        public static void Remove<T>(Expression<Func<T, bool>> express)
        {
           
            using (var db = new LiteDatabase(DbName))
            {
                var collection = db.GetCollection<T>(typeof(T).Name.ToLower());
                 collection.Delete(express);
              
            }
        }
    }
}
