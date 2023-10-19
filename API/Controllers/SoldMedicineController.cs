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
public class SoldMedicineController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SoldMedicineController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [Authorize(Roles = "Administrator, Employee")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SoldMedicineDto>>> Get1()
    {
        var soldMedicines = await _unitOfWork.SoldMedicines.GetAllAsync();
        return _mapper.Map<List<SoldMedicineDto>>(soldMedicines);
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SoldMedicineDto>> Get2(int id)
    {
        var soldMedicine = await _unitOfWork.SoldMedicines.GetByIdAsync(id);
        return _mapper.Map<SoldMedicineDto>(soldMedicine);
    }
    [HttpPost]
    [Authorize(Roles = "Administrator, Employee")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SoldMedicine>> Post(SoldMedicineDto resultDto)
    {
        var result = _mapper.Map<SoldMedicine>(resultDto);
        this._unitOfWork.SoldMedicines.Add(result);
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
    public async Task<ActionResult<SoldMedicine>> Put(int id, [FromBody] SoldMedicineDto resultDto)
    {
        var result = _mapper.Map<SoldMedicine>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.SoldMedicines.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrator, Employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.SoldMedicines.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.SoldMedicines.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet("GetMovMedWithTotal")]
    [Authorize(Roles = "Administrator")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SoldMedicineTotalDto>>> Get3()
    {
        var soldMedicines = await _unitOfWork.SoldMedicines.GetMovMedWithTotal();
        return _mapper.Map<List<SoldMedicineTotalDto>>(soldMedicines);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [Authorize(Roles = "Administrator, Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<SoldMedicineDto>>> Getpag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.SoldMedicines.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<SoldMedicineDto>>(result.registros);
        return new Pager<SoldMedicineDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
}