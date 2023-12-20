using FluxusApi.Repositories;
using FluxusApi.Repositories.Contracts;
using MySql.Data.MySqlClient;

namespace FluxusApi;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration) 
        => services.AddScoped<MySqlConnection>(sp => 
            new MySqlConnection(configuration.GetConnectionString("Default")));

    public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
    {
        services.AddScoped<IBankBranchRepository, BankBranchRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IProfessionalRepository, ProfessionalRepository>();
        services.AddScoped<IProfileRepository, ProfileRepository>();
        services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
