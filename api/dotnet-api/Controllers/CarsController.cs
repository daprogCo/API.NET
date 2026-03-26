using dotnet_api.DTOs;
using dotnet_api.Models;
using dotnet_api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_api.Controllers;

[ApiController]
[Route("cars")]
public class CarsController(ICarRepository carRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CarDto>>> GetAll()
    {
        var cars = await carRepository.GetAllAsync();
        return Ok(cars.Select(ToDto));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarDto>> GetById(int id)
    {
        var car = await carRepository.GetByIdAsync(id);
        if (car is null)
        {
            return NotFound(new { message = $"Car with id {id} was not found." });
        }

        return Ok(ToDto(car));
    }

    [HttpGet("search")]
    public async Task<ActionResult<IReadOnlyList<CarDto>>> Search(
        [FromQuery] string? name,
        [FromQuery] string? origin,
        [FromQuery] decimal? mpgMax)
    {
        var cars = await carRepository.SearchAsync(name, origin, mpgMax);
        return Ok(cars.Select(ToDto));
    }

    [HttpPost]
    public async Task<ActionResult<CarDto>> Create([FromBody] CreateCarRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "name is required." });
        }

        var newId = await carRepository.CreateAsync(request);
        var created = await carRepository.GetByIdAsync(newId);
        if (created is null)
        {
            return StatusCode(500, new { message = "Car was created but could not be loaded." });
        }

        return CreatedAtAction(nameof(GetById), new { id = newId }, ToDto(created));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<CarDto>> Update(int id, [FromBody] UpdateCarRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return BadRequest(new { message = "name is required." });
        }

        var updated = await carRepository.UpdateAsync(id, request);
        if (!updated)
        {
            return NotFound(new { message = $"Car with id {id} was not found." });
        }

        var car = await carRepository.GetByIdAsync(id);
        return Ok(ToDto(car!));
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await carRepository.SoftDeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Car with id {id} was not found." });
        }

        return NoContent();
    }

    private static CarDto ToDto(Car car) => new()
    {
        Id = car.Id,
        Name = car.Name,
        Mpg = car.Mpg,
        Cylinders = car.Cylinders,
        Displacement = car.Displacement,
        Horsepower = car.Horsepower,
        Weight = car.Weight,
        Acceleration = car.Acceleration,
        ModelYear = car.ModelYear,
        Origin = car.Origin
    };
}
