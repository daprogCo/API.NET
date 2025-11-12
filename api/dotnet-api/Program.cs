var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 🔧 Always enable Swagger (even in production)
app.UseSwagger();
app.UseSwaggerUI();

// ✅ Optional: Root message for testing with curl
app.MapGet("/", () => Results.Ok("✅ .NET API is running and reachable!"));

// Authorization (if needed)
app.UseAuthorization();

// API endpoints
app.MapControllers();

// 🏁 Start the app (host and port controlled by env or launch config)
app.Run();
