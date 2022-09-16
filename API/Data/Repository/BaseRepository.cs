using Domain.Entities;
using Domain.Interfaces;
using Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext applicationDb;

        public BaseRepository(ApplicationDbContext context)
        {
            applicationDb = context;
        }

        public void Insert(TEntity obj)
        {
            applicationDb.Set<TEntity>().Add(obj);
            applicationDb.SaveChanges();
        }

        public void Update(TEntity obj)
        {
            applicationDb.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            applicationDb.SaveChanges();
        }

        public void Delete(int id)
        {
            applicationDb.Set<TEntity>().Remove(Select(id));
            applicationDb.SaveChanges();
        }

        public IList<TEntity> Select() =>
            applicationDb.Set<TEntity>().ToList();

        public TEntity Select(int id) =>
            applicationDb.Set<TEntity>().Find(id)!;

    }
}