using System.Collections.ObjectModel;
using AccountsManager.Core;
using AccountsManager.Models;
using ReactiveUI;

namespace AccountsManager.Desktop.ViewModels;

public class MainWindowViewModels : ViewModelBase
{
    private int? _id;
    public int? Id
    {
        get => _id;
        set => this.RaiseAndSetIfChanged(ref _id, value);
    }
    
    private string? _firstName;
    public string? FirstName
    {
        get => _firstName;
        set => this.RaiseAndSetIfChanged(ref _firstName, value);
    }
    
    private string? _lastName;
    public string? LastName
    {
        get => _lastName;
        set => this.RaiseAndSetIfChanged(ref _lastName, value);
    }
    
    private string? _login;
    public string? Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
    
    private string? _password;
    public string? Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public ObservableCollection<Account> Accounts { get; } = [];
    
    private Account? _selectedAccount;
    public Account? SelectedAccount
    {
        get => _selectedAccount;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedAccount, value);
            
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