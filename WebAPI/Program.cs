using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAppAPI.Context;
using MyAppAPI.Interface;
using MyAppAPI.Services;
using RSA_Angular_.NET_CORE.RSA;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration _config = new ConfigurationBuilder()
.AddJsonFile("appsettings.json").Build();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(option =>
               {
                   option.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["TokenKey"])),
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true
                   };
               });
//builder.Services.AddScoped<IDataRepository<>, DataRepository>();
builder.Services.AddScoped(typeof(IDataRepository), typeof(DataRepository));
builder.Services.AddScoped<IAuthentication, Authentication>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddScoped<IRsaHelper, RsaHelper>();
builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseSqlite("Filename=D:/home/site/wwwroot/App.db"); //For Publish
    options.UseSqlite(_config["SQLiteConnection"]);
    //options.UseSqlite(_config["SQLiteConnectionPublish"]);
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()); //enable cors policy with the given url

app.UseAuthorization();

app.MapControllers();

app.Run();
