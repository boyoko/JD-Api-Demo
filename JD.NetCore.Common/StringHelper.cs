using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace JD.NetCore.Common
{
    public class StringHelper
    {
        public static string ToJson<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }

        public static T JsonToObj<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}
