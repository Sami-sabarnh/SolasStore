using Microsoft.EntityFrameworkCore;
using Solas.BL.Consts;
using Solas.BL.IRepositories;
using System.Linq.Expressions;

namespace Solas.EF.Repositories
{
    public class BaseRepositories<T> : IBaseRepositories<T> where T : class
    {
        private AppDbContext _db;
        public BaseRepositories(AppDbContext db)
        {
            _db = db;

        }
        public void Add(T model)
        {
            _db.Set<T>().Add(model);

        }

        public void Delete(int id)
        {
            var entity = _db.Set<T>().Find(id);
            if (entity != null)
            {
                _db.Set<T>().Remove(entity);
              
            }

        }
        public void Delete(Expression<Func<T, bool>> match)
        {
            var entity = _db.Set<T>().FirstOrDefault(match);
            if (entity != null)
            {
                _db.Set<T>().Remove(entity);
               // SaveData();
            }

        }
        public void Delete(List<int> ids)
        {
            foreach (var id in ids)
            {
                var entity = _db.Set<T>().Find(id);
                if (entity != null)
                {
                    _db.Set<T>().Remove(entity);
                }
            }
        }
        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>().ToList();
        }
        public async void Update(int id, T model)
        {
           

            _db.Set<T>().Update(model);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }



        public void SaveData()
        {
            _db.SaveChanges();
        }

     


        public IEnumerable<T> GetAll(string[] includes = null)
        {
            IQueryable<T> query = _db.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.ToList();
        }

        public async Task<T> Find(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query =  _db.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.Where(match).SingleOrDefault();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _db.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.Where(match).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match)
        {
            IQueryable<T> query = _db.Set<T>();

            return await query.Where(match).ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(string[] includes = null)
        {
            IQueryable<T> query = _db.Set<T>();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.ToListAsync();
        }

        public async Task<T> AddAsync(T model)
        {
            await _db.Set<T>().AddAsync(model);
            return model;
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> match, string[] includes = null, Expression<Func<T, object>> orderBy = null, string orderByDirection = "ASC")
        {
            IQueryable<T> query = _db.Set<T>().Where(match);


            if (orderBy != null)
            {
                if (orderByDirection == OrderBy.Ascending)
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }

            return query.ToList();
        }

        public async Task<IEnumerable<T>> Findbyid(Expression<Func<T, bool>> match, string[] includes = null)
        {
            IQueryable<T> query = _db.Set<T>().Where(match);
            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);
            return query.ToList();
        }

    }
}
