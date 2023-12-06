using System.Collections;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories
{
    public class Repository<T> where T : class
    {

        protected readonly MySqlConnection Connection;


        protected Repository(MySqlConnection connection)
            => Connection = connection;

        
        public async Task<long> InsertAsync(T model)
            => await Connection.InsertAsync<T>(model);
        
        
        public virtual async Task<bool> UpdateAsync(T model)
            => await Connection.UpdateAsync<T>(model);
        
        
        public async Task<bool> DeleteAsync(T model)
            => await Connection.DeleteAsync<T>(model);
        
        
        public async Task<T> GetAsync(int id)
            => await Connection.GetAsync<T>(id);
        
        public async Task<T> GetAsync(string id)
            => await Connection.GetAsync<T>(id);
        
        public async Task<IEnumerable> GetAllAsync()
            => await Connection.GetAllAsync<T>();

    }
}

