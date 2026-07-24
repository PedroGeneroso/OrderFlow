using OrderFlow.Domain.Entities;

namespace OrderFlow.Domain.Repositories;

public interface IOrderRepository : IRepository<Order>
{
    Task<IEnumerable<Order>> GetByCustomerAsync(Guid customerId);
}