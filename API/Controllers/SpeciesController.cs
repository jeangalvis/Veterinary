using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class SpeciesController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SpeciesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpeciesDto>>> Get1()
    {
        var species = await _unitOfWork.Species.GetAllAsync();
        return _mapper.Map<List<SpeciesDto>>(species);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SpeciesDto>> Get2(int id)
    {
        var species = await _unitOfWork.Species.GetByIdAsync(id);
        return _mapper.Map<SpeciesDto>(species);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Species>> Post(SpeciesDto resultDto)
    {
        var result = _mapper.Map<Species>(resultDto);
        this._unitOfWork.Species.Add(result);
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
    public async Task<ActionResult<Species>> Put(int id, [FromBody] SpeciesDto resultDto)
    {
        var result = _mapper.Map<Species>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Species.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Species.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Species.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
