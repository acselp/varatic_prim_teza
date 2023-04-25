using System.Reflection;
using EvolveDb;
using Microsoft.Data.SqlClient;
using Npgsql;
using VaraticPrim.Service.Interfaces;

namespace VaraticPrim.Service.Migrations;

public class MigrationService:IMigrationService
{
    private readonly Evolve _evolve;
    
    public MigrationService(string connectionString)
    {
        var cnx = new NpgsqlConnection(connectionString);
        var location = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

        _evolve = new Evolve(cnx)
        {
            Locations = new[]{location},
            IsEraseDisabled = true,
        };

        _evolve.Placeholders = new Dictionary<string, string>
        {
            ["${database}"] = "varatic_prim",
            ["${schema1}"] = "public"
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