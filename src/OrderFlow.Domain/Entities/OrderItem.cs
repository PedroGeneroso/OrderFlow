namespace OrderFlow.Domain.Entities;

public class OrderItem
{
    public Guid Id {get; private set;}
    public Guid ProductId {get; private set;}
    public int Quantity {get; private set;}
    public decimal UnitPrice {get; private set;}
    public decimal Subtotal => Quantity * UnitPrice;

    internal OrderItem(Guid productId, int quantity, decimal unitPrice)
    {
        Validate(quantity, unitPrice);
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    private void Validate(int quantity, decimal unitPrice)
    {
        ValidateQuantity(quantity);
        ValidatePrice(unitPrice);
    }

    private void ValidateQuantity(int quantity)
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
    private void ValidatePrice(decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Price below or equals 0", nameof(price));
        }
    }
}