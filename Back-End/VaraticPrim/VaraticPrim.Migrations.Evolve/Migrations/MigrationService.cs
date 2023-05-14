using System.Reflection;
using Npgsql;

namespace Infrastructure.Migrations.Evolve.Migrations;

public class MigrationService:IMigrationService
{
    private readonly EvolveDb.Evolve _evolve;

    public MigrationService(string connectionString)
    {
        var cnx = new NpgsqlConnection(connectionString);
        var location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        _evolve = new EvolveDb.Evolve(cnx)
        {
            Locations = new[]{location},
            IsEraseDisabled = true,
            MetadataTableSchema	= "varatic_prim"
        };

        _evolve.Placeholders = new Dictionary<string, string>
        {
            ["${database}"] = "postgres",
            ["${schema}"] = "varatic_prim"
        };
    }

    public void Migrate()
    {
        try
        {
            _evolve.Migrate();
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to run migration", ex);
        }
    }
}