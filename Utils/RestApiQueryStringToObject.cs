﻿using Newtonsoft.Json;
using System.Web;

namespace Artaco.Infrastructure.CoreX
{
    public static class RestApiQueryStringToObject<TObject> where TObject : struct
    {
        public static TObject Convert(string QueryString)
        {
            //string QueryString = "BaseNo=5&Width=100";
            var dict = HttpUtility.ParseQueryString(QueryString);
            string json = JsonConvert.SerializeObject(dict.Cast<string>()
                .ToDictionary(k => k, v => dict[v]));
            return JsonConvert.DeserializeObject<TObject>(json);
        }
    }
}