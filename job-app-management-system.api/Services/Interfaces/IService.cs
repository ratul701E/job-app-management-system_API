using job_app_management_system.api.Result;

namespace job_app_management_system.api.Services.Interfaces
{
    public interface IService<T>
    {
        public Result<List<T>> GetAll();
        public Result<T> GetByID(long id);
        public Result<bool> Add(T entity);
        public Result<T> Remove(T entity);
        public Result<bool> RemoveAll();
        public Result<T> Update(T entity);
    }
}
