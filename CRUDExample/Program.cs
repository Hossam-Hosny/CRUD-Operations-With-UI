using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(typeof(IPersonsServiece), typeof(PersonServices));
builder.Services.AddSingleton(typeof(ICountryService), typeof(CountriesServices));
//builder.Services.AddScoped<ICountryService,CountriesServices>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
