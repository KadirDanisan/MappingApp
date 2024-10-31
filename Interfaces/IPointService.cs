using MappingApp.Models;

namespace MappingApp.Interfaces
{
    public interface IPointService
    {
        Response<List<PointDto>> GetAll();
        Response<PointDto> GetById(int id);
        Response<PointDto> AddPoint(PointDto point);
        Response<PointDto> UpdatePoint(int id, PointDto updatePoint);
        Response<bool> DeletePoint(int id);
    }
}