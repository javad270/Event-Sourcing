using EventSourcingDemo.Abstractions;
using EventSourcingDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AzureSqlEventStore
var connectionString = builder.Configuration["AzureSql:ConnectionString"];
builder.Services.AddSingleton<IEventStore>(sp => new AzureSqlEventStore(connectionString));

var app = builder.Build();

// Enable Swagger and OpenAPI documentation in development

if (app.Environment.IsDevelopment())
{
    // Ensure Swagger middleware is available by referencing Swashbuckle.AspNetCore
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
