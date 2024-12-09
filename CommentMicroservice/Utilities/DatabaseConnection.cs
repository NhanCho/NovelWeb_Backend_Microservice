using MySql.Data.MySqlClient;

public class DatabaseConnection
{
    private static DatabaseConnection _instance;
    private readonly string _connectionString;
    private MySqlConnection _connection;

    private DatabaseConnection()
    {
<<<<<<< HEAD
        _connectionString = "Server=localhost;Database=comment_db;User=root;Password=1111;";
=======
        _connectionString = "Server=localhost;Database=comment_db;User=root;Password=NHAN2003@p;";
>>>>>>> 6e6c5584370c7fe913c0f68da3ae731bc8fb3690
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
