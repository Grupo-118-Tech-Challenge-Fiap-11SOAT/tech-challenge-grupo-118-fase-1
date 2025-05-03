using System.Reflection;
using Microsoft.OpenApi.Models;

namespace TechChallengeFastFood.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
        
        //TODO: Insert Dependency Injections implementation
        
        builder.Services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Tech Challenge - Fast Food API",
                Version = "v1",
                Description = "API para gerenciamento de pedidos para lanchonete.",
                Contact = new OpenApiContact
                {
                    Name = "Grupo 118 (Inserir nomes e matricula aqui)",
                    Url = new Uri("https://github.com/Grupo-118-Tech-Challenge-Fiap-11SOAT/tech-challenge-grupo-118-fase-1")
                }
            });
        });

        builder.Services.AddHealthChecks();        
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("../swagger/v1/swagger.json", "Tech Challenge - Fast Food API");
                s.RoutePrefix = string.Empty;
                s.DocumentTitle = "Tech Challenge - Fast Food API | Swagger";
            });            

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();
        
        app.MapHealthChecks("/healthz");        

        app.Run();
    }
}