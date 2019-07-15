using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Helpers.Extensions.ObjectTypeCheckers.ObjectInputMakers;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;

namespace ferrilata_devilline.Services.Helpers.ObjectTypeCheckers
{
    public class JTokenAnalyzer
    {
        private readonly JsonSchemaService _service;

        public JTokenAnalyzer(JsonSchemaService service)
        {
            _service = service;
        }

        public bool FindsMissingFieldsOrValuesIn(JToken requestBody, string className)
        {
            string badgeInDTOName = (typeof(BadgeInDTO)).ToString();
            string PitchInDTOName = (typeof(PitchInDTO)).ToString();
            string PitchDTO = (typeof(PitchDTO)).ToString();

   /*         switch (className)
            {
                case badgeInDTOName:
                    var correctBadgeDTO = BadgeInputMaker.MakeCorrect();
                    break;

                
            } */

            string schemaString = _service.GetSchemaFor(className);
            JSchema schema = JSchema.Parse(schemaString);

            return !requestBody.IsValid(schema);
        }

        public bool ConsidersValid(JToken requestBody, string className)
        {
            return !FindsMissingFieldsOrValuesIn(requestBody, className);
        }
    }
}