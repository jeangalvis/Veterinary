using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class SupplierController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SupplierController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SupplierDto>>> Get1()
    {
        var suppliers = await _unitOfWork.Suppliers.GetAllAsync();
        return _mapper.Map<List<SupplierDto>>(suppliers);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SupplierDto>> Get2(int id)
    {
        var supplier = await _unitOfWork.Suppliers.GetByIdAsync(id);
        return _mapper.Map<SupplierDto>(supplier);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Supplier>> Post(SupplierDto resultDto)
    {
        var result = _mapper.Map<Supplier>(resultDto);
        this._unitOfWork.Suppliers.Add(result);
        await _unitOfWork.SaveAsync();
        if (result == null)
        {
            return BadRequest();
        }
        resultDto.Id = result.Id;
        return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Supplier>> Put(int id, [FromBody] SupplierDto resultDto)
    {
        var result = _mapper.Map<Supplier>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Suppliers.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Suppliers.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Suppliers.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
