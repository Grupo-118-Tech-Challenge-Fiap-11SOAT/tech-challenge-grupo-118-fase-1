using Common.Dto.MercadoPago;
using Common.Interfaces.Customer.Controller;
using Common.Interfaces.Customer.Gateway;
using Common.Interfaces.Customer.Presenter;
using Common.Interfaces.Customer.Repositories;
using Common.Interfaces.Employee;
using Common.Interfaces.Employee.Controller;
using Common.Interfaces.Employee.Gateway;
using Common.Interfaces.Employee.Presenter;
using Common.Interfaces.Employee.Repositories;
using Common.Interfaces.Login.Gateway;
using Common.Interfaces.Order.Controller;
using Common.Interfaces.Order.Gateway;
using Common.Interfaces.Order.Presenter;
using Common.Interfaces.Order.Repositories;
using Common.Interfaces.Payments;
using Common.Interfaces.Payments.Controller;
using Common.Interfaces.Payments.Gateway;
using Common.Interfaces.Payments.Presenter;
using Common.Interfaces.Payments.Repositories;
using Common.Interfaces.Products.Controller;
using Common.Interfaces.Products.Gateway;
using Common.Interfaces.Products.Presenter;
using Common.Interfaces.Products.Repositories;
using External.Factories;
using External.Processors;
using External.Repositories.Interfaces;
using Infra.Password;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Refit;
using System.Reflection;
using System.Text.Json.Serialization;
using TechChallengeFastFood.CleanArch.API.Converters;
using TechChallengeFastFood.CleanArch.API.Filter;
using TechChallengeFastFood.CleanArch.API.Handlers;
using TechChallengeFastFood.CleanArch.Infrastructure.Database;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Customers.Repositories;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Employee.Repositories;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Order.Repositories;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Payments.Repositories;
using TechChallengeFastFood.CleanArch.Infrastructure.Database.Products.Repositories;
using TechChallengeFastFood.CleanArch.Presentation.Controllers.Payments;
using TechChallengeFastFood.CleanArch.Presentation.Controllers.Products;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Customer;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Employee;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Login;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Order;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Payment;
using TechChallengeFastFood.CleanArch.Presentation.Gateway.Products;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Customer;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Employee;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Order;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Payments;
using TechChallengeFastFood.CleanArch.Presentation.Presenters.Products;
using CustomerController = TechChallengeFastFood.CleanArch.Presentation.Controllers.Customer.CustomerController;
using EmployeeController = TechChallengeFastFood.CleanArch.Presentation.Controllers.Employee.EmployeeController;
using OrderController = TechChallengeFastFood.CleanArch.Presentation.Controllers.Order.OrderController;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace TechChallengeFastFood.CleanArch.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddExceptionHandler<CustomExceptionHandler>();
        builder.Configuration.AddEnvironmentVariables();
        builder.Services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                }
            )
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var compiledErrors = context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage)
                        .ToArray();

                    var response = new ProblemDetails
                    {
                        Type = "",
                        Title = "One or more model validation errors occurred.",
                        Detail = string.Join(" || ", compiledErrors)
                    };

                    return new BadRequestObjectResult(response);
                };
            });

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

        builder.Services.AddDbContext<CleanArchDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        //TODO: Insert Dependency Injections implementation

        builder.Services.AddTransient<IProductRepository, ProductRepository>();

        builder.Services.AddTransient<IImageProductRepository, ImageProductRepository>();

        builder.Services.AddTransient<IOrderRepository, OrderRepository>();

        builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();

        builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();

        builder.Services.AddTransient<IPaymentProcessorFactory, PaymentProcessorFactory>();
        builder.Services.AddTransient<MercadoPagoPaymentProcessor>();

        builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();


        builder.Services.AddRefitClient<IMercadoPagoRepository>().ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri(
                builder.Configuration.GetSection("MercadoPago:BaseUrl").Value
                ?? throw new ArgumentNullException("BaseUrl"));
            c.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {builder.Configuration.GetSection("MercadoPago:AccessToken").Value}");
        });

        builder.Services.AddTransient<IPasswordManager, PasswordManager>();

        builder.Services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Tech Challenge - Fast Food API - Fase 2",
                Version = "v1",
                Description =
                    "API para gerenciamento de pedidos para lanchonete usando conceitos de Clean Architecture.",
                Contact = new OpenApiContact
                {
                    Name = "Grupo 118 - Sabrina Cardoso | Tiago Koch | Tiago Oliveira | Túlio Rezende | Vinícius Nunes",
                    Url = new Uri(
                        "https://github.com/Grupo-118-Tech-Challenge-Fiap-11SOAT/tech-challenge-grupo-118-fase-1")
                }
            });

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n" +
                              "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                              "Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            s.OperationFilter<OAuthOperationsFilter>();
        });

        // Configuração JWT
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey =
                        new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        builder.Services.AddHealthChecks().AddDbContextCheck<CleanArchDbContext>();

        var app = builder.Build();
        //Clean the Standard Exception handlers to a more custom return
        app.UseExceptionHandler(_ => { });

        // Execute migrations automatically on app startup
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<CleanArchDbContext>();
            await db.Database.MigrateAsync();
        }

        // Configure the HTTP request pipeline.
        app.UseSwagger();
        app.UseSwaggerUI(s =>
        {
            s.SwaggerEndpoint("../swagger/v1/swagger.json", "Tech Challenge - Fast Food API");
            s.RoutePrefix = string.Empty;
            s.DocumentTitle = "Tech Challenge - Fast Food API - Fase 2 | Swagger";
        });

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.MapHealthChecks("/healthz");

        app.Run();
    }
}