using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

namespace FluxusApi.Repositories
{
    public class ProfileRepository
    {
        private string _connectionString = string.Empty;

        public ProfileRepository()
        {
            _connectionString = ConnectionString.Get();
        }


        public IEnumerable GetAll()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var profile = connection.QueryFirst(@"
                        SELECT 
                            *
                        FROM 
                            Profile
                        WHERE
                            Id = 1");
                    return profile;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public byte[] GetLogo()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var profile = connection.QueryFirst(@"
                        SELECT 
                            Logo 
                        FROM 
                            Profile 
                        WHERE 
                            Id = 1");
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
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var profile = connection.QueryFirst(@"
                        SELECT 
                            Cnpj, 
                            CompanyName, 
                            ContractNotice, 
                            ContractNumber, 
                            Logo 
                        FROM 
                            Profile 
                        WHERE 
                            Id = 1");
                        return profile;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public string GetTradingName()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    var profile = connection.QueryFirst(@"
                        SELECT 
                            TradingName 
                        FROM 
                            Profile 
                        WHERE 
                            Id = 1");
                    return profile.TradingName;
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Insert(Profile profile)
        {
            try
            {
                string insertSQL = @"
                    INSERT INTO Profile
                        (Id, Cnpj, TradingName, CompanyName, StateId, CityId, Address,
                        Complement, District, City, Zip, State, EstablishmentDate, Phone1,
                        Phone2, Email, BankAccountName, BankAccountType, BankAccountBranch, 
                        BankAccountDigit, BankAccountNumber, ContractorName, ContractNotice,
                        ContractNumber, ContractEstablished, ContractStart, ContractEnd, Logo) 
                    VALUES 
                        (1, @Cnpj, @TradingName, @CompanyName, @StateId, @CityId, @Address, 
                        @Complement, @District, @City, @Zip, @State, @EstablishmentDate, @Phone1, 
                        @Phone2, @Email, @BankAccountName, @BankAccountType, @BankAccountBranch, 
                        @BankAccountDigit, @BankAccountNumber, @ContractorName, @ContractNotice, 
                        @ContractNumber, @ContractEstablished, @ContractStart, @ContractEnd, @Logo)";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(insertSQL, profile);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public int Update(Profile profile)
        {
            try
            {
                string updateSQL = @"
                    UPDATE 
                        Profile 
                    SET 
                        Cnpj = @Cnpj, 
                        TradingName = @TradingName, 
                        CompanyName = @CompanyName, 
                        StateId = @StateId, 
                        CityId = @CityId, 
                        Address = @Address, 
                        Complement = @Complement, 
                        District = @District, 
                        City = @City, 
                        Zip = @Zip, 
                        State = @State, 
                        EstablishmentDate = @EstablishmentDate, 
                        Phone1 = @Phone1, 
                        Phone2 = @Phone2, 
                        Email = @Email, 
                        BankAccountName = @BankAccountName, 
                        BankAccountType = @BankAccountType, 
                        BankAccountBranch = @BankAccountBranch, 
                        BankAccountDigit = @BankAccountDigit, 
                        BankAccountNumber = @BankAccountNumber, 
                        ContractorName = @ContractorName, 
                        ContractNotice = @ContractNotice, 
                        ContractNumber = @ContractNumber, 
                        ContractEstablished = @ContractEstablished, 
                        ContractStart = @ContractStart, 
                        ContractEnd = @ContractEnd, 
                        Logo = @Logo 
                    WHERE 
                        Id = 1";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    return connection.Execute(updateSQL, profile);
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
