using Web_3_Shevelenkov.API.Data;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Web_3_Shevelenkov.API.Services.Interfaces;
using Web_3_Shevelenkov.API.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

var configBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json"); 

IConfiguration _configuration = configBuilder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                }
                ));

builder.Services.AddSingleton<ITankTypeService, TankTypeService>();
builder.Services.AddScoped<ITankService, TankService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

await DbInitializer.SeedData(app);


app.Run();
