using FluxusApi.Repositories;
using FluxusApi.Repositories.Contracts;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

//---Configurar Kestrel para ouvir em todas as interfaces
builder.WebHost.UseKestrel(serverOptions =>
    {
        // Listen on port 5001 on all network interfaces.
        serverOptions.ListenAnyIP(5001);
    });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.OperationFilter<AddCustomHeader>(); });


//---Adicionar Singleton

builder.Services.AddScoped<MySqlConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("Default");
    return new MySqlConnection(connectionString);
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IBankBranchRepository, BankBranchRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();