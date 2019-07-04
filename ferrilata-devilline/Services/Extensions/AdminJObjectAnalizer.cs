using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace ferrilata_devilline.Services.Extensions
{
    public static class AdminJObjectAnalizer
    {
        public static bool HasMissingFieldsOrValuesAsAdmin(this JObject data)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema adminDTOSchema = generator.Generate(typeof(AdminDTO));

            return !data.IsValid(adminDTOSchema);
        }
    }
}
