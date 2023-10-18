using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class PetController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PetController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get1()
    {
        var pets = await _unitOfWork.Pets.GetAllAsync();
        return _mapper.Map<List<PetDto>>(pets);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PetDto>> Get2(int id)
    {
        var pet = await _unitOfWork.Pets.GetByIdAsync(id);
        return _mapper.Map<PetDto>(pet);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pet>> Post(PetDto resultDto)
    {
        var result = _mapper.Map<Pet>(resultDto);
        this._unitOfWork.Pets.Add(result);
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
    public async Task<ActionResult<Pet>> Put(int id, [FromBody] PetDto resultDto)
    {
        var result = _mapper.Map<Pet>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Pets.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Pets.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Pets.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GetPetsxSpecie")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get3()
    {
        var pets = await _unitOfWork.Pets.GetPetsxSpecie();
        return _mapper.Map<List<PetDto>>(pets);
    }
    [HttpGet("GetPetsxReason")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get4()
    {
        var pets = await _unitOfWork.Pets.GetPetsxReason();
        return _mapper.Map<List<PetDto>>(pets);
    }
}
