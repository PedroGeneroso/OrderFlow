using Microsoft.EntityFrameworkCore;
using OrderFlow.Domain.Entities;
using OrderFlow.Domain.Repositories;

namespace OrderFlow.Infrastructure.Persistence.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(OrderFlowDbContext context) : base(context)
    {
        
    }

    public new async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p=>p.Id == id);
    }
    public async Task<IEnumerable<Product>> GetByCategoryAsync(Guid categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
}