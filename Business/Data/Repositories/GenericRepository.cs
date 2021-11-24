using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        #region Normal Get Methods
        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _context.Set<T>().FindAsync(id);
            return item;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            var items = await _context.Set<T>().ToListAsync();
            return items;
        }
        #endregion Normal Get Methods

        #region Get Methods with spec
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            var entity = await ApplySpecification(spec).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<IReadOnlyList<T>> GetListWithSpecAsync(ISpecification<T> spec)
        {
            var entities = await ApplySpecification(spec).ToListAsync();
            return entities;
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), spec);
        }
        #endregion Get Methods with spec
    }
}
