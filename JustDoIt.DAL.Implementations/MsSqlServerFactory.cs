using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace JustDoIt.DAL.Implementations;

public class MsSqlServerFactory
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public MsSqlServerFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("DefaultConnectionString");
    }

    public IDbConnection CreateConnection()
    {
        return new SqlConnection(_connectionString);
    }
}