using Knila.BAL.Implementations;
using Knila.BAL.Interface;
using Knila.DAL.Models;
using Knila_Assessment.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();
Log.Information("Logger initialized");
var swaggerVersion =
              $"Build - {File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location):MM.dd.yyyy.HH.mm}";
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.Services.AddDbContext<KnilaDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var testHost = new HostBuilder()
    .ConfigureServices(services =>
    {
        services.AddDbContext<KnilaDbContext>(options =>
            options.UseInMemoryDatabase("TestDatabase"));

        services.AddScoped<IContactRepository, ContactRepository>();
    })
    .Build();

var context = testHost.Services.GetRequiredService<KnilaDbContext>();



builder.Services.AddScoped<IContactRepository, ContactRepository>();






builder.Services.AddScoped<AuthenticationController>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();

    return new AuthenticationController(
        configuration,
        provider.GetRequiredService<KnilaDbContext>(),
        httpClientFactory
    );
});


builder.Services.AddScoped<ContactController, ContactController>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
    var customTimeoutClient = httpClientFactory.CreateClient("CustomTimeoutClient");

    return new ContactController(
        provider.GetRequiredService<IContactRepository>(),  
        configuration
    );
});


builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


builder.Services.AddHttpClient("CustomTimeoutClient", client =>
{
    client.Timeout = TimeSpan.FromMinutes(2);
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Knila API", Version = $"Knila API  {swaggerVersion}" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authentication",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Host.UseSerilog((context, services, loggerConfig) =>
{
    loggerConfig
        .MinimumLevel.Debug()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .Enrich.FromLogContext()
        .WriteTo.Async(wt => wt.Console())
        .WriteTo.Async(wt => wt.File("ExdionLogs\\Exdion-log.txt",
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 7,
            rollOnFileSizeLimit: true,
            fileSizeLimitBytes: 1_048_576,
            shared: true));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", $"EXDION API {swaggerVersion}");
    });
}

app.UseCors(option =>
    option.SetIsOriginAllowed((host) => true).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

app.UseHttpsRedirection();

// Enable response compression
app.UseResponseCompression();

app.UseRouting();
app.UseAuthentication();  // Add this for JWT authentication
app.UseAuthorization();
app.MapControllers();


app.Run();

