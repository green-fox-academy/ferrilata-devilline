using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ferrilata_devilline.Services.Extensions
{
    public static class AdminJObjectAnalizer
    {
        public static bool HasMissingFieldsAsAdmin(this JObject data)
        {
            return ((data["version"] == null) ||
                    (data["name"] == null) ||
                    (data["tag"] == null) ||
                    (data["levels"] == null));
        }

        public static bool HasNullValuesAsAdmin(this JObject data)
        {
            AdminDTO admin = data.ToObject<AdminDTO>();

            return ((admin.version == null) ||
                    (admin.name == null) ||
                    (admin.tag == null) ||
                    (admin.levels == null));
        }
    }
}
