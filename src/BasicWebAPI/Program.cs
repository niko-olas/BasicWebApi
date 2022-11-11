using System.Text.Json.Serialization;
using TinyHelpers.Json.Serialization;
using Serilog;
using BasicWebApi.BusinessLayer.MapperProfiles;
using BasicWebApi.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using BasicWebApi.BusinessLayer.Services;
using FluentValidation.AspNetCore;
using BasicWebApi.BusinessLayer.Validation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using SimpleAuthentication;
using OperationResults.AspNetCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
     options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
 });


builder.Services.AddSimpleAuthentication(builder.Configuration);

// Operation Result
builder.Services.AddOperationResult(options =>
{
    options.ErrorResponseFormat = ErrorResponseFormat.Default;
},
updateModelStateResponseFactory: true,
validationErrorDefaultMessage: "Errors occurred");

// Mapper
builder.Services.AddAutoMapper(typeof(OrderMapperProfile).Assembly);

//FluentValidation
builder.Services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<SaveOrderRequest>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer();
builder.Services
    .AddSwaggerGen(options =>
{
    options.AddSimpleAuthentication(builder.Configuration);

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Basic Web", Version = "v1" });


    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });

    options.MapType<DateTime>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date-time",
        Example = new OpenApiString(new DateTime(2022, 04, 08, 16, 22, 0).ToString("yyyy-MM-ddTHH:mm:ssZ"))
    });

    options.UseAllOfToExtendReferenceSchemas();

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
})
    .AddFluentValidationRulesToSwagger(options =>
    {
        options.SetNotNullableIfMinLengthGreaterThenZero = true;
    });



// DBContext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), sqlOptions =>
    {
        //sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
    });
});
//builder.Services.AddScoped<IReadOnlyDataContext>(services => services.GetRequiredService<DataContext>());
builder.Services.AddScoped<IDataContext>(services => services.GetRequiredService<DataContext>());


//Service
builder.Services.Scan(scan => scan.FromAssemblyOf<OrderService>()
    .AddClasses(classes => classes.InNamespaceOf<OrderService>())
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);


builder.Services.AddProblemDetails(options =>
{
    options.Map<Exception>(ex =>
    new StatusCodeProblemDetails(StatusCodes.Status503ServiceUnavailable)
    {
        Title = "Services Unavailable"
    });
});



var app = builder.Build();

app.UseHttpsRedirection();

app.UseProblemDetails();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = string.Empty;
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Basic API");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  
}

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});

app.UseAuthenticationAndAuthorization();

app.UseAuthorization();

app.MapControllers();

app.Run();
