using FluxusApi;
using FluxusApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

Configuration.JwtKey = builder.Configuration.GetValue<string>("JwtKey");

// Registers controller services
builder.Services.AddControllers();

// Extends Services to add JWT Bearer authentication with token validation
builder.Services.AddBearerAuthentication(builder.Configuration);

// Extends Services to add Dependency Injection services configuration
builder.Services.AddDatabaseServices(builder.Configuration);
builder.Services.AddRepositoryServices();

// Extends Services to add swagger configuration in development environment
//if (builder.Environment.IsDevelopment())
    builder.Services.AddSwaggerConfiguration();

// Configure Kestrel to listen on all interfaces on port 5001
//if (builder.Environment.IsProduction())
    //builder.WebHost.UseKestrel(serverOptions => serverOptions.ListenAnyIP(5001));

var app = builder.Build();

// Swagger UI configuration in development environment
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

// Configure SMTP Configurations
var smtp = new Configuration.SmtpConfiguration();
app.Configuration.GetSection("Smtp").Bind(smtp);
Configuration.Smtp = smtp;

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();