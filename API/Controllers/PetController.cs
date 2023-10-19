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
    [Authorize(Roles = "Administrator, Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get1()
    {
        var pets = await _unitOfWork.Pets.GetAllAsync();
        return _mapper.Map<List<PetDto>>(pets);
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PetDto>> Get2(int id)
    {
        var pet = await _unitOfWork.Pets.GetByIdAsync(id);
        return _mapper.Map<PetDto>(pet);
    }
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
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
    [Authorize(Roles = "Administrator, Employee")]
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
    [Authorize(Roles = "Administrator, Employee")]
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
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get3()
    {
        var pets = await _unitOfWork.Pets.GetPetsxSpecie();
        return _mapper.Map<List<PetDto>>(pets);
    }
    [HttpGet("GetPetsxReason")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get4()
    {
        var pets = await _unitOfWork.Pets.GetPetsxReason();
        return _mapper.Map<List<PetDto>>(pets);
    }
    [HttpGet("GetPetsGroupBySpecie")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpeciesWithPetsDto>>> Get5()
    {
        var pets = await _unitOfWork.Pets.GetPetsGroupBySpecie();
        return _mapper.Map<List<SpeciesWithPetsDto>>(pets);
    }
    [HttpGet("GetPetsxVeterinarian/{name}")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetDto>>> Get6(string name)
    {
        var pets = await _unitOfWork.Pets.GetPetsxVeterinarian(name);
        return _mapper.Map<List<PetDto>>(pets);
    }
    [HttpGet("GetPetsGoldenRetriever")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PetsWithOwnerDto>>> Get7()
    {
        var pets = await _unitOfWork.Pets.GetPetsGoldenRetriever();
        return _mapper.Map<List<PetsWithOwnerDto>>(pets);
    }
    [HttpGet("GetPetCountByBreed")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<BreedWithPetCountDto>>> Get8()
    {
        var pets = await _unitOfWork.Pets.GetPetCountByBreed();
        return _mapper.Map<List<BreedWithPetCountDto>>(pets);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [Authorize(Roles = "Administrator, Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetDto>>> Getpag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Pets.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<PetDto>>(result.registros);
        return new Pager<PetDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
    [HttpGet("GetPetsxSpecie")]
    [MapToApiVersion("1.1")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetDto>>> GetPetsxSpecie([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Pets.GetPetsxSpecie(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<PetDto>>(result.registros);
        return new Pager<PetDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
    [HttpGet("GetPetsxReason")]
    [MapToApiVersion("1.1")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetDto>>> GetPetsxReason([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Pets.GetPetsxReason(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<PetDto>>(result.registros);
        return new Pager<PetDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
    [HttpGet("GetPetsGroupBySpecie")]
    [MapToApiVersion("1.1")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<SpeciesWithPetsDto>>> GetPetsGroupBySpecie([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Pets.GetPetsGroupBySpecie(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<SpeciesWithPetsDto>>(result.registros);
        return new Pager<SpeciesWithPetsDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
    [HttpGet("GetPetsxVeterinarian")]
    [MapToApiVersion("1.1")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetDto>>> GetPetsxVeterinarian([FromQuery] Params resultParams, string name)
    {
        var result = await _unitOfWork.Pets.GetPetsxVeterinarian(resultParams.PageIndex, resultParams.PageSize, resultParams.Search, name);
        var lstResultDto = _mapper.Map<List<PetDto>>(result.registros);
        return new Pager<PetDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
    [HttpGet("GetPetsGoldenRetriever")]
    [MapToApiVersion("1.1")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<PetsWithOwnerDto>>> GetPetsGoldenRetriever([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Pets.GetPetsGoldenRetriever(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<PetsWithOwnerDto>>(result.registros);
        return new Pager<PetsWithOwnerDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
    [HttpGet("GetPetCountByBreed")]
    [MapToApiVersion("1.1")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<BreedWithPetCountDto>>> GetPetCountByBreed([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.Pets.GetPetCountByBreed(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<BreedWithPetCountDto>>(result.registros);
        return new Pager<BreedWithPetCountDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
}
