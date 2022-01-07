using App.Common.Extensions;
using App.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Data.Repositories.BaseRepository
{
    public class BaseRepository<TEntity, TDTO> : IBaseRepository<TEntity, TDTO> where TEntity : class
    {
        internal readonly ApplicationDBContext _context;
        internal readonly IMapper _mapper;
        private DbSet<TEntity> _entities;

        public BaseRepository(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _entities = context.Set<TEntity>();
            _mapper = mapper;
        }

        internal virtual IQueryable<TEntity> Table
        {
            get
            {
                return _entities;
            }
        }

        internal virtual IQueryable<TEntity> TableUntracked
        {
            get
            {
                return this._entities.AsNoTracking();
            }
        }

        public virtual async Task<IEnumerable<TDTO>> GetAllAsync()
        {
            return (await TableUntracked.ToListAsync()).Select(x => _mapper.Map<TDTO>(x));
        }

        public virtual async Task<TDTO> GetByIdAsync(object id)
        {
            return _mapper.Map<TDTO>(await _entities.FindAsync(id));
        }

        public virtual IEnumerable<TDTO> Add(List<TDTO> dto)
        {
            var entity = _mapper.Map<List<TEntity>>(dto);
            _entities.AddRange(entity);
            return _mapper.Map<IEnumerable<TDTO>>(entity);
        }

        public virtual int Add(TDTO dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            _entities.Add(entity);
            return entity is BaseModel ? (entity as BaseModel).Id : 0;
        }
        public virtual void Edit(TDTO dto, int id)
        {
            var entity = _entities.Find(id);
            entity.SetGenericPropertiesValues(dto);
        }
        public async virtual Task<int> SaveChanges()
        {
            int result = 0;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    if (ex.InnerException == null) throw ex;
                    throw ex.InnerException.GetBaseException();
                }
            }

            return result;
        }
    }
}
