﻿using MySql.Data.MySqlClient;
using FluxusApi.Entities;
using System.Collections;
using Dapper;

namespace FluxusApi.Repositories
{
    public class ProfessionalRepository : Repository<Professional>
    {
        public ProfessionalRepository(MySqlConnection connection) : base(connection) { }

        public IEnumerable GetIndex()
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

            return _connection.Query(query);
        }


        public IEnumerable GetTagNameid()
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

            return _connection.Query(query);
        }


        public IEnumerable GetUserInfoBy(string userName)
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

            return _connection.QueryFirst(query, new { userName });
        }
    }

}
