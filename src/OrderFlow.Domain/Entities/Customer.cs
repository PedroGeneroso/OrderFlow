using System.Text.RegularExpressions;

namespace OrderFlow.Domain.Entities;

public class Customer
{
    public Guid Id {get; private set;}
    public string Name {get; private set;}
    public string Email {get; private set;}

    public Customer(string name, string email){
        ValidateName(name);
        ValidateEmail(email);
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    private void ValidateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
    }

    private void ValidateEmail(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        if(!(Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase)))
        {
            throw new ArgumentException("Email needs to follow the format: email@host.com", nameof(email));
        }
    }
}