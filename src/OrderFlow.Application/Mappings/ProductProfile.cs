using AutoMapper;
using OrderFlow.Application.DTOs.Product;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<CreateProductDto, Product>()
            .ConstructUsing(dto => new Product(dto.Name, dto.Price, dto.StockQuantity, dto.CategoryId));
    }
}