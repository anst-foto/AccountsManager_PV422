using AccountsManager.Models;
using Npgsql;

namespace AccountsManager.Core;

public class Service
{
    private const string CONNECTION_STRING = "Server=127.0.0.1;Port=5432;Database=accounts_db;User Id=postgres;Password=1234;";
    
    public bool AddAccount(Account account)
    {
        using var db = new NpgsqlConnection(CONNECTION_STRING);
        db.Open();
        var sql = "INSERT INTO table_accounts(last_name, first_name, login, password) VALUES(@LastName, @FirstName, @Login, @Password)";
        var command = new NpgsqlCommand(sql, db);
        command.Parameters.AddWithValue("@LastName", account.LastName);
        command.Parameters.AddWithValue("@FirstName", account.FirstName);
        command.Parameters.AddWithValue("@Login", account.Login);
        command.Parameters.AddWithValue("@Password", account.Password);
        
        var result = command.ExecuteNonQuery();
        db.Close();
        
        return result > 0;
    }
}