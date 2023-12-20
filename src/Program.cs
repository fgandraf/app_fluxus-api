using FluxusApi;

var builder = WebApplication.CreateBuilder(args);

// Configure Kestrel to listen on all interfaces on port 5001
builder.WebHost.UseKestrel(serverOptions => serverOptions.ListenAnyIP(5001));

// Adding and configuring MVC services for using Controllers
builder.Services.AddControllers();

// Swagger configuration for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.OperationFilter<AddCustomHeader>(); });

// Dependency Injection services configuration
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddRepositoryServices();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Swagger UI configuration in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();