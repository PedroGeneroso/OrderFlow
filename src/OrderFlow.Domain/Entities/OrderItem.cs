namespace OrderFlow.Domain.Entities;

internal class OrderItem
{
    public Guid Id {get; private set;}
    public Guid ProductId {get; private set;}
    public int Quantity {get; private set;}
    public decimal UnitPrice {get; private set;}
    public decimal Subtotal {get; private set;}

    internal OrderItem(Guid productId, int quantity, decimal unitPrice){
        Id = Guid.NewGuid();
        ProductId = productId;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Subtotal = quantity*unitPrice;
    }
}