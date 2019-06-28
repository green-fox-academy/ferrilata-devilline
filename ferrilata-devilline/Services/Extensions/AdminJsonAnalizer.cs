using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services.Extensions
{
    public static class AdminJsonAnalizer
    {
        private class ExpectedFormat
        {
            [JsonProperty(Required = Required.Always)]
            string version { set; get; }

            [JsonProperty(Required = Required.Always)]
            string name { set; get; }

            [JsonProperty(Required = Required.Always)]
            string tag { set; get; }

            [JsonProperty(Required = Required.Always)]
            List<object> levels { set; get; }

            public ExpectedFormat()
            {
            }
        }

        public static bool HasMissingFieldsAsAdmin(this JObject data)
        {
            try
            {
                data.ToObject<ExpectedFormat>();
                return false;
            }
            catch
            {
                return true;
            }
        }

        public static bool HasNullValuesAsAdmin(this JObject data)
        {
            var listOfNullValues = ((IEnumerable<KeyValuePair<string, JToken>>)data)
                .Where(x => x.Value == null)
                .ToList();

            return listOfNullValues.Count > 0;
        }
    }
}
