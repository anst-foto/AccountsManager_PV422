namespace AccountsManager.Models;


public partial class Account
{
    public int Id { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
}