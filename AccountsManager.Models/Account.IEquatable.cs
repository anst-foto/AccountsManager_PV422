namespace AccountsManager.Models;

public partial class Account : IEquatable<Account>
{
    public bool Equals(Account? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id &&
               LastName == other.LastName &&
               FirstName == other.FirstName &&
               Login == other.Login &&
               Password == other.Password;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Account)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, LastName, FirstName, Login, Password);
    }

    public static bool operator ==(Account? left, Account? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Account? left, Account? right)
    {
        return !Equals(left, right);
    }
}