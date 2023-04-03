using System.Collections;
using Dapper.Contrib.Extensions;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories
{
    public class Repository<T> where T : class
    {

        protected readonly MySqlConnection _connection;


        public Repository(MySqlConnection connection)
            => _connection = connection;


        public void Insert(T model)
            => _connection.Insert<T>(model);


        public virtual void Update(T model)
            => _connection.Update<T>(model);


        public bool Delete(T model)
            => _connection.Delete<T>(model);


        public T Get(int id)
            => _connection.Get<T>(id);


        public IEnumerable GetAll()
            => _connection.GetAll<T>();

    }
}

