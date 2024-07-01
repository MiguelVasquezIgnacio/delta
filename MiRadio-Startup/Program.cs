using Microsoft.EntityFrameworkCore;
using MiRadio_Startup.Context;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores con vistas
builder.Services.AddControllersWithViews();

// Configurar la cadena de conexi�n y agregar el DbContext
builder.Services.AddDbContext<MyContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaConexion"));
});

var app = builder.Build();

// Configuraci�n del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
