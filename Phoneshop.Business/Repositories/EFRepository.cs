using Microsoft.EntityFrameworkCore;
using Phoneshop.Business.Data;
using Phoneshop.Domain.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Phoneshop.Business.Repositories
{
    [ExcludeFromCodeCoverage]
    public class EFRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DataContext _context;

        public EFRepository(DataContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var found = await GetByIdAsync(id);
            if (found != null)
            {
                _context.Set<T>().Remove(found);
                await SaveChangesAsync();
            }
        }

        public IQueryable<T> GetAll()
            => _context.Set<T>().AsNoTracking();

        public async Task<T> GetByIdAsync(int id)
            => await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task SaveChangesAsync()
            => await _context.SaveChangesAsync();

        public void Update(T entity)
            => _context.Update(entity);

    }
}
