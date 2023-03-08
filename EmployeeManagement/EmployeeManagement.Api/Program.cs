using EmployeeManagement.Api.DbContexts;
using EmployeeManagement.Api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<EmployeeDBContext>(options => options.UseInMemoryDatabase("EmployeeDb"));
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder=>
builder.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
app.UseAuthorization();

app.MapControllers();

app.Run();
