﻿using ferrilata_devilline.Models;
using ferrilata_devilline.Models.DTOs;
using ferrilata_devilline.Models.DTOs.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using System;

namespace ferrilata_devilline.Services.Extensions
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
    }
}