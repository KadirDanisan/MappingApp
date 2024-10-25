using MappingApp.Services;

namespace MappingApp.Interfaces
{
    public interface IPointService
    {
        Response<List<Point>> GetAll();
        Response<Point> GetById(int id);
        Response<Point> AddPoint(Point point);
        Response<Point> UpdatePoint(int id, Point updatePoint);
        Response<bool> DeletePoint(int id);
    }
}
