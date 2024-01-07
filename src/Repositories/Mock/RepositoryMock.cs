using System.Collections;

namespace FluxusApi.Repositories.Mock;

public abstract class RepositoryMock<T> where T : class 
{
    public static List<T> Repository { get; set; } = new();

    public Task<long> InsertAsync(T model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        var id = ((dynamic)model).Id;
        if (id == null || (id is int && id == 0) || (id is string && string.IsNullOrEmpty(id)))
            throw new ArgumentException("Id não pode ser nulo ou vazio", nameof(model));
        
        if (Repository.Exists(x => Equals(((dynamic)x).Id == id)))
            throw new InvalidOperationException("Entidade com o mesmo ID já existe.");
        
        Repository.Add(model);
        return Task.FromResult(Convert.ToInt64(id));
    }
        
        
    public Task<bool> UpdateAsync(T model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));
        
        var idProperty = typeof(T).GetProperty("Id")!;
        var idFromModel = idProperty.GetValue(model);
        var entity = Repository.SingleOrDefault(x => Equals( idProperty.GetValue(x), idFromModel) );
        
        if (entity == null)
            throw new KeyNotFoundException();

        foreach (var property in typeof(T).GetProperties())
        {
            if (property.Name != "Id")
            {
                var value = property.GetValue(model);
                property.SetValue(entity, value);
            }
        }
        
        return Task.FromResult(true);
    }
    
    public Task<T> GetAsync(int id)
    {
        var idProperty = typeof(T).GetProperty("Id")!;
        var entity = Repository.SingleOrDefault(x => (long)idProperty.GetValue(x)! == id );
        if (entity == null)
            throw new KeyNotFoundException();

        return Task.FromResult(entity);
    }
    
    public Task<T> GetAsync(string id)
    {
        var idProperty = typeof(T).GetProperty("Id")!;
        var entity = Repository.SingleOrDefault(x => Equals(  (string)idProperty.GetValue(x)!, id )  );
        if (entity == null)
            throw new KeyNotFoundException();

        return Task.FromResult(entity);
    }
    
    public Task<bool> DeleteAsync(T model)
    {
        var idProperty = typeof(T).GetProperty("Id")!;
        var idFromModel = idProperty.GetValue(model);
        
        if (idFromModel == null || (idFromModel is long longId && longId == 0) || (idFromModel is string stringId && string.IsNullOrEmpty(stringId)))
            throw new ArgumentException("Id não pode ser nulo ou vazio", nameof(model));

        T entity;
        if (idFromModel is long)
            entity = Repository.SingleOrDefault(x => (long)idProperty.GetValue(x)! == (long)idFromModel );
        else
            entity = Repository.SingleOrDefault(x => (string)idProperty.GetValue(x)! == (string)idFromModel );
        
        if (entity == null)
            throw new KeyNotFoundException();

        Repository.Remove(entity);
        return Task.FromResult(true);
    }
    
    
    public Task<List<T>> GetAllAsync()
        => Task.FromResult(Repository.ToList());
}