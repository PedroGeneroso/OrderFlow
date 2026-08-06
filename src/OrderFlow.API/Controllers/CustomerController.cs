using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Application.DTOs.Customer;
using OrderFlow.Domain.Repositories;

namespace OrderFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> GetById(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);

        if (customer is null)
        {
            return NotFound();
        }

        var dto = _mapper.Map<CustomerResponseDto>(customer);
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponseDto>> Create(CreateCustomerDto dto)
    {
        var customer = _mapper.Map<Domain.Entities.Customer>(dto);

        await _unitOfWork.Customers.AddAsync(customer);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = _mapper.Map<CustomerResponseDto>(customer);
        return CreatedAtAction(nameof(GetById), new {id = customer.Id}, responseDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
    {
        var customer = await _unitOfWork.Customers.GetAllAsync();
        var dtos = _mapper.Map<IEnumerable<CustomerResponseDto>>(customer);
        return Ok(dtos);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> Update(Guid id, CreateCustomerDto dto)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);
        if (customer is null)
        {
            return NotFound();
        }

        customer.UpdateDetails(dto.Name, dto.Email);

        _unitOfWork.Customers.Update(customer);
        await _unitOfWork.SaveChangesAsync();

        var responseDto = _mapper.Map<CustomerResponseDto>(customer);
        return Ok(responseDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var customer = await _unitOfWork.Customers.GetByIdAsync(id);
        if (customer is null)
        {
            return NotFound();
        }

        _unitOfWork.Customers.Delete(customer);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}