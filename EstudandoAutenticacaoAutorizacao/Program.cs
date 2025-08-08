using System.Text;
using EstudandoAutenticacaoAutorizacao.DataBase;
using EstudandoAutenticacaoAutorizacao.Helper;
using EstudandoAutenticacaoAutorizacao.Repository;
using EstudandoAutenticacaoAutorizacao.Repository.Interface;
using EstudandoAutenticacaoAutorizacao.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "Admin API",
        Description = "Estudando Autenticacao Autorizacao",
    });
    
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Cabeçalho de Autorizacao  esta utilizando o esquema Bearer \r \n\r\n Digite 'Bearer' antes de colocar o Token"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:issuer"],
        ValidAudience = builder.Configuration["JWT:audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"]))
    };
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));



builder.Services.AddDbContext<Context>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")) );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();
app.MapControllers();

app.Run();

