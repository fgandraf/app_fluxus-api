using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;

namespace FluxusApi.Repositories
{
    public class ProfileRepository
    {
        private string _connectionString = string.Empty;

        public ProfileRepository()
        {
            _connectionString = ConnectionString.Get();
        }


        public ArrayList GetAll()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        SELECT 
                            * 
                        FROM 
                            profile",
                        connection);

                    var reader = command.ExecuteReader();



                    if (reader.HasRows)
                    {
                        var profiles = new ArrayList();

                        if (reader.Read())
                        {
                            Profile profile = new Profile();

                            profile.Cnpj = Convert.ToString(reader["cnpj"]);
                            profile.TradingName = Convert.ToString(reader["trading_name"]);
                            profile.CompanyName = Convert.ToString(reader["company_name"]);
                            profile.StateId = Convert.ToString(reader["state_id"]);
                            profile.CityId = Convert.ToString(reader["city_id"]);
                            profile.Address = Convert.ToString(reader["address"]);
                            profile.Complement = Convert.ToString(reader["complement"]);
                            profile.District = Convert.ToString(reader["district"]);
                            profile.City = Convert.ToString(reader["city"]);
                            profile.Zip = Convert.ToString(reader["zip"]);
                            profile.State = Convert.ToString(reader["state"]);
                            profile.EstablishmentDate = Convert.ToString(reader["establishment_date"]);
                            profile.Phone1 = Convert.ToString(reader["phone1"]);
                            profile.Phone2 = Convert.ToString(reader["phone2"]);
                            profile.Email = Convert.ToString(reader["email"]);
                            profile.BankAccountName = Convert.ToString(reader["bank_account_name"]);
                            profile.BankAccountType = Convert.ToString(reader["bank_account_type"]);
                            profile.BankAccountBranch = Convert.ToString(reader["bank_account_branch"]);
                            profile.BankAccountDigit = Convert.ToString(reader["bank_account_digit"]);
                            profile.BankAccountNumber = Convert.ToString(reader["bank_account_number"]);
                            profile.ContractorName = Convert.ToString(reader["contractor_name"]);
                            profile.ContractNotice = Convert.ToString(reader["contract_notice"]);
                            profile.ContractNumber = Convert.ToString(reader["contract_number"]);
                            profile.ContractEstablished = Convert.ToString(reader["contract_established"]);
                            profile.ContractStart = Convert.ToString(reader["contract_start"]);
                            profile.ContractEnd = Convert.ToString(reader["contract_end"]);
                            profile.Logo = Convert.ToBase64String((byte[])(reader["logo"]));

                            profiles.Add(profile);
                        }

                        return profiles;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public byte[] GetLogo()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        SELECT 
                            logo 
                        FROM 
                            profile 
                        WHERE 
                            id = 1",
                        connection);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        return (byte[])(reader["logo"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public ArrayList GetToPrint()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                        SELECT 
                            cnpj, 
                            company_name, 
                            contract_notice, 
                            contract_number, 
                            logo 
                        FROM 
                            profile 
                        WHERE 
                            id = 1",
                        connection);

                    var reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        var profiles = new ArrayList();

                        if (reader.Read())
                        {
                            dynamic profile = new
                            {
                                Cnpj = Convert.ToString(reader["cnpj"]),
                                CompanyName = Convert.ToString(reader["company_name"]),
                                ContractNotice = Convert.ToString(reader["contract_notice"]),
                                ContractNumber = Convert.ToString(reader["contract_number"]),
                                Logo = (byte[])(reader["logo"])
                            };

                            profiles.Add(profile);
                        }

                        return profiles;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
        }


        public string GetTradingName()
        {
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = new MySqlCommand(@"
                    SELECT 
                        trading_name 
                    FROM 
                        profile 
                    WHERE 
                        id = 1",
                    connection);

                    var reader = command.ExecuteReader();


                    if (reader.HasRows)
                    {
                        string tradingName = null;

                        if (reader.Read())
                        {
                            tradingName = Convert.ToString(reader["trading_name"]);
                        }

                        return tradingName;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            return null;
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
                    command.Parameters.AddWithValue("@establishment_date", Util.DateOrNull(profile.EstablishmentDate));
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
                    command.Parameters.AddWithValue("@contract_established", Util.DateOrNull(profile.ContractEstablished));
                    command.Parameters.AddWithValue("@contract_start", Util.DateOrNull(profile.ContractStart));
                    command.Parameters.AddWithValue("@contract_end", Util.DateOrNull(profile.ContractEnd));
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


        public void Update(Profile dado)
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
                    command.Parameters.AddWithValue("@cnpj", dado.Cnpj);
                    command.Parameters.AddWithValue("@trading_name", dado.TradingName);
                    command.Parameters.AddWithValue("@company_name", dado.CompanyName);
                    command.Parameters.AddWithValue("@state_id", dado.StateId);
                    command.Parameters.AddWithValue("@city_id", dado.CityId);
                    command.Parameters.AddWithValue("@address", dado.Address);
                    command.Parameters.AddWithValue("@complement", dado.Complement);
                    command.Parameters.AddWithValue("@district", dado.District);
                    command.Parameters.AddWithValue("@city", dado.City);
                    command.Parameters.AddWithValue("@zip", dado.Zip);
                    command.Parameters.AddWithValue("@state", dado.State);
                    command.Parameters.AddWithValue("@establishment_date", Util.DateOrNull(dado.EstablishmentDate));
                    command.Parameters.AddWithValue("@phone1", dado.Phone1);
                    command.Parameters.AddWithValue("@phone2", dado.Phone2);
                    command.Parameters.AddWithValue("@email", dado.Email);
                    command.Parameters.AddWithValue("@bank_account_name", dado.BankAccountName);
                    command.Parameters.AddWithValue("@bank_account_type", dado.BankAccountType);
                    command.Parameters.AddWithValue("@bank_account_branch", dado.BankAccountBranch);
                    command.Parameters.AddWithValue("@bank_account_digit", dado.BankAccountDigit);
                    command.Parameters.AddWithValue("@bank_account_number", dado.BankAccountNumber);
                    command.Parameters.AddWithValue("@contractor_name", dado.ContractorName);
                    command.Parameters.AddWithValue("@contract_notice", dado.ContractNotice);
                    command.Parameters.AddWithValue("@contract_number", dado.ContractNumber);
                    command.Parameters.AddWithValue("@contract_established", Util.DateOrNull(dado.ContractEstablished));
                    command.Parameters.AddWithValue("@contract_start", Util.DateOrNull(dado.ContractStart));
                    command.Parameters.AddWithValue("@contract_end", Util.DateOrNull(dado.ContractEnd));
                    command.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(dado.Logo));

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
