namespace OrderFlow.Domain.Entities;

public class Category
{
    public Guid Id {get; private set;}
    public string Name {get; private set;}

    public Category(string name)
    {
        Id = Guid.NewGuid();
        ValidateName(name);
        Name= name;
    }

    public void Rename(string newName)
    {
        ValidateName(newName);
        Name = newName;
    }

    private void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("No name for Category provided", nameof(name));
        }
    }

}
