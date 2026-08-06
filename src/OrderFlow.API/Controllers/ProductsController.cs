using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.DTOs.Product;
using OrderFlow.Domain.Repositories;

namespace OrderFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(Guid id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);

        if(product is null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<ProductResponseDto>(product);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create(CreateProductDto dto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
        if (category is null)
        {
            return BadRequest("Provided category does not exist");
        }
        
        var product = _mapper.Map<Domain.Entities.Product>(dto);

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = _mapper.Map<ProductResponseDto>(product);
        responseDto.CategoryName = category.Name;

        return CreatedAtAction(nameof(GetById), new {id = product.Id}, responseDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<ProductResponseDto>>(products);
        return Ok(dtos);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductResponseDto>> Update(Guid id, UpdateProductDto dto)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        product.UpdateDetails(dto.Name, dto.Price);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = _mapper.Map<ProductResponseDto>(product);
        return Ok(responseDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product is null)
        {
            return NotFound();
        }

        _unitOfWork.Products.Delete(product);
        await _unitOfWork.SaveChangesAsync();
        
        return NoContent();
    }
}