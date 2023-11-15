using Web_3_Shevelenkov;
using Web_3_Shevelenkov.Services.Implementations;
using Web_3_Shevelenkov.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

var configBuilder = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json");

IConfiguration _configuration = configBuilder.Build();

UriData UriData = _configuration.GetSection("UriData").Get<UriData>();


builder.Services.AddScoped<ITankTypeService, ApiTankTypeService>();
builder.Services.AddScoped<ITankService, ApiTankService>();

builder.Services
    .AddHttpClient<ITankService, ApiTankService>(opt =>
    opt.BaseAddress = new Uri(UriData.ApiUri));

builder.Services
    .AddHttpClient<ITankTypeService, ApiTankTypeService>(opt =>
    opt.BaseAddress = new Uri(UriData.ApiUri));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areaRoute",
        pattern: "{area:exists}/{controller=Tanks}/{action=Index}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

});

app.MapRazorPages();

app.Run();
