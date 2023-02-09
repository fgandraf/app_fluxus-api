using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class ProfileRepository
    {
        private string _connectionString = string.Empty;

        public ProfileRepository()
            => _connectionString = ConnectionString.Get();


        public IEnumerable GetAll()
        {
            string query = @"
                SELECT 
                    *
                FROM 
                    Profile
                WHERE
                    Id = 1";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.Query(query);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public byte[] GetLogo()
        {
            string query = @"
                SELECT 
                    Logo 
                FROM 
                    Profile 
                WHERE 
                    Id = 1";

            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var profile = connection.QueryFirst(query);
                    return (byte[])(profile.Logo);
                }
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
                using (var connection = new MySqlConnection(_connectionString))
                    return connection.QueryFirst(query);
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
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var profile = connection.QueryFirst(query);
                    return profile.TradingName;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Insert(Profile profile)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    connection.Insert<Profile>(profile);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public void Update(Profile profile)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                    connection.Update<Profile>(profile);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
