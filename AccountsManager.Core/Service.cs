using System.Data;
using AccountsManager.Models;
using Npgsql;

namespace AccountsManager.Core;

public class Service
{
    private const string CONNECTION_STRING = "Server=127.0.0.1;Port=5432;Database=accounts_db;User Id=postgres;Password=1234;Search Path=test;";
    
    public bool AddAccount(Account account)
    {
        using var db = new NpgsqlConnection(CONNECTION_STRING);
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

    public IEnumerable<Account> GetAllAccounts()
    {
        using var db = new NpgsqlConnection(CONNECTION_STRING);
        db.Open();
        const string SQL = "SELECT id, last_name, first_name, login, password FROM table_accounts";
        var command = new NpgsqlCommand(SQL, db);
        var reader = command.ExecuteReader();
        
        if (!reader.HasRows) throw new Exception("Пустая таблица");

        /*var accounts = new List<Account>();
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
        return accounts;*/

        while (reader.Read())
        {
            yield return new Account()
            {
                Id = reader.GetInt32("id"),
                LastName = reader.GetString("last_name"),
                FirstName = reader.GetString("first_name"),
                Login = reader.GetString("login"),
                Password = reader.GetString("password")
            };
        }
    }
}