using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class PurchasedMedicineController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PurchasedMedicineController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PurchasedMedicineDto>>> Get1()
    {
        var purchasedMedicines = await _unitOfWork.PurchasedMedicine.GetAllAsync();
        return _mapper.Map<List<PurchasedMedicineDto>>(purchasedMedicines);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PurchasedMedicineDto>> Get2(int id)
    {
        var purchasedMedicine = await _unitOfWork.PurchasedMedicine.GetByIdAsync(id);
        return _mapper.Map<PurchasedMedicineDto>(purchasedMedicine);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PurchasedMedicine>> Post(PurchasedMedicineDto resultDto)
    {
        var result = _mapper.Map<PurchasedMedicine>(resultDto);
        this._unitOfWork.PurchasedMedicine.Add(result);
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
    public async Task<ActionResult<PurchasedMedicine>> Put(int id, [FromBody] PurchasedMedicineDto resultDto)
    {
        var result = _mapper.Map<PurchasedMedicine>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.PurchasedMedicine.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.PurchasedMedicine.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.PurchasedMedicine.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
