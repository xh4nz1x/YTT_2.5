using Npgsql;

namespace YTT_5_1;

public class DBService
{
    private static NpgsqlConnection? _connection;
    
    private static string GetConnectionString()
    {
        return @"Host=46.17.97.92;Port=5432;Database=h4nz1;Username=h4nz1;Password=password";
    }
    
    public static NpgsqlConnection GetSqlConnection()
    {
        if (_connection is null)
        {
            _connection = new NpgsqlConnection(GetConnectionString());
            _connection.Open();
        }
        
        return _connection;
    }
}
