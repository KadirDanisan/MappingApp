using MappingApp.Models;
using MappingApp.Repository.Point;
using MappingApp.Services;

namespace MappingApp.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MappingAppDbContext _context;
        public UnitOfWork(MappingAppDbContext context)
        {
            _context = context;
            Points = new PointRepository(_context);
        }

        public IPointRepository Points { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
