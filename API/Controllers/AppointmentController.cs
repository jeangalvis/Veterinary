using API.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AppointmentController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AppointmentController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;

    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> Get1()
    {
        var appointments = await _unitOfWork.Appointments
                                    .GetAllAsync();

        return _mapper.Map<List<AppointmentDto>>(appointments);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppointmentDto>> Get2(int id)
    {
        var appointment = await _unitOfWork.Appointments.GetByIdAsync(id);
        return _mapper.Map<AppointmentDto>(appointment);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Appointment>> Post(AppointmentDto resultDto)
    {
        var result = _mapper.Map<Appointment>(resultDto);
        this._unitOfWork.Appointments.Add(result);
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
    public async Task<ActionResult<Appointment>> Put(int id, [FromBody] AppointmentDto resultDto)
    {
        var result = _mapper.Map<Appointment>(resultDto);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Appointments.Update(result);
        await _unitOfWork.SaveAsync();
        return result;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _unitOfWork.Appointments.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        _unitOfWork.Appointments.Remove(result);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
