using OrderFlow.Domain.Repositories;

namespace OrderFlow.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly OrderFlowDbContext _context;

    public IProductRepository Products {get;}
    public ICategoryRepository Categories {get;}
    public ICustomerRepository Customers {get;}
    public IOrderRepository Orders {get;}

    public UnitOfWork(OrderFlowDbContext context)
    {
        _context = context;
        Products = new ProductRepository(context);
        Categories = new CategoryRepository(context);
        Customers = new CustomerRepository(context);
        Orders = new OrderRepository(context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}