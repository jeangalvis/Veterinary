using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class VeterinarianController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VeterinarianController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VeterinarianDto>>> Get1()
    {
        var suppliers = await _unitOfWork.Veterinarians.GetAllAsync();
        return _mapper.Map<List<VeterinarianDto>>(suppliers);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VeterinarianDto>> Get2(int id)
    {
        var supplier = await _unitOfWork.Veterinarians.GetByIdAsync(id);
        return _mapper.Map<VeterinarianDto>(supplier);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Veterinarian>> Post(VeterinarianDto resultDto)
    {
        var result = _mapper.Map<Veterinarian>(resultDto);
        this._unitOfWork.Veterinarians.Add(result);
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
    public async Task<ActionResult<Veterinarian>> Put(int id, [FromBody] VeterinarianDto resultDto)
    {
        var result = _mapper.Map<Veterinarian>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Veterinarians.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Veterinarians.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Veterinarians.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
