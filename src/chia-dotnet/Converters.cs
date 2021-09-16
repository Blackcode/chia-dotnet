﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace chia.dotnet
{
    internal static class Converters
    {
        public static T? ToObject<T>(JObject? j, string childItem)
        {
            if (j is not null)
            {
                var token = string.IsNullOrEmpty(childItem) ? j : j.GetValue(childItem);

                return ToObject<T>(token);
            }

            return default;
        }

        public static T? ToObject<T>(JToken? token)
        {
            if (token is not null)
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                };

                return token.ToObject<T>(JsonSerializer.Create(serializerSettings));
            }

            return default;
        }

        public static T? ToObject<T>(this string json)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            return JsonConvert.DeserializeObject<T>(json, serializerSettings);
        }

        public static IEnumerable<T> ConvertList<T>(dynamic enumerable)
        {
            Debug.Assert(enumerable is not null);

            return ((IEnumerable<dynamic>)enumerable).Select(item => (T)item);
        }

        public static DateTime? ToDateTime(this ulong? epoch)
        {
            if (epoch.HasValue)
            {
                var start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); //from start epoch time
                return start.AddSeconds(epoch.Value); //add the seconds to the start DateTime
            }
            return null;
        }

        public static DateTime ToDateTime(this ulong epoch)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); //from start epoch time
            return start.AddSeconds(epoch); //add the seconds to the start DateTime
        }

        public static DateTime ToDateTime(this double epoch)
        {
            var start = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc); //from start epoch time
            return start.AddSeconds(epoch); //add the seconds to the start DateTime
        }

        public static string ToJson(this object o)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new SnakeCaseNamingStrategy()
                }
            };
            return JsonConvert.SerializeObject(o, serializerSettings);
        }
    }
}
