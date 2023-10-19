using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
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
    [Authorize(Roles = "Administrator, Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VeterinarianDto>>> Get1()
    {
        var result = await _unitOfWork.Veterinarians.GetAllAsync();
        return _mapper.Map<List<VeterinarianDto>>(result);
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<VeterinarianDto>> Get2(int id)
    {
        var result = await _unitOfWork.Veterinarians.GetByIdAsync(id);
        return _mapper.Map<VeterinarianDto>(result);
    }
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
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
    [Authorize(Roles = "Administrator, Employee")]
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
    [Authorize(Roles = "Administrator, Employee")]
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
    [HttpGet("GetVeterinarianxSpeaciality")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VeterinarianDto>>> Get3()
    {
        var result = await _unitOfWork.Veterinarians.GetVeterinarianxSpeaciality();
        return _mapper.Map<List<VeterinarianDto>>(result);
    }
    [HttpGet]
    [Authorize(Roles = "Administrator, Employee")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<VeterinarianDto>>> Getpag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Veterinarians.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<VeterinarianDto>>(result.registros);
        return new Pager<VeterinarianDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
    [HttpGet("GetVeterinarianxSpeaciality")]
    [Authorize(Roles = "Administrator")]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<VeterinarianDto>>> GetVeterinarianxSpeaciality([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Veterinarians.GetVeterinarianxSpeaciality(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<VeterinarianDto>>(result.registros);
        return new Pager<VeterinarianDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
}
