using Solas.BL.Consts;
using System.Linq.Expressions;

namespace Solas.BL.IRepositories
{
    public interface IBaseRepositories<T> where T : class
    {
        void Add(T model);
        Task<T> AddAsync(T model);
        void Delete(int id);
        void Delete(Expression<Func<T, bool>> match);
        public void Delete(List<int> ids);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(string[] includes = null);



        void Update(int id, T model);
        //Async Methods
        Task<T> GetByIdAsync(int id);

        void SaveData();

        Task<T> Find(Expression<Func<T, bool>> match, string[] includes = null);

        Task<IEnumerable<T>> Findbyid(Expression<Func<T, bool>> match, string[] includes = null);
        // public IQueryable<T> Find(Expression<Func<T, bool>> match, string[] includes = null)


        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = OrderBy.Ascending);

        Task<IEnumerable<T>> GetAllAsync(string[] includes = null);



    }




}

