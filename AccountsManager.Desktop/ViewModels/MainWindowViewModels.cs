using System.Collections.ObjectModel;
using AccountsManager.Core;
using AccountsManager.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AccountsManager.Desktop.ViewModels;

public class MainWindowViewModels : ViewModelBase
{
    [Reactive] public int? Id { get; set; }
    
    [Reactive] public string? FirstName{ get; set; }
    
    [Reactive] public string? LastName{ get; set; }
    
    [Reactive] public string? Login{ get; set; }
    
    [Reactive] public string? Password{ get; set; }

    public ObservableCollection<Account> Accounts { get; } = [];
    
    [Reactive] public Account? SelectedAccount{ get; set; }

    public MainWindowViewModels()
    {
        this.WhenAnyValue(vm => vm.SelectedAccount)
            .Subscribe(a =>
            {
                Id = a?.Id;
                FirstName = a?.FirstName;
                LastName = a?.LastName;
                Login = a?.Login;
                Password = a?.Password;
            });
        
        var connectionString = DbConfig.GetConnectionString("db_config.json");
        var service = new Service(connectionString);
        var accounts = service.GetAllAccounts();

        foreach (var account in accounts)
        {
            Accounts.Add(account);
        }
    }
}