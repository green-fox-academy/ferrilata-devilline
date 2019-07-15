using ferrilata_devilline.Models.DAOs.JsonHelper;
using ferrilata_devilline.Repositories;
using System.Linq;

namespace ferrilata_devilline.Services
{
    public class JsonSchemaService
    {
        readonly ApplicationContext _context;

        public JsonSchemaService(ApplicationContext context)
        {
            _context = context;
        }

        public void Save(JsonSchema schema)
        {
            _context.JsonSchemas.Add(schema);
            _context.SaveChanges();
        }

        public string GetSchemaFor(string className)
        {
            return _context.JsonSchemas
                .Where(s => s.Class.Equals(className))
                .FirstOrDefault()
                .Schema;
        }
    }
}
