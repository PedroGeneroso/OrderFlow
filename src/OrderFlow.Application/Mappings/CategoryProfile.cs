using AutoMapper;
using OrderFlow.Application.DTOs.Category;
using OrderFlow.Domain.Entities;

namespace OrderFlow.Application.Mappings;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryResponseDto>();
        CreateMap<CreateCategoryDto, Category>()
            .ConstructUsing(dto => new Category(dto.Name));
    }
}