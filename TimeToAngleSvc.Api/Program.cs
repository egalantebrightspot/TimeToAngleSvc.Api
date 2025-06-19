var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<TimeToAngleSvc.Library.Interfaces.ICalculateTimeAngleLibrary, TimeToAngleSvc.Library.Implementations.CalculateTimeAngleLibrary>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/calculate-angle", async (
        TimeToAngleSvc.Library.Interfaces.ICalculateTimeAngleLibrary angleLibrary,
        TimeToAngleSvc.Models.TimeToAngleRequest request) =>
    {
        var result = await angleLibrary.CalculateAngleAsync(request);
        return Results.Ok(result);
    })
    .WithName("CalculateAngle")
    .WithOpenApi();

app.Run();