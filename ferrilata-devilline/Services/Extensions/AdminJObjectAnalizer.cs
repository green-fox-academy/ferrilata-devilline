using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ferrilata_devilline.Services.Extensions
{
    public static class AdminJObjectAnalizer
    {
        private class ExpectedFormat
        {
            [JsonProperty(Required = Required.Always)]
            string Version;

            [JsonProperty(Required = Required.Always)]
            string Name;

            [JsonProperty(Required = Required.Always)]
            string Tag; 

            [JsonProperty(Required = Required.Always)]
            List<object> Levels; 
        }

        public static bool HasMissingFieldsAsAdmin(this JObject data)
        {
            // return !data.GetType().Name.Equals("ExpectedFormat"); 
            // return !data.GetType().Equals(typeof(ExpectedFormat));
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
            return data.Properties().Any(x => x.Value == null);
        }
    }
}
