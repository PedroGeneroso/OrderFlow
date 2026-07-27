using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Entities;
using OrderFlow.Domain.Repositories;

namespace OrderFlow.Infrastructure.Persistence.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OrderFlowDbContext context) : base(context)
    {
        
    }

    public new async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(o => o.Items)
            .FirstOrDefaultAsync(o=>o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetByCustomerAsync(Guid customerId)
    {
        return await _dbSet
            .Include(o => o.Items)
            .Where(o => o.CustomerId ==customerId)
            .ToListAsync();
    }
}