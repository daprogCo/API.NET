using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext with SQL Server
builder.Services.AddDbContext<CarsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

// Authorization (if needed)
app.UseAuthorization();

// API endpoints
app.MapControllers();

// 🏁 Start the app (host and port controlled by env or launch config.)
app.Run();
