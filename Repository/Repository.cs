using MappingApp.Models;
using MappingApp.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MappingApp.Interfaces
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly MappingAppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(MappingAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual Response<List<T>> GetAll()
        {
            var entities = _dbSet.ToList();
            return new Response<List<T>>(entities, "Success", "Kayıtlar başarıyla getirildi.");
        }

        public virtual Response<T> GetById(int id)
        {
            var entity = _dbSet.Find(id);
            return entity != null
                ? new Response<T>(entity, "Success", "Kayıt başarıyla getirildi.")
                : new Response<T>(null, "Not Found", "Kayıt bulunamadı.");
        }

        public virtual Response<T> Add(T entity)
        {
            if (entity == null)
            {
                return new Response<T>(null, "Bad Request", "Geçersiz kayıt.");
            }

            _dbSet.Add(entity);
            _context.SaveChanges();
            return new Response<T>(entity, "Success", "Kayıt başarıyla eklendi.");
        }

        public virtual Response<T> Update(T entity)
        {
            if (entity == null)
            {
                return new Response<T>(null, "Bad Request", "Geçersiz güncelleme.");
            }

            _dbSet.Update(entity);
            _context.SaveChanges();
            return new Response<T>(entity, "Success", "Kayıt başarıyla güncellendi.");
        }

        public virtual Response<bool> Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                return new Response<bool>(false, "Not Found", "Silinecek kayıt bulunamadı.");
            }

            _dbSet.Remove(entity);
            _context.SaveChanges();
            return new Response<bool>(true, "Success", "Kayıt başarıyla silindi.");
        }
    }
}
