using PriceComparisonWebAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configureService = new ConfigurationService(builder);
configureService.ConfigureService();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


// 1. Install packages
// 2. Change user model
// 3. Change context
// 4. Configure JWT 
