using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Interfaces;
using ProductService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ProductContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//CUANDO TENGA EL MICROSERVICIO DE CATEGORIA FUNCIONANDO OBTENGO LA URL Y LA INSERTO ACÁ
builder.Services.AddHttpClient<CategoryClientService>(client =>
{
    // URL del microservicio de categorías
    client.BaseAddress = new Uri("http://localhost:PUERTO");
});

builder.Services.AddScoped<IProductRepository, ProductRepositoryService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
