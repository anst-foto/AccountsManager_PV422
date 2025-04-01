using System.Data;
using AccountsManager.Models;
using Npgsql;

namespace AccountsManager.Core;

/// <summary>
/// Сервис для работы с базой данных.
/// </summary>
public class Service
{
    private readonly string _connectionString;
    
    /// <summary>
    /// Конструктор класса.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных</param>
    public Service(string connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString)) 
            throw new ArgumentNullException(nameof(connectionString));
        
        _connectionString = connectionString;
    }

    /// <summary>
    /// Добавляет аккаунт в таблицу.
    /// </summary>
    /// <param name="account">Аккаунт</param>
    /// <returns>Результат выполнения операции (true - успех, false - неудача)</returns>
    public bool AddAccount(Account account)
    {
        using var db = new NpgsqlConnection(_connectionString);
        db.Open();
        const string SQL = """
                           INSERT INTO table_accounts(last_name, first_name, login, password) 
                           VALUES(@LastName, @FirstName, @Login, @Password)
                           """;
        var command = new NpgsqlCommand(SQL, db);
        command.Parameters.AddWithValue("@LastName", account.LastName);
        command.Parameters.AddWithValue("@FirstName", account.FirstName);
        command.Parameters.AddWithValue("@Login", account.Login);
        command.Parameters.AddWithValue("@Password", account.Password);
        
        var result = command.ExecuteNonQuery();
        db.Close();
        
        return result > 0;
    }

    /// <summary>
    /// Получает все аккаунты из таблицы.
    /// </summary>
    /// <returns>Список аккаунтов</returns>
    /// <exception cref="EmptyTableException">Получено пустое значение</exception>
    public IEnumerable<Account> GetAllAccounts()
    {
        using var db = new NpgsqlConnection(_connectionString);
        db.Open();
        const string SQL = "SELECT id, last_name, first_name, login, password FROM table_accounts";
        var command = new NpgsqlCommand(SQL, db);
        var reader = command.ExecuteReader();
        
        if (!reader.HasRows) throw new EmptyTableException();

        var accounts = new List<Account>();
        while (reader.Read())
        {
            accounts.Add(new Account()
            {
                Id = reader.GetInt32("id"),
                LastName = reader.GetString("last_name"),
                FirstName = reader.GetString("first_name"),
                Login = reader.GetString("login"),
                Password = reader.GetString("password")
            });
        }
        
        db.Close();
        
        return accounts;
    }
}