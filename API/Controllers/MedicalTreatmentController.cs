using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
public class MedicalTreatmentController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicalTreatmentController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicalTreatmentDto>>> Get1()
    {
        var medicalTreatments = await _unitOfWork.MedicalTreatments.GetAllAsync();
        return _mapper.Map<List<MedicalTreatmentDto>>(medicalTreatments);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicalTreatmentDto>> Get2(int id)
    {
        var medicalTreatment = await _unitOfWork.MedicalTreatments.GetByIdAsync(id);
        return _mapper.Map<MedicalTreatmentDto>(medicalTreatment);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicalTreatment>> Post(MedicalTreatmentDto resultDto)
    {
        var result = _mapper.Map<MedicalTreatment>(resultDto);
        this._unitOfWork.MedicalTreatments.Add(result);
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
    public async Task<ActionResult<MedicalTreatment>> Put(int id, [FromBody] MedicalTreatmentDto resultDto)
    {
        var result = _mapper.Map<MedicalTreatment>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.MedicalTreatments.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.MedicalTreatments.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.MedicalTreatments.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<MedicalTreatmentDto>>> Getpag([FromQuery] Params resultParams)
    {
        var result = await _unitOfWork.MedicalTreatments.GetAllAsync(resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
        var lstResultDto = _mapper.Map<List<MedicalTreatmentDto>>(result.registros);
        return new Pager<MedicalTreatmentDto>(lstResultDto, result.totalRegistros, resultParams.PageIndex, resultParams.PageSize, resultParams.Search);
    }
}
