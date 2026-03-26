using dotnet_api.DTOs;
using dotnet_api.Models;
using Microsoft.Data.SqlClient;

namespace dotnet_api.Repositories;

public class SqlCarRepository(IConfiguration configuration) : ICarRepository
{
    private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Missing connection string: DefaultConnection");

    public async Task<IReadOnlyList<Car>> GetAllAsync()
    {
        const string sql = @"
SELECT Id, name, mpg, cylinders, displacement, horsepower, weight, acceleration, model_year, origin, is_deleted
FROM Car
WHERE is_deleted = 0
ORDER BY Id;";

        return await QueryCarsAsync(sql, []);
    }

    public async Task<Car?> GetByIdAsync(int id)
    {
        const string sql = @"
SELECT Id, name, mpg, cylinders, displacement, horsepower, weight, acceleration, model_year, origin, is_deleted
FROM Car
WHERE Id = @id AND is_deleted = 0;";

        var cars = await QueryCarsAsync(sql, [new SqlParameter("@id", id)]);
        return cars.FirstOrDefault();
    }

    public async Task<IReadOnlyList<Car>> SearchAsync(string? nameContains, string? origin, decimal? mpgMax)
    {
        const string sql = @"
SELECT Id, name, mpg, cylinders, displacement, horsepower, weight, acceleration, model_year, origin, is_deleted
FROM Car
WHERE is_deleted = 0
  AND (@nameContains IS NULL OR name LIKE '%' + @nameContains + '%')
  AND (@origin IS NULL OR origin = @origin)
  AND (@mpgMax IS NULL OR mpg <= @mpgMax)
ORDER BY Id;";

        return await QueryCarsAsync(
            sql,
            [
                new SqlParameter("@nameContains", (object?)nameContains ?? DBNull.Value),
                new SqlParameter("@origin", (object?)origin ?? DBNull.Value),
                new SqlParameter("@mpgMax", (object?)mpgMax ?? DBNull.Value)
            ]);
    }

    public async Task<int> CreateAsync(CreateCarRequest request)
    {
        const string sql = @"
INSERT INTO Car (name, mpg, cylinders, displacement, horsepower, weight, acceleration, model_year, origin)
VALUES (@name, @mpg, @cylinders, @displacement, @horsepower, @weight, @acceleration, @modelYear, @origin);
SELECT CAST(SCOPE_IDENTITY() AS INT);";

        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(sql, conn);
        AddWriteParameters(cmd, request.Name, request.Mpg, request.Cylinders, request.Displacement, request.Horsepower, request.Weight, request.Acceleration, request.ModelYear, request.Origin);

        var newId = (int)(await cmd.ExecuteScalarAsync() ?? throw new InvalidOperationException("Failed to create car."));
        return newId;
    }

    public async Task<bool> UpdateAsync(int id, UpdateCarRequest request)
    {
        const string sql = @"
UPDATE Car
SET
    name = @name,
    mpg = @mpg,
    cylinders = @cylinders,
    displacement = @displacement,
    horsepower = @horsepower,
    weight = @weight,
    acceleration = @acceleration,
    model_year = @modelYear,
    origin = @origin
WHERE Id = @id AND is_deleted = 0;";

        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        AddWriteParameters(cmd, request.Name, request.Mpg, request.Cylinders, request.Displacement, request.Horsepower, request.Weight, request.Acceleration, request.ModelYear, request.Origin);

        var rows = await cmd.ExecuteNonQueryAsync();
        return rows > 0;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        const string sql = "UPDATE Car SET is_deleted = 1 WHERE Id = @id AND is_deleted = 0;";

        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);

        var rows = await cmd.ExecuteNonQueryAsync();
        return rows > 0;
    }

    private async Task<IReadOnlyList<Car>> QueryCarsAsync(string sql, SqlParameter[] parameters)
    {
        var cars = new List<Car>();

        await using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddRange(parameters);

        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            cars.Add(new Car
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Mpg = reader.IsDBNull(2) ? null : reader.GetDecimal(2),
                Cylinders = reader.IsDBNull(3) ? null : reader.GetInt32(3),
                Displacement = reader.IsDBNull(4) ? null : reader.GetDecimal(4),
                Horsepower = reader.IsDBNull(5) ? null : reader.GetInt32(5),
                Weight = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                Acceleration = reader.IsDBNull(7) ? null : reader.GetDecimal(7),
                ModelYear = reader.IsDBNull(8) ? null : reader.GetInt32(8),
                Origin = reader.IsDBNull(9) ? null : reader.GetString(9),
                IsDeleted = reader.GetBoolean(10)
            });
        }

        return cars;
    }

    private static void AddWriteParameters(
        SqlCommand cmd,
        string name,
        decimal? mpg,
        int? cylinders,
        decimal? displacement,
        int? horsepower,
        int? weight,
        decimal? acceleration,
        int? modelYear,
        string? origin)
    {
        cmd.Parameters.AddWithValue("@name", name);
        cmd.Parameters.AddWithValue("@mpg", (object?)mpg ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@cylinders", (object?)cylinders ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@displacement", (object?)displacement ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@horsepower", (object?)horsepower ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@weight", (object?)weight ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@acceleration", (object?)acceleration ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@modelYear", (object?)modelYear ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@origin", (object?)origin ?? DBNull.Value);
    }
}
