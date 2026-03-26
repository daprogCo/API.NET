namespace dotnet_api.Models;

public class Car
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal? Mpg { get; set; }
    public int? Cylinders { get; set; }
    public decimal? Displacement { get; set; }
    public int? Horsepower { get; set; }
    public int? Weight { get; set; }
    public decimal? Acceleration { get; set; }
    public int? ModelYear { get; set; }
    public string? Origin { get; set; }
    public bool IsDeleted { get; set; }
}
