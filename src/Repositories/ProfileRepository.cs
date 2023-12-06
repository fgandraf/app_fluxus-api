using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

namespace FluxusApi.Repositories
{
    public class ProfileRepository : Repository<Profile>
    {
        public ProfileRepository(MySqlConnection connection) : base(connection) { }
        
        public async Task<byte[]>  GetLogoAsync()
        {
            string query = @"SELECT Logo FROM Profile WHERE Id = 1";

            var profile = await Connection.QueryFirstAsync(query);
            return (byte[])(profile.Logo);
        }
        
        public async Task<string> UpdateLogoAsync(byte[] logoByte)
        {
            Connection.Open();
            using (var cmd = new MySqlCommand("UPDATE Profile SET Logo = @logo WHERE Id = @id", Connection))
            {
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = 1;
                cmd.Parameters.Add("@logo", MySqlDbType.Binary).Value = logoByte;
                await cmd.ExecuteNonQueryAsync();
            }
            return string.Empty;
        }   
        
        public async Task<IEnumerable> GetToPrintAsync()
        {
            string query = @"
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

            return await Connection.QueryFirstAsync(query);
        }
        
        public async Task<string> GetTradingNameAsync()
        {
            string query = @"
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
}
