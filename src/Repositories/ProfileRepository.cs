using MySql.Data.MySqlClient;
using FluxusApi.Models;
using System.Collections;
using Dapper;
using FluxusApi.Repositories.Contracts;
using FluxusApi.ViewModels;

namespace FluxusApi.Repositories;

public class ProfileRepository : Repository<Profile>, IProfileRepository
{
    public ProfileRepository(MySqlConnection connection) : base(connection) { }
        
    public async Task<byte[]>  GetLogoAsync()
    {
        const string query = @"SELECT Logo FROM Profile WHERE Id = 1";

        var profile = await Connection.QueryFirstAsync(query);
        return (byte[])(profile.Logo);
    }
    
    public async Task<ProfileToPrintViewModel> GetToPrintAsync()
    {
        const string query = @"
                SELECT 
                    Cnpj, 
                    CompanyName, 
                    ContractNotice, 
                    ContractNumber,
                    Logo 
                FROM 
                    Profile 
                WHERE 
                    Id = 1";

        return await Connection.QueryFirstAsync<ProfileToPrintViewModel>(query);
    }
        
    public async Task<string> GetTradingNameAsync()
    {
        const string query = @"
                SELECT 
                    TradingName 
                FROM 
                    Profile 
                WHERE 
                    Id = 1";

        var profile = await Connection.QueryFirstAsync(query);
        return profile.TradingName;
    }
}