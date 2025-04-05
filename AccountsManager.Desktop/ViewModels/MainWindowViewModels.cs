using System.Collections.ObjectModel;
using System.Reactive;
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
    
    public ReactiveCommand<Unit, Unit> CommandClear { get; }
    public ReactiveCommand<Unit, Unit> CommandDelete { get; }
    public ReactiveCommand<Unit, Unit> CommandSave { get; }

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
        RefreshAccounts(accounts);
        
        var canExecuteCommandClear = this.WhenAnyValue(
            vm => vm.FirstName, 
            vm => vm.LastName, 
            vm => vm.Login, 
            vm => vm.Password, 
            (firstName, lastName, login, password) => 
                firstName != null || 
                lastName != null || 
                login != null || 
                password != null);
        var canExecuteCommandDelete = this.WhenAnyValue(
            vm => vm.SelectedAccount,
            vm => vm.Id,
            (account, id) => account != null && id != null);
        var canExecuteCommandSave = this.WhenAnyValue(
            vm => vm.FirstName, 
            vm => vm.LastName, 
            vm => vm.Login, 
            vm => vm.Password, 
            (firstName, lastName, login, password) => 
                firstName != null && 
                lastName != null && 
                login != null && 
                password != null);

        CommandClear = ReactiveCommand.Create(
            execute: Clear,
            canExecute: canExecuteCommandClear);
        CommandDelete = ReactiveCommand.Create(
            execute: () => throw new NotImplementedException(),
            canExecute: canExecuteCommandDelete);
        CommandSave = ReactiveCommand.Create(
            execute: () =>
            {
                if (SelectedAccount == null)
                {
                    var account = new Account()
                    {
                        FirstName = this.FirstName!,
                        LastName = this.LastName!,
                        Login = this.Login!,
                        Password = this.Password!
                    };
                    service.AddAccount(account);
                    
                    Clear();
                    
                    var accounts = service.GetAllAccounts();
                    RefreshAccounts(accounts);
                }
                else
                {
                    throw new NotImplementedException();
                }
            },
            canExecute: canExecuteCommandSave);
    }

    private void Clear()
    {
        SelectedAccount = null;
        
        Id = null;
        FirstName = null;
        LastName = null;
        Login = null;
        Password = null;
    }

    private void RefreshAccounts(IEnumerable<Account> accounts)
    {
        Accounts.Clear();
        
        foreach (var account in accounts)
        {
            Accounts.Add(account);
        }
    }
}