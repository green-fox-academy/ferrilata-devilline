using ferrilata_devilline.Models.DTOs;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace ferrilata_devilline.Services.Extensions
{
    public static class JTokenAnalyzer
    {
        public static bool HasMissingFieldsOrValuesAsAdmin(this JToken requestBody)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema adminDTOSchema = generator.Generate(typeof(BadgeInDTO));

            return !requestBody.IsValid(adminDTOSchema);
        }
    }
}
