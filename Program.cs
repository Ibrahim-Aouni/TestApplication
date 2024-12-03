using Microsoft.EntityFrameworkCore;
using TestApplication.Data.Connexion;
using TestApplication.Data.DAO;
using TestApplication.Data.Interfaces.IDao;
using TestApplication.Data.Interfaces.IRepository;
using TestApplication.Data.Repository;
using TestApplication.Services;
using dotenv.net;
using TestApplication.Data.Interfaces.IService;

DotEnv.Load(); 

var builder = WebApplication.CreateBuilder(args);

string logLevel = Environment.GetEnvironmentVariable("LOGGING_DEFAULT_LOGLEVEL_INFORMATION");
string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_DEFAULT_CONNECTION");
string allowedHosts = Environment.GetEnvironmentVariable("ALLOWED_HOSTS");
string logFilePath = Environment.GetEnvironmentVariable("LOG_FILE_PATH");


builder.Configuration["Logging:LogLevel:Default"] = logLevel ?? "Information";
builder.Configuration["Logging:LogLevel:Microsoft.AspNetCore"] = Environment.GetEnvironmentVariable("LOGGING_MICROSOFT_ASPNETCORE_LOGLEVEL") ?? "Warning";
builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;
builder.Configuration["AllowedHosts"] = allowedHosts;


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString); 
});

#region Repository and DAO Configuration
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonDAO, PersonDAO>();
builder.Services.AddScoped<IPersonDAOSup40, PersonDAOSup40>();
builder.Services.AddScoped<IFileLogger, FileLogger>();
#endregion
#region Logger Configuration


FileLogger logger = new FileLogger();
#endregion

builder.Services.AddControllers();

// Swagger/OpenAPI configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});
#endregion

var app = builder.Build();
#region Middleware de gestion des erreurs globales
app.UseExceptionHandler(appError =>
{
    appError.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var errorDetails = new { message = "An error occurred while processing your request." };

        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError("Unhandled exception occurred.");

        await context.Response.WriteAsJsonAsync(errorDetails);
    });
});


#endregion

#region HTTP Pipeline Configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API v1"));
}

app.UseCors("AllowAllOrigins");

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
