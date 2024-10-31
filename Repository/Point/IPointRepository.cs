using MappingApp.Models;
using System.Collections.Generic;

namespace MappingApp.Repository.Point
{
    public interface IPointRepository : IRepository<PointDto>
    {
        Response<List<PointDto>> GetPointsByCriteria(string criteria);
    }
}
