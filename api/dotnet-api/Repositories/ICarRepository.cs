using dotnet_api.DTOs;
using dotnet_api.Models;

namespace dotnet_api.Repositories;

public interface ICarRepository
{
    Task<IReadOnlyList<Car>> GetAllAsync();
    Task<Car?> GetByIdAsync(int id);
    Task<IReadOnlyList<Car>> SearchAsync(string? nameContains, string? origin, decimal? mpgMax);
    Task<int> CreateAsync(CreateCarRequest request);
    Task<bool> UpdateAsync(int id, UpdateCarRequest request);
    Task<bool> SoftDeleteAsync(int id);
}
