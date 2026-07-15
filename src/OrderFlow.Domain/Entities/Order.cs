using OrderFlow.Domain.Enums;
using OrderFlow.Domain.Exceptions;

namespace OrderFlow.Domain.Entities;

public class Order
{
    public Guid Id {get; private set;}
    public Guid CustomerId {get; private set;}
    public DateTime CreatedAt {get; private set;}
    public OrderStatus Status {get; private set;}
    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    public decimal Total => _items.Sum(item => item.Subtotal);

    public Order(Guid customerId)
    {
        Id = Guid.NewGuid();
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        Status = OrderStatus.Created;
    }

    public void AddItem(Guid productId, int quantity, decimal unitPrice)
    {
        if (Status != OrderStatus.Created)
        {
            throw new DomainException("Cannot add items to an order that is not in Created status");
        }

        var item = new OrderItem(productId, quantity, unitPrice);
        _items.Add(item);
    }

    public void Pay()
    {
        if (Status != OrderStatus.Created)
        {
            throw new DomainException("Cannot pay an order that is not in Created status");
        }
        if (_items.Count == 0)
        {
            throw new DomainException("Cannot pay an order with 0 items");
        }
        Status = OrderStatus.Paid;
    }
    public void Ship()
    {
        if (Status != OrderStatus.Paid)
        {
            throw new DomainException("Cannot ship an order that is not in Paid status");
        }
        Status = OrderStatus.Shipped;
    }
    public void Deliver()
    {
        if (Status != OrderStatus.Shipped)
        {
            throw new DomainException("Cannot deliver an order that is not in Shipped status");
        }
        Status = OrderStatus.Delivered;
    }
    public void Cancel()
    {
        if (!(Status == OrderStatus.Created || Status == OrderStatus.Paid))
        {
            throw new DomainException("Cannot cancel an order that is not either in Created or Paid status");
        }
        Status = OrderStatus.Cancelled;
    }
}