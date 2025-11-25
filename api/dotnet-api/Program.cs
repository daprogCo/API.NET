using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Build the app
var app = builder.Build();

// 🔧 Always enable Swagger (even in production)
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Optional: Root message for testing with curl
app.MapGet("/", () => Results.Ok("✅ .NET API is running and reachable!"));

// ✅ Optional: Database connection test endpoint
app.MapGet("/dbtest", async () =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    try
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();
        return Results.Ok("✅ dotnet-api CAN connect to SQL Server!");
    }
    catch (Exception ex)
    {
        return Results.Problem("❌ dotnet-api CANNOT connect to SQL Server: " + ex.Message);
    }
});

app.MapGet("/cars-test", async () =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    var results = new List<object>();

    try
    {
        using var conn = new SqlConnection(connectionString);
        await conn.OpenAsync();

        var cmd = new SqlCommand("SELECT TOP 5 Id, Name, Mpg, Cylinders FROM Car", conn);

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            results.Add(new
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Mpg = reader.GetDecimal(2),
                Cylinders = reader.GetInt32(3)
            });
        }

        return Results.Ok(results);
    }
    catch (Exception ex)
    {
        return Results.Problem("❌ Error reading data from SQL Server: " + ex.Message);
    }
});

// Authorization (if needed)
app.UseAuthorization();

// API endpoints
app.MapControllers();

// 🏁 Start the app (host and port controlled by env or launch config.)
app.Run();
