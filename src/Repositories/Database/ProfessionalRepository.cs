﻿using System.Collections;
using Dapper;
using FluxusApi.Models.DTO;
using FluxusApi.Repositories.Contracts;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories.Database;

public class ProfessionalRepository : Repository<ProfessionalDTO>, IProfessionalRepository
{
    public ProfessionalRepository(MySqlConnection connection) : base(connection) { }
        
    public async Task<IEnumerable> GetIndexAsync()
    {
        const string query = @"
                SELECT 
                    Id, 
                    Tag, 
                    Name, 
                    Profession, 
                    Phone1
                FROM 
                    Professional 
                ORDER BY 
                    Tag";

        return await Connection.QueryAsync(query);
    }
        
    public async Task<IEnumerable> GetTagNameidAsync()
    {
        const string query = @"
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

}