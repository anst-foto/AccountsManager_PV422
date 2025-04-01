using AccountsManager.Models;
using Dapper;
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
        
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
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
        
        var result = db.Execute(SQL, new
        {
            LastName = account.LastName, 
            FirstName = account.FirstName, 
            Login = account.Login, 
            Password = account.Password
        });
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
        var accounts = db.Query<Account>(SQL);
        
        db.Close();
        
        return accounts;
    }
}