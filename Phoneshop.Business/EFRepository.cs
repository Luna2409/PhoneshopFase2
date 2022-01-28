using Microsoft.EntityFrameworkCore;
using Phoneshop.Business.Data;
using Phoneshop.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Phoneshop.Business
{
    [ExcludeFromCodeCoverage]
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public EFRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            SaveChanges();
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
            => _context.Set<T>().AsNoTracking(); 

        public T GetById(int id)
            => _context.Set<T>().AsNoTracking().FirstOrDefault(x => x.Id == id); 

        public void SaveChanges()
            => _context.SaveChanges();
    }
}
