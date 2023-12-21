using FluxusApi;
using FluxusApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Configure Kestrel to listen on all interfaces on port 5001
if (builder.Environment.IsProduction())
    builder.WebHost.UseKestrel(serverOptions => serverOptions.ListenAnyIP(5001));

// Dependency Injection services configuration
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddRepositoryServices();

// Swagger configuration in development environment
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c => c.OperationFilter<AddCustomHeader>());
}

var app = builder.Build();

// Swagger UI configuration in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();