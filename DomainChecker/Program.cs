using DomainChecker.Interfaces;
using DomainChecker.Middleware;
using DomainChecker.Repository;
using DomainChecker.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework Core with SQLite
builder.Services.AddDbContext<DomainContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add controllers
builder.Services.AddControllers();

// Register the DomainService with DI container
builder.Services.AddScoped<IDomainService, DomainService>();
builder.Services.AddScoped<IDomainRepository, DomainRepository>();

builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseGlobalExceptionHandling();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MigrateDatabase(app);

app.Run();

static void MigrateDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var dbContext = services.GetRequiredService<DomainContext>();

        // Check if the database exists, and ensure it's created if it does not exist
        var dbExists = dbContext.Database.CanConnect();
        if (!dbExists)
        {
            logger.LogInformation("Database does not exist. Ensuring it is created...");
            dbContext.Database.EnsureCreated();
        }

        // Apply any pending migrations
        logger.LogInformation("Applying migrations...");
        dbContext.Database.Migrate();

        logger.LogInformation("Database setup complete.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while setting up the database.");
    }
}
