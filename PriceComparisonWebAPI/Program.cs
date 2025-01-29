using Microsoft.Extensions.FileProviders;
using PriceComparisonWebAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

CreateMediaDirectoryIfNotExists();

ConfigurationService.ConfigureServices(builder);

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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Media", "Images")),
    RequestPath = "/images"
});

app.MapControllers();

app.Run();


void CreateMediaDirectoryIfNotExists()
{
    var directoryPath = Path.Combine(builder.Environment.ContentRootPath, "Media", "Images");

    if (!Directory.Exists(directoryPath))
    {
        Directory.CreateDirectory(directoryPath);
    }
}