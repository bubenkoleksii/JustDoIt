using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JustDoIt.DAL.Implementations;

public class MsSqlServerConnectionFactory
{
    private readonly string _connectionString;

    public MsSqlServerConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnectionString");
    }

    public IDbConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}