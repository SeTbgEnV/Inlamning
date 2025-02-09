using System.Text;
using System.Text.Json.Serialization;
using eshop.api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ViktorEngmanInlämning.Data.Migrations;
using ViktorEngmanInlämning.Data;
using ViktorEngmanInlämning.Entities;
using ViktorEngmanInlämning.Interfaces;
using ViktorEngmanInlämning.Services;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(9, 1, 0));
builder.Services.AddDbContext<DataContext>(options =>
{
    //options.UseMySql(builder.Configuration.GetConnectionString("MySQL"), serverVersion);
    options.UseSqlite(builder.Configuration.GetConnectionString("DevConnection"));
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddIdentityCore<User>(Options =>
{
    Options.Password.RequiredLength = 10;
    Options.User.RequireUniqueEmail = true;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("TokenSettings:TokenKey").Value))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using var scope = app.Services.CreateScope();
var Services = scope.ServiceProvider;
try
{
    var context = Services.GetRequiredService<DataContext>();
    // await context.Database.MigrateAsync();
    // await Seed.LoadSalesPeople(context);
    // await Seed.LoadProducts(context);
    // await Seed.LoadSalesOrders(context);
    // await Seed.LoadOrderItems(context);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();