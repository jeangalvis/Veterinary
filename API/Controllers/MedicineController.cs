using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class MedicineController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MedicineController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MedicineDto>>> Get1()
    {
        var medicines = await _unitOfWork.Medicines.GetAllAsync();
        return _mapper.Map<List<MedicineDto>>(medicines);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<MedicineDto>> Get2(int id)
    {
        var medicine = await _unitOfWork.Medicines.GetByIdAsync(id);
        return _mapper.Map<MedicineDto>(medicine);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Medicine>> Post(MedicineDto resultDto)
    {
        var result = _mapper.Map<Medicine>(resultDto);
        this._unitOfWork.Medicines.Add(result);
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
    public async Task<ActionResult<Medicine>> Put(int id, [FromBody] MedicineDto resultDto)
    {
        var result = _mapper.Map<Medicine>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Medicines.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Medicines.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Medicines.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
