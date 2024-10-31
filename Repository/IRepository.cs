using MappingApp.Interfaces;
using MappingApp.Models;

namespace MappingApp.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Response<List<T>> GetAll();
        Response<T> GetById(int id);
        Response<T> Add(T entity);
        Response<T> Update(T entity);
        Response<bool> Delete(int id);
    }
}
