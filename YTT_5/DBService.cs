using Npgsql;

namespace YTT_5_1;

public class DBService
{
    private static NpgsqlConnection? _connection;
    
    private static string GetConnectionString()
    {
        return @"Host=10.30.0.137;Port=5432;Database=gr624_cheaan;Username=gr624_cheaan;Password=Litokop_908";
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