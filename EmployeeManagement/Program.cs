using EmployeeManagement.Data;
using EmployeeManagement.Mappings;
using EmployeeManagement.Middlewares;
using EmployeeManagement.Repositories;
using EmployeeManagement.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
    options.IncludeXmlComments(xmlPath);
});
//Add AutoMapper as per AutoMapper 13.0+
builder.Services.AddAutoMapper( cfg => {cfg.AddProfile<EmployeeProfile>(); },typeof(Program));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Now it's at the root URL
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

app.Run();
