namespace job_app_management_system.api.Services.Interfaces
{
    public interface IService<T>
    {
        public List<T> GetAll();
        public T GetByID(int id);
        public bool Add(T entity);
        public T Remove(T entity);
        public bool RemoveAll();
        public T Update(T entity);
    }
}
