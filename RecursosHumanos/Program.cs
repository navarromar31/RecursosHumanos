using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecursosHumanos_AccesoDatos;
using RecursosHumanos_AccesoDatos.Datos.Repositorio.IRepositorio;
using RecursosHumanos_AccesoDatos.Datos.Repositorio;
using RecursosHumanos_AccesoDatos.Migrations;
using RecursosHumanos_AccesoDatos.Datos;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AplicationDbContext>(options =>
                                                options.UseSqlServer(
                                                builder.Configuration.GetConnectionString("MarianaConnection")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AplicationDbContext>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identify/Account/Login"; //Ruta del Login
        options.AccessDeniedPath = "/Identity/Account/AccessDenied"; //Pagina de acceso denegada
    });

//Lo modificamos para agregar al servicio la asignacion de roles de usuario
/*builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddDefaultTokenProviders().AddDefaultUI().
    AddEntityFrameworkStores<AplicationDbContext>(); 
 ESTO SE OCUPA CUANDO HAGAMOS EL EMAIL SENDER*/


builder.Services.AddControllersWithViews();


builder.Services.AddScoped<ICapacitacionRepositorio, CapacitacionRepositorio>();
builder.Services.AddScoped<IColaboradorRepositorio, ColaboradorRepositorio>();
builder.Services.AddScoped<IEvaluacionRepositorio, EvaluacionRepositorio>();



// A?ade el servicio HttpContextAccessor al contenedor de servicios
builder.Services.AddHttpContextAccessor();

// Configura el servicio de sesi?n-

builder.Services.AddSession(Options =>
{
    // Establece el tiempo de inactividad antes de que la sesi?n expire
    Options.IdleTimeout = TimeSpan.FromMinutes(10);

    // Configura la cookie de sesi?n para que sea accesible solo a trav?s de HTTP
    Options.Cookie.HttpOnly = true;

    // Marca la cookie de sesi?n como esencial, lo que significa que no se ver? afectada por las pol?ticas de consentimiento de cookies
    Options.Cookie.IsEssential = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//MIDLEWARE
app.UseHttpsRedirection();
app.UseStaticFiles();//HOJAS DE STILO, JS , ETC

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();//este el pipeline para utlizar el servicio de sesiones 

app.MapRazorPages();

app.MapGet("/", async context =>
{
    //Si el usuario no esta registrado, redirige al login
    if (!context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/Identity/Account/Login");
        return;
    }

    //Si esta atenticado, envia a la pagina de inicio
    context.Response.Redirect("/Home/Index");
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
