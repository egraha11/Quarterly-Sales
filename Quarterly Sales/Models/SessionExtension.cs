using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Quarterly_Sales.Models
{
    public static class SessionExtension
    {

        public static void SetObject<T>(this ISession session, T value, string key)
        {

            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            if(string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
        }
    }
}
