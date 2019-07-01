using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json.Linq;

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

            return ((admin.Version == null) ||
                    (admin.Name == null) ||
                    (admin.Tag == null) ||
                    (admin.Levels == null));
        }
    }
}
