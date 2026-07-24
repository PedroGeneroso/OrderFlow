using OrderFlow.Domain.Exceptions;

namespace OrderFlow.Domain.Entities;

public class Product
{
    public Guid Id {get; private set; }
    public string Name {get; private set;}
    public decimal Price {get; private set;}
    public int StockQuantity{get; private set;} = 0;

    private Product(){}

    public Product(string name, decimal price, int stockQuantity){
        Id = Guid.NewGuid();
        ValidateName(name);
        ValidatePrice(price);
        ValidateStockQuantity(stockQuantity);
        Name = name;
        Price = price;
        StockQuantity = stockQuantity;
        
    }

    public void DecreaseStock(int quantity)
    {
        ValidateInputQuantity(quantity);
        if (StockQuantity - quantity < 0)
        {
            throw new DomainException("Stock cannot be negative");
        }
        StockQuantity -= quantity;

    }
    public void IncreaseStock(int quantity)
    {
        ValidateInputQuantity(quantity);
        StockQuantity += quantity;
    }

// Validation of arguments
    private void ValidateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
    }
    private void ValidatePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Price below or equals 0", nameof(price));
        }
    }
    private void ValidateStockQuantity(int newStockQuantity)
    {
        if (newStockQuantity < 0)
        {
            throw new ArgumentException("StockQuantity below 0", nameof(newStockQuantity));
        }
    }

    private void ValidateInputQuantity(int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentException("Quantity cannot be negative", nameof(quantity));
        }
        if (quantity == 0)
        {
            throw new ArgumentException("Quantity cannot be equal to 0", nameof(quantity));
        }
    }
}