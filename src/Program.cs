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
builder.Services.AddRepositoryServices(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

// For staging environment, configure Kestrel to listen on all interfaces on port 5001
//builder.WebHost.UseKestrel(serverOptions => serverOptions.ListenAnyIP(5001));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure SMTP Configurations
var smtp = new Configuration.SmtpConfiguration();
app.Configuration.GetSection("Smtp").Bind(smtp);
Configuration.Smtp = smtp;

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();