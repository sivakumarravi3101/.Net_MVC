using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using System.Reflection;

namespace WebApplication1.Core.Helpers
{
    public class DbInstaller
    {
        private readonly EfDbContext _context;

        public DbInstaller(EfDbContext context)
        {
            _context = context;
        }

        public void EnsureSchemaVersionTable()
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS schema_version (
                    Id SERIAL PRIMARY KEY,
                    ScriptName VARCHAR(255) NOT NULL,
                    Applied TIMESTAMP NOT NULL
                )";

            _context.Database.ExecuteSqlRaw(sql);
        }

        public void Install()
        {
            EnsureSchemaVersionTable();

            var executedScripts = _context.SchemaVersions.ToList();

            string appDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            string schemaPath = Path.Combine(appDir, "sql", "schema");

            var files = new DirectoryInfo(schemaPath)
                .GetFiles("*.sql")
                .OrderBy(f => f.Name);

            foreach (var file in files)
            {
                if (executedScripts.Any(e => e.ScriptName == file.Name))
                    continue;

                var sql = File.ReadAllText(file.FullName);
                _context.Database.ExecuteSqlRaw(sql);

                _context.SchemaVersions.Add(new SchemaVersion
                {
                    ScriptName = file.Name,
                    Applied = DateTime.UtcNow
                });

                _context.SaveChanges();
            }
        }
    }
}
