using MySql.Data.MySqlClient;

public class DatabaseConnection
{
    private static DatabaseConnection _instance;
    private readonly string _connectionString;
    private MySqlConnection _connection;

    private DatabaseConnection()
    {
        _connectionString = "Server=localhost;Database=comment_db;User=root;Password=123456;";
        _connection = new MySqlConnection(_connectionString);
    }

    public static DatabaseConnection GetInstance()
    {
        if (_instance == null)
        {
            _instance = new DatabaseConnection();
        }
        return _instance;
    }

    public MySqlConnection GetConnection()
    {
        return _connection;
    }
}
