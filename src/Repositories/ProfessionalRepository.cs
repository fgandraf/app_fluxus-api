using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

namespace FluxusApi.Repositories
{
    public class ProfessionalRepository : Repository<Professional>
    {
        public ProfessionalRepository(MySqlConnection connection) : base(connection) { }
        
        public async Task<IEnumerable> GetIndexAsync()
        {
            string query = @"
                SELECT 
                    Id, 
                    Tag, 
                    Name, 
                    Profession, 
                    Phone1, 
                    UserActive
                FROM 
                    Professional 
                ORDER BY 
                    Tag";

            return await Connection.QueryAsync(query);
        }
        
        public async Task<IEnumerable> GetTagNameidAsync()
        {
            string query = @"
                SELECT 
                    Id,
                    Tag, 
                    CONCAT(
                        IFNULL(LEFT(Profession, 3), ''), 
                        '. ', 
                        SUBSTRING_INDEX(Name, ' ', 1),
                        ' ',
                        SUBSTRING_INDEX(SUBSTRING_INDEX(Name, ' ', -1), ' ', 1)
                        ) 
                        AS Nameid 
                FROM 
                    Professional 
                ORDER BY 
                    Tag";

            return await Connection.QueryAsync(query);
        }
        
        public async Task<IEnumerable> GetUserInfoByAsync(string userName)
        {
            string query = @"
                SELECT 
                    Id, 
                    Tag,
                    TechnicianResponsible, 
                    LegalResponsible, 
                    UserName, 
                    UserPassword, 
                    UserActive 
                FROM 
                    Professional 
                WHERE 
                    UserName = @userName";

            return await Connection.QueryFirstAsync(query, new { userName });
        }
    }

}
