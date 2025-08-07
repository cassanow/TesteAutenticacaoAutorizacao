using EstudandoAutenticacaoAutorizacao.DataBase;
using EstudandoAutenticacaoAutorizacao.Repository;
using EstudandoAutenticacaoAutorizacao.Repository.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddDbContext<Context>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")) );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

