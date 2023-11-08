var builder = WebApplication.CreateBuilder(args);

//---Configurar Kestrel para ouvir em todas as interfaces
builder.WebHost.UseKestrel(serverOptions =>
    {
        // Listen on port 5001 on all network interfaces.
        serverOptions.ListenAnyIP(5001);
    })
    .UseIISIntegration();


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//---Adicionar parâmetro de cabeçalho no Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<AddCustomHeader>();
});

//---Adicionar Singleton
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 

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