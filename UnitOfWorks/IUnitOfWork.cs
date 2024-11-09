using MappingApp.Interfaces;
using MappingApp.Repository;
using MappingApp.Repository.Point;

namespace MappingApp.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        IPointRepository Points { get; }
        int Complete();
    }
}
