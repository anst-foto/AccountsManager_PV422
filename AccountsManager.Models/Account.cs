namespace AccountsManager.Models;


//public record class Account
public partial class Account
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}