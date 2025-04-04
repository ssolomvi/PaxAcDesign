using Microsoft.Data.Sqlite;

namespace PaxAcDesign.Data;

public class ConnectionProvider
{

    private static ConnectionProvider? _connectionProvider;

    private SqliteConnection _connection;

    private ConnectionProvider(String connectionDatasource)
    {
        _connection = new SqliteConnection("" + new SqliteConnectionStringBuilder
        {
            Mode = SqliteOpenMode.Memory,
            Cache = SqliteCacheMode.Shared,
            DataSource = connectionDatasource
            // DataSource = Path.Combine(Directory.GetCurrentDirectory(), "calculate", "Data", "pax-ac-design.db")
            // "~/calculate/Data/pax-ac-design.db"
        });
        
        _connection.Open();
    }

    public static ConnectionProvider create(String connectionDatasource)
    {
        if (_connectionProvider == null)
        {
            _connectionProvider = new ConnectionProvider(connectionDatasource);
        }

        return _connectionProvider;
    }

    public void Deconstruct()
    {
        _connection.Close();
    }

    public SqliteConnection getConnection()
    {
        return _connection;
    }

    public void fun()
    {
        using (var connection = new SqliteConnection("" +
                                                     new SqliteConnectionStringBuilder
                                                     {
                                                         DataSource = "hello.db"
                                                     }))
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                var insertCommand = connection.CreateCommand();
                insertCommand.Transaction = transaction;
                insertCommand.CommandText = "INSERT INTO message ( text ) VALUES ( $text )";
                insertCommand.Parameters.AddWithValue("$text", "Hello, World!");
                insertCommand.ExecuteNonQuery();

                var selectCommand = connection.CreateCommand();
                selectCommand.Transaction = transaction;
                selectCommand.CommandText = "SELECT text FROM message";
                using (var reader = selectCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = reader.GetString(0);
                        Console.WriteLine(message);
                    }
                }

                transaction.Commit();
            }
        }
    }
}