using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;
using Dapper.Contrib.Extensions;

namespace FluxusApi.Repositories
{
    public class ProfessionalRepository : Repository<Professional>
    {
        public override IEnumerable GetAll()
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

            try
            {
                return Connection.Query(query);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetTagNameid()
        {
            string query = @"
                SELECT 
                    Id,
                    Tag, 
                    Nameid 
                FROM 
                    Professional 
                ORDER BY 
                    Tag";

            try
            {
                return Connection.Query(query);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }


        public IEnumerable GetUserInfoBy(string userName)
        {
            string query = @"
                SELECT 
                    Id, 
                    TechnicianResponsible, 
                    LegalResponsible, 
                    UserName, 
                    UserPassword, 
                    UserActive 
                FROM 
                    Professional 
                WHERE 
                    UserName = @userName";

            try
            {
                return Connection.Query(query, new { userName });
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }

}
