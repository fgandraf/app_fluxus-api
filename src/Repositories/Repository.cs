using System;
using System.Collections;
using Dapper.Contrib.Extensions;
using FluxusApi.Entities;
using MySql.Data.MySqlClient;

namespace FluxusApi.Repositories
{
    public class Repository<T> where T : class
    {

        protected readonly MySqlConnection Connection;


        public Repository()
            => Connection = new MySqlConnection(ConnectionString.Get());


        public void Insert(T model)
            => Connection.Insert<T>(model);


        public void Update(T model)
            => Connection.Update<T>(model);


        public bool Delete(T model)
            => Connection.Delete<T>(model);


        public T Get(int id)
            => Connection.Get<T>(id);


        public virtual IEnumerable GetAll()
            => Connection.GetAll<T>();

    }
}

