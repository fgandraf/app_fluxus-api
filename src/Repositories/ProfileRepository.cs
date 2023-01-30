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


        public long Insert(Profile profile)
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        INSERT INTO profile
                            (id, cnpj, trading_name, company_name, state_id, city_id, address, 
                            complement, district, city, zip, state, establishment_date, phone1, 
                            phone2, email, bank_account_name, bank_account_type, bank_account_branch, 
                            bank_account_digit, bank_account_number, contractor_name, contract_notice, 
                            contract_number, contract_established, contract_start, contract_end, logo) 
                        VALUES 
                            (@id, @cnpj, @trading_name, @company_name, @state_id, @city_id, @address, 
                            @complement, @district, @city, @zip, @state, @establishment_date, @phone1, 
                            @phone2, @email, @bank_account_name, @bank_account_type, @bank_account_branch, 
                            @bank_account_digit, @bank_account_number, @contractor_name, @contract_notice, 
                            @contract_number, @contract_established, @contract_start, @contract_end, @logo)",
                        connection);

                    command.Parameters.AddWithValue("@id", "1");
                    command.Parameters.AddWithValue("@cnpj", profile.Cnpj);
                    command.Parameters.AddWithValue("@trading_name", profile.TradingName);
                    command.Parameters.AddWithValue("@company_name", profile.CompanyName);
                    command.Parameters.AddWithValue("@state_id", profile.StateId);
                    command.Parameters.AddWithValue("@city_id", profile.CityId);
                    command.Parameters.AddWithValue("@address", profile.Address);
                    command.Parameters.AddWithValue("@complement", profile.Complement);
                    command.Parameters.AddWithValue("@district", profile.District);
                    command.Parameters.AddWithValue("@city", profile.City);
                    command.Parameters.AddWithValue("@zip", profile.Zip);
                    command.Parameters.AddWithValue("@state", profile.State);
                    command.Parameters.AddWithValue("@establishment_date", profile.EstablishmentDate);
                    command.Parameters.AddWithValue("@phone1", profile.Phone1);
                    command.Parameters.AddWithValue("@phone2", profile.Phone1);
                    command.Parameters.AddWithValue("@email", profile.Email);
                    command.Parameters.AddWithValue("@bank_account_name", profile.BankAccountName);
                    command.Parameters.AddWithValue("@bank_account_type", profile.BankAccountType);
                    command.Parameters.AddWithValue("@bank_account_branch", profile.BankAccountBranch);
                    command.Parameters.AddWithValue("@bank_account_digit", profile.BankAccountDigit);
                    command.Parameters.AddWithValue("@bank_account_number", profile.BankAccountNumber);
                    command.Parameters.AddWithValue("@contractor_name", profile.ContractorName);
                    command.Parameters.AddWithValue("@contract_notice", profile.ContractNotice);
                    command.Parameters.AddWithValue("@contract_number", profile.ContractNumber);
                    command.Parameters.AddWithValue("@contract_established", profile.ContractEstablished);
                    command.Parameters.AddWithValue("@contract_start", profile.ContractStart);
                    command.Parameters.AddWithValue("@contract_end", profile.ContractEnd);
                    command.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(profile.Logo));

                    command.ExecuteNonQuery();

                    return command.LastInsertedId;
                }
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
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        UPDATE 
                            profile 
                        SET 
                            cnpj = @cnpj, 
                            trading_name = @trading_name, 
                            company_name = @company_name, 
                            state_id = @state_id, 
                            city_id = @city_id, 
                            address = @address, 
                            complement = @complement, 
                            district = @district, 
                            city = @city, 
                            zip = @zip, 
                            state = @state, 
                            establishment_date = @establishment_date, 
                            phone1 = @phone1, 
                            phone2 = @phone2, 
                            email = @email, 
                            bank_account_name = @bank_account_name, 
                            bank_account_type = @bank_account_type, 
                            bank_account_branch = @bank_account_branch, 
                            bank_account_digit = @bank_account_digit, 
                            bank_account_number = @bank_account_number, 
                            contractor_name = @contractor_name, 
                            contract_notice = @contract_notice, 
                            contract_number = @contract_number, 
                            contract_established = @contract_established, 
                            contract_start = @contract_start, 
                            contract_end = @contract_end, 
                            logo = @logo 
                        WHERE 
                            id = @id", 
                        connection);

                    command.Parameters.AddWithValue("@id", "1");
                    command.Parameters.AddWithValue("@cnpj", profile.Cnpj);
                    command.Parameters.AddWithValue("@trading_name", profile.TradingName);
                    command.Parameters.AddWithValue("@company_name", profile.CompanyName);
                    command.Parameters.AddWithValue("@state_id", profile.StateId);
                    command.Parameters.AddWithValue("@city_id", profile.CityId);
                    command.Parameters.AddWithValue("@address", profile.Address);
                    command.Parameters.AddWithValue("@complement", profile.Complement);
                    command.Parameters.AddWithValue("@district", profile.District);
                    command.Parameters.AddWithValue("@city", profile.City);
                    command.Parameters.AddWithValue("@zip", profile.Zip);
                    command.Parameters.AddWithValue("@state", profile.State);
                    command.Parameters.AddWithValue("@establishment_date", profile.EstablishmentDate);
                    command.Parameters.AddWithValue("@phone1", profile.Phone1);
                    command.Parameters.AddWithValue("@phone2", profile.Phone2);
                    command.Parameters.AddWithValue("@email", profile.Email);
                    command.Parameters.AddWithValue("@bank_account_name", profile.BankAccountName);
                    command.Parameters.AddWithValue("@bank_account_type", profile.BankAccountType);
                    command.Parameters.AddWithValue("@bank_account_branch", profile.BankAccountBranch);
                    command.Parameters.AddWithValue("@bank_account_digit", profile.BankAccountDigit);
                    command.Parameters.AddWithValue("@bank_account_number", profile.BankAccountNumber);
                    command.Parameters.AddWithValue("@contractor_name", profile.ContractorName);
                    command.Parameters.AddWithValue("@contract_notice", profile.ContractNotice);
                    command.Parameters.AddWithValue("@contract_number", profile.ContractNumber);
                    command.Parameters.AddWithValue("@contract_established", profile.ContractEstablished);
                    command.Parameters.AddWithValue("@contract_start", profile.ContractStart);
                    command.Parameters.AddWithValue("@contract_end", profile.ContractEnd);
                    command.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(profile.Logo));

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
