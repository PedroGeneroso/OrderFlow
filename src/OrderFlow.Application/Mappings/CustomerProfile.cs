using AutoMapper;
using OrderFlow.Application.DTOs.Customer;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerResponseDto>();
        CreateMap<CreateCustomerDto, Customer>()
            .ConstructUsing(dto => new Customer(dto.Name, dto.Email));
    }
}