using Application;
using Application.Employee;
using Domain.Employee.Ports.In;
using Domain.Employee.Ports.Out;
using System.Reflection;
using System.Text.Json.Serialization;
using Application.Products;
using Domain.Products.Ports.In;
using Domain.Products.Ports.Out;
using Domain.Payments.Ports.In;
using Domain.Payments.Ports.Out;
using Application.Payments;
using Infra.Database.SqlServer;
using Infra.Database.SqlServer.Employee.Repositories;
using Infra.Database.SqlServer.Products.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Infra.Api.MercadoPago.Payments.Options;
using Infra.Api.MercadoPago.Payments.Repositories.Interfaces;
using Infra.Api.MercadoPago.Payments.Processors;
using Infra.Api.MercadoPago.Payments.Factories;
using Infra.Database.SqlServer.Payments.Repositories;
using Refit;

namespace TechChallengeFastFood.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Configuration.AddEnvironmentVariables();
        builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        //Uso via variavel de ambiente (Double underscore para representar o nível): ConnectionStrings__DefaultConnection

        builder.Services.Configure<MercadoPagoOptions>(builder.Configuration.GetSection("MercadoPago"));

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        //TODO: Insert Dependency Injections implementation
        builder.Services.AddTransient<IProductManager, ProductManager>();
        builder.Services.AddTransient<IProductRepository, ProductRepository>();

        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        builder.Services.AddTransient<IEmployeeManager, EmployeeManager>();

        builder.Services.AddTransient<IPaymentManager, PaymentManager>();
        builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
        builder.Services.AddTransient<IPaymentProcessorFactory, PaymentProcessorFactory>();
        builder.Services.AddTransient<MercadoPagoPaymentProcessor>();

        builder.Services.AddRefitClient<IMercadoPagoRepository>().ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(
                builder.Configuration.GetSection("MercadoPago:BaseUrl").Value
                ?? throw new ArgumentNullException("BaseUrl"));
            c.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {builder.Configuration.GetSection("MercadoPago:AccessToken").Value}");
        });

        builder.Services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Tech Challenge - Fast Food API",
                Version = "v1",
                Description = "API para gerenciamento de pedidos para lanchonete.",
                Contact = new OpenApiContact
                {
                    Name = "Grupo 118 (Inserir nomes e matricula aqui).",
                    Url = new Uri(
                        "https://github.com/Grupo-118-Tech-Challenge-Fiap-11SOAT/tech-challenge-grupo-118-fase-1")
                }
            });
        });

        builder.Services.AddHealthChecks().AddDbContextCheck<AppDbContext>();

        var app = builder.Build();

        // Execute migrations automatically on app startup
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await db.Database.MigrateAsync();
        }

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