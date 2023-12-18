using EcommerceStore.Data;
using EcommerceStore.Services;
using EcommerceStore.Services.Iservices;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")));
builder.Services.AddScoped<Iorders, OrderServices>();
builder.Services.AddScoped<Iproducts, ProductServices>();
builder.Services.AddScoped<Iusers, UserServices>();
builder.Services.AddScoped<Ijwt,JwtService>();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", options =>
    {
        options.RequireAuthenticatedUser();
        options.RequireClaim("Roles","Admin");
    });
});
builder.Services.AddAuthentication("bearer").AddJwtBearer(options => {
    options.TokenValidationParameters = new()
    {
        
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey= true,
        ValidAudience = builder.Configuration.GetSection("JwtOptions:Audience").Value,
        ValidIssuer = builder.Configuration.GetSection("JwtOptions:Issuer").Value,
        IssuerSigningKey= new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:SecretKey").Value))

    };
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
