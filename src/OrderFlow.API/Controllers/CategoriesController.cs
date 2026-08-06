using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.DTOs.Category;
using OrderFlow.Domain.Repositories;

namespace OrderFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDto>> GetById(Guid id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);

        if (category is null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<CategoryResponseDto>(category);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> Create(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Domain.Entities.Category>(dto);

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = _mapper.Map<CategoryResponseDto>(category);
        return CreatedAtAction(nameof(GetById), new {id = category.Id}, responseDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var categories = await _unitOfWork.Categories.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<CategoryResponseDto>>(categories);
        return Ok(dtos);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CategoryResponseDto>> Update(Guid id, CreateCategoryDto dto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        category.Rename(dto.Name);

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = _mapper.Map<CategoryResponseDto>(category);
        return Ok(responseDto);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id);
        if (category is null)
        {
            return NotFound();
        }

        _unitOfWork.Categories.Delete(category);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}