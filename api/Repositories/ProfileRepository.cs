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

                    var sql = new MySqlCommand(@"
                        SELECT 
                            * 
                        FROM 
                            profile",
                        connection);

                    MySqlDataReader dr = sql.ExecuteReader();



                    if (dr.HasRows)
                    {
                        var profiles = new ArrayList();

                        if (dr.Read())
                        {
                            Profile profile = new Profile();

                            profile.Cnpj = Convert.ToString(dr["cnpj"]);
                            profile.TradingName = Convert.ToString(dr["trading_name"]);
                            profile.CompanyName = Convert.ToString(dr["company_name"]);
                            profile.StateId = Convert.ToString(dr["state_id"]);
                            profile.CityId = Convert.ToString(dr["city_id"]);
                            profile.Address = Convert.ToString(dr["address"]);
                            profile.Complement = Convert.ToString(dr["complement"]);
                            profile.District = Convert.ToString(dr["district"]);
                            profile.City = Convert.ToString(dr["city"]);
                            profile.Zip = Convert.ToString(dr["zip"]);
                            profile.State = Convert.ToString(dr["state"]);
                            profile.EstablishmentDate = Convert.ToString(dr["establishment_date"]);
                            profile.Phone1 = Convert.ToString(dr["phone1"]);
                            profile.Phone2 = Convert.ToString(dr["phone2"]);
                            profile.Email = Convert.ToString(dr["email"]);
                            profile.BankAccountName = Convert.ToString(dr["bank_account_name"]);
                            profile.BankAccountType = Convert.ToString(dr["bank_account_type"]);
                            profile.BankAccountBranch = Convert.ToString(dr["bank_account_branch"]);
                            profile.BankAccountDigit = Convert.ToString(dr["bank_account_digit"]);
                            profile.BankAccountNumber = Convert.ToString(dr["bank_account_number"]);
                            profile.ContractorName = Convert.ToString(dr["contractor_name"]);
                            profile.ContractNotice = Convert.ToString(dr["contract_notice"]);
                            profile.ContractNumber = Convert.ToString(dr["contract_number"]);
                            profile.ContractEstablished = Convert.ToString(dr["contract_established"]);
                            profile.ContractStart = Convert.ToString(dr["contract_start"]);
                            profile.ContractEnd = Convert.ToString(dr["contract_end"]);
                            profile.Logo = Convert.ToBase64String((byte[])(dr["logo"]));

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

                    var sql = new MySqlCommand(@"
                        SELECT 
                            logo 
                        FROM 
                            profile 
                        WHERE 
                            id = 1",
                        connection);

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();

                        return (byte[])(dr["logo"]);
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

                    var sql = new MySqlCommand(@"
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

                    MySqlDataReader dr = sql.ExecuteReader();

                    if (dr.HasRows)
                    {
                        var profiles = new ArrayList();

                        if (dr.Read())
                        {
                            dynamic profile = new
                            {
                                Cnpj = Convert.ToString(dr["cnpj"]),
                                CompanyName = Convert.ToString(dr["company_name"]),
                                ContractNotice = Convert.ToString(dr["contract_notice"]),
                                ContractNumber = Convert.ToString(dr["contract_number"]),
                                Logo = (byte[])(dr["logo"])
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

                    var sql = new MySqlCommand(@"
                    SELECT 
                        trading_name 
                    FROM 
                        profile 
                    WHERE 
                        id = 1",
                    connection);

                    MySqlDataReader dr = sql.ExecuteReader();


                    if (dr.HasRows)
                    {
                        string tradingName = null;

                        if (dr.Read())
                        {
                            tradingName = Convert.ToString(dr["trading_name"]);
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

                    var sql = new MySqlCommand(@"
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

                    sql.Parameters.AddWithValue("@id", "1");
                    sql.Parameters.AddWithValue("@cnpj", profile.Cnpj);
                    sql.Parameters.AddWithValue("@trading_name", profile.TradingName);
                    sql.Parameters.AddWithValue("@company_name", profile.CompanyName);
                    sql.Parameters.AddWithValue("@state_id", profile.StateId);
                    sql.Parameters.AddWithValue("@city_id", profile.CityId);
                    sql.Parameters.AddWithValue("@address", profile.Address);
                    sql.Parameters.AddWithValue("@complement", profile.Complement);
                    sql.Parameters.AddWithValue("@district", profile.District);
                    sql.Parameters.AddWithValue("@city", profile.City);
                    sql.Parameters.AddWithValue("@zip", profile.Zip);
                    sql.Parameters.AddWithValue("@state", profile.State);
                    sql.Parameters.AddWithValue("@establishment_date", Util.DateOrNull(profile.EstablishmentDate));
                    sql.Parameters.AddWithValue("@phone1", profile.Phone1);
                    sql.Parameters.AddWithValue("@phone2", profile.Phone1);
                    sql.Parameters.AddWithValue("@email", profile.Email);
                    sql.Parameters.AddWithValue("@bank_account_name", profile.BankAccountName);
                    sql.Parameters.AddWithValue("@bank_account_type", profile.BankAccountType);
                    sql.Parameters.AddWithValue("@bank_account_branch", profile.BankAccountBranch);
                    sql.Parameters.AddWithValue("@bank_account_digit", profile.BankAccountDigit);
                    sql.Parameters.AddWithValue("@bank_account_number", profile.BankAccountNumber);
                    sql.Parameters.AddWithValue("@contractor_name", profile.ContractorName);
                    sql.Parameters.AddWithValue("@contract_notice", profile.ContractNotice);
                    sql.Parameters.AddWithValue("@contract_number", profile.ContractNumber);
                    sql.Parameters.AddWithValue("@contract_established", Util.DateOrNull(profile.ContractEstablished));
                    sql.Parameters.AddWithValue("@contract_start", Util.DateOrNull(profile.ContractStart));
                    sql.Parameters.AddWithValue("@contract_end", Util.DateOrNull(profile.ContractEnd));
                    sql.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(profile.Logo));

                    sql.ExecuteNonQuery();

                    return sql.LastInsertedId;
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

                    var sql = new MySqlCommand(@"
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

                    sql.Parameters.AddWithValue("@id", "1");
                    sql.Parameters.AddWithValue("@cnpj", dado.Cnpj);
                    sql.Parameters.AddWithValue("@trading_name", dado.TradingName);
                    sql.Parameters.AddWithValue("@company_name", dado.CompanyName);
                    sql.Parameters.AddWithValue("@state_id", dado.StateId);
                    sql.Parameters.AddWithValue("@city_id", dado.CityId);
                    sql.Parameters.AddWithValue("@address", dado.Address);
                    sql.Parameters.AddWithValue("@complement", dado.Complement);
                    sql.Parameters.AddWithValue("@district", dado.District);
                    sql.Parameters.AddWithValue("@city", dado.City);
                    sql.Parameters.AddWithValue("@zip", dado.Zip);
                    sql.Parameters.AddWithValue("@state", dado.State);
                    sql.Parameters.AddWithValue("@establishment_date", Util.DateOrNull(dado.EstablishmentDate));
                    sql.Parameters.AddWithValue("@phone1", dado.Phone1);
                    sql.Parameters.AddWithValue("@phone2", dado.Phone2);
                    sql.Parameters.AddWithValue("@email", dado.Email);
                    sql.Parameters.AddWithValue("@bank_account_name", dado.BankAccountName);
                    sql.Parameters.AddWithValue("@bank_account_type", dado.BankAccountType);
                    sql.Parameters.AddWithValue("@bank_account_branch", dado.BankAccountBranch);
                    sql.Parameters.AddWithValue("@bank_account_digit", dado.BankAccountDigit);
                    sql.Parameters.AddWithValue("@bank_account_number", dado.BankAccountNumber);
                    sql.Parameters.AddWithValue("@contractor_name", dado.ContractorName);
                    sql.Parameters.AddWithValue("@contract_notice", dado.ContractNotice);
                    sql.Parameters.AddWithValue("@contract_number", dado.ContractNumber);
                    sql.Parameters.AddWithValue("@contract_established", Util.DateOrNull(dado.ContractEstablished));
                    sql.Parameters.AddWithValue("@contract_start", Util.DateOrNull(dado.ContractStart));
                    sql.Parameters.AddWithValue("@contract_end", Util.DateOrNull(dado.ContractEnd));
                    sql.Parameters.AddWithValue("@logo", (byte[])Convert.FromBase64String(dado.Logo));

                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
