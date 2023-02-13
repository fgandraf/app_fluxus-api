using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class ProfileRepository : Repository<Profile>
    {
        public ProfileRepository(MySqlConnection connection) : base(connection) { }

        public byte[] GetLogo()
        {
            string query = @"SELECT Logo FROM Profile WHERE Id = 1";

            var profile = _connection.QueryFirst(query);
            return (byte[])(profile.Logo);
        }


        public IEnumerable GetToPrint()
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

            return _connection.QueryFirst(query);
        }


        public string GetTradingName()
        {
            string query = @"
                SELECT 
                    TradingName 
                FROM 
                    Profile 
                WHERE 
                    Id = 1";

            var profile = _connection.QueryFirst(query);
            return profile.TradingName;
        }
    }
}
