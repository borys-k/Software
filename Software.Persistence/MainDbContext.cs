using Microsoft.EntityFrameworkCore;
using Software.Application;
using Software.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Software.Persistence
{
    public class MainDbContext : DbContext, IMainDbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Document> Documents { get; set; }

        public void AddEntity<T>(T entity) where T : class
        {
            this.Set<T>().Add(entity);
        }

        public T Get<T>(int id) where T : class
        {
            return this.Set<T>().Find(id);
        }

        public IQueryable<T> GetAllQuery<T>() where T : class
        {
            return this.Set<T>().AsQueryable();
        }

        public void RemoveEntity<T>(T entity) where T : class
        {
            this.Set<T>().Remove(entity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(MainDbContext).Assembly);
        }
    }
}
