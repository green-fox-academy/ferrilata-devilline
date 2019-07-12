using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.Input;
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
            JSchema adminDTOSchema = generator.Generate(typeof(AdminDTO));

            return !requestBody.IsValid(adminDTOSchema);
        }

        public static bool HasMissingFieldsOrValuesAsPitchInDTO(this JToken requestBody)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema pitchInDTOSchema = generator.Generate(typeof(PitchInDTO));

            return !requestBody.IsValid(pitchInDTOSchema);
        }

        public static bool HasMissingFieldsOrValuesAsPitchDTO(this JToken requestBody)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema pitchInToUpdateDTOSchema = generator.Generate(typeof(PitchDTO));

            return !requestBody.IsValid(pitchInToUpdateDTOSchema);
        }
    }
}