using Datos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Presentacion.Config;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:44423",
                                "http://localhost:4200")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Configurar cadena de Conexion con EF
var connectionString = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddDbContext<LibreriaContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllersWithViews();

#region configure strongly typed settings objects
    var appSettingsSection = builder.Configuration.GetSection("AppSetting");
    builder.Services.Configure<AppSetting>(appSettingsSection);
#endregion

#region Configure jwt authentication inteprete el token 
    var appSettings = appSettingsSection.Get<AppSetting>();
    var key = Encoding.ASCII.GetBytes(appSettings.Secret!);

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
