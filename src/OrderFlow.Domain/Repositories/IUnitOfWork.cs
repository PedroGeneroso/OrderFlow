namespace OrderFlow.Domain.Repositories;

public interface IUnitOfWork
{
    IProductRepository Products {get;}
    ICategoryRepository Categories {get;}
    ICustomerRepository Customers {get;}
    IOrderRepository Orders {get;}

    Task<int> SaveChangesAsync();
}