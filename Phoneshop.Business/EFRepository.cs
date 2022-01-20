using Phoneshop.Business.Data;
using Phoneshop.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Phoneshop.Business
{
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public EFRepository(DataContext context)
        {
            _context = context;
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            SaveChanges();

            return entity;
        }

        public void Delete(int id)
        {
            var found = _context.Set<T>().FirstOrDefault(x => x.Id == id);
            if (found != null)
            {
                _context.Set<T>().Remove(found);
                _context.SaveChanges();
            }
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        //public IEnumerable<T> GetAll() 
        //    => _context.Set<T>();

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
