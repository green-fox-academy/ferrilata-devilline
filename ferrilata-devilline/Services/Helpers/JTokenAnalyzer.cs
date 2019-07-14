using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace ferrilata_devilline.Services.Helpers
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