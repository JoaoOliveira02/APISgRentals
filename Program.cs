using GerenciadorDeEmpresas.Context;
using GerenciadorDeEmpresas.Repositories.Interfaces;
using GerenciadorDeEmpresas.Repositories;
using Microsoft.EntityFrameworkCore;
using GerenciadorDeEmpresas.UnitOfWork;
using System.Text.Json.Serialization;
using System.Reflection;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions
            .ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "APIGerenciadorDeEmpresas",
        Description = "Gerenciador de Empresas, e Usuarios dessas empresas.",
    });

    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));

});

string ConexaoOminiclub = builder.Configuration.GetConnectionString("ConnectionBanco");

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(ConexaoOminiclub));

builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ITipoEmpresaRepository, TipoEmpresaRepository>();
builder.Services.AddScoped<IPerfilUsuarioRepository, PerfilUsuarioRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


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
