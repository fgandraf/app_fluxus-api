using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class ProfileRepository : Repository<Profile>
    {
        public byte[] GetLogo()
        {
            string query = @"SELECT Logo FROM Profile WHERE Id = 1";

            try
            {
                var profile = Connection.QueryFirst(query);
                return (byte[])(profile.Logo);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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

            try
            {
                return Connection.QueryFirst(query);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
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

            try
            {
                var profile = Connection.QueryFirst(query);
                return profile.TradingName;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
