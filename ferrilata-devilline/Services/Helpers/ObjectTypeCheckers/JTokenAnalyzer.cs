using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;
using ferrilata_devilline.Models.DAOs.JsonHelper;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Services.Helpers.Extensions.ObjectTypeCheckers.ObjectInputMakers;

namespace ferrilata_devilline.Services.Helpers.ObjectTypeCheckers
{
    public class JTokenAnalyzer
    {
        private readonly JsonSchemaService _service;
        private readonly string _badgeInDTOName;
        private readonly string _pitchInDTOName;
        private readonly string _pitchDTOName;

        public JTokenAnalyzer(JsonSchemaService service)
        {
            _service = service;
            _badgeInDTOName = (typeof(BadgeInDTO)).ToString();
            _pitchInDTOName = (typeof(PitchInDTO)).ToString();
            _pitchDTOName = (typeof(PitchDTO)).ToString();
        }

        public bool FindsMissingFieldsOrValuesIn(JToken requestBody, string className)
        {
                var correctBadgeInDTOJToken = CreateCorrectJTokenOfType(className);

                if (IsCorrect(correctBadgeInDTOJToken, className))
                {
                    return IsInCorrect(requestBody, className);
                } 

                UpdateSchemaFor(className, typeof(BadgeInDTO));

                return IsInCorrect(requestBody, className);
        }

        private JToken CreateCorrectJTokenOfType(string classname)
        {
            if (classname.Equals(_badgeInDTOName))
            {
                var correctBadgeInDTO = BadgeInputMaker.MakeCorrect();
                return JToken.FromObject(correctBadgeInDTO);
            }

            if (classname.Equals(_pitchInDTOName))
            {
                var correctPitchInDTO = PitchInputMaker.MakeCorrectPitchInDTO();
                return JToken.FromObject(correctPitchInDTO);
            }

            var correctPitchDTO = PitchInputMaker.MakeCorrectPitchDTO();
            return JToken.FromObject(correctPitchDTO);
        }

        private bool IsInCorrect(JToken requestBody, string className)
        {
            string schemaString = _service.GetSchemaFor(className);
            JSchema schema = JSchema.Parse(schemaString);
            return !requestBody.IsValid(schema);
        }

        private bool IsCorrect(JToken requestBody, string className)
        {
            return !IsInCorrect(requestBody, className);
        }

        private void UpdateSchemaFor(string className, Type type)
        {
            var schemaGenerator = new JSchemaGenerator();
            var schema = schemaGenerator.Generate(type);
            _service.Save(new JsonSchemaForDevilline { Class = className, Schema = schema.ToString() });
        }

        public bool ConsidersValid(JToken requestBody, string className)
        {
            return !FindsMissingFieldsOrValuesIn(requestBody, className);
        }
    }
}