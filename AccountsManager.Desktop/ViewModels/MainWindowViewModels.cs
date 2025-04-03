using System.Collections.ObjectModel;
using AccountsManager.Core;
using AccountsManager.Models;

namespace AccountsManager.Desktop.ViewModels;

public class MainWindowViewModels : ViewModelBase
{
    private int? _id;
    public int? Id
    {
        get => _id;
        set => SetField(ref _id, value);
    }
    
    private string? _firstName;
    public string? FirstName
    {
        get => _firstName;
        set => SetField(ref _firstName, value);
    }
    
    private string? _lastName;
    public string? LastName
    {
        get => _lastName;
        set => SetField(ref _lastName, value);
    }
    
    private string? _login;
    public string? Login
    {
        get => _login;
        set => SetField(ref _login, value);
    }
    
    private string? _password;
    public string? Password
    {
        get => _password;
        set => SetField(ref _password, value);
    }

    public ObservableCollection<Account> Accounts { get; } = [];
    
    private Account? _selectedAccount;
    public Account? SelectedAccount
    {
        get => _selectedAccount;
        set
        {
            if (!SetField(ref _selectedAccount, value)) return;
            
            Id = value?.Id;
            FirstName = value?.FirstName;
            LastName = value?.LastName;
            Login = value?.Login;
            Password = value?.Password;
        }
    }

    public MainWindowViewModels()
    {
        var connectionString = DbConfig.GetConnectionString("db_config.json");
        var service = new Service(connectionString);
        var accounts = service.GetAllAccounts();

        foreach (var account in accounts)
        {
            Accounts.Add(account);
        }
    }
}