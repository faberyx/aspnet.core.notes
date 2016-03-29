namespace notes_manager.Models.Repositories
{
    /// <summary>
    /// Repository Interface containing possible database actions 
    /// Interface in injected to the controller trough Unity
    /// </summary>
    /// <typeparam name="T">Entity that must be managed by the interface</typeparam>
    public interface IRepository<T> where T : class
    {
        ResultPage<T> GetPage(ResultPage<T> pagingData);
        T GetById(int id);
        void Add(T insertData);        
        T Edit(T editData); 
        void Delete(T deleteData);

    }
}