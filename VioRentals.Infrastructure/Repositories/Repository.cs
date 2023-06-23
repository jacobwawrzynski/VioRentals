using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VioRentals.Core.Entities;
using VioRentals.Infrastructure.Data;
using VioRentals.Infrastructure.Repositories.Interfaces;

namespace VioRentals.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;
        private DbSet<T> _entities;
        public Repository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public T Get(int id)
        {
            return _entities.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.AsEnumerable();
        }

        public void Create(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            _entities.Update(entity);
            _context.SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
