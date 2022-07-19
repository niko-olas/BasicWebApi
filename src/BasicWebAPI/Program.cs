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

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
});


// Add services to the container.
builder.Services.AddControllers()
.AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
     options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
 });

// Mapper
builder.Services.AddAutoMapper(typeof(OrderMapperProfile).Assembly);

//FluentValidation
builder.Services.AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssemblyContaining<SaveOrderRequest>();
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen()
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
builder.Services.AddScoped<IReadOnlyDataContext>(services => services.GetRequiredService<DataContext>());
builder.Services.AddScoped<IDataContext>(services => services.GetRequiredService<DataContext>());



//Service
builder.Services.Scan(scan => scan.FromAssemblyOf<OrderService>()
    .AddClasses(classes => classes.InNamespaceOf<OrderService>())
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);


builder.Services.AddProblemDetails(options =>
{
    options.Map<ApplicationException>(ex =>
    new StatusCodeProblemDetails(StatusCodes.Status503ServiceUnavailable)
    {
        Title = "Services Unavailable"
    });
});



var app = builder.Build();

app.UseHttpsRedirection();

app.UseProblemDetails();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging(options =>
{
    options.IncludeQueryInRequestPath = true;
});

app.UseAuthorization();

app.MapControllers();

app.Run();
