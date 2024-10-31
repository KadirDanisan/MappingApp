using MappingApp.Interfaces;
using MappingApp.Models;
using MappingApp.Repository.Point;
using System.Collections.Generic;
using System.Linq;

namespace MappingApp.Services
{
    public class PointRepository : Repository<PointDto>, IPointRepository
    {
        public PointRepository(MappingAppDbContext context) : base(context)
        {
        }

        public Response<List<PointDto>> GetPointsByCriteria(string criteria)
        {
            var points = _dbSet.Where(p => p.Name.Contains(criteria)).ToList(); // _dbSet kullanıyoruz
            return new Response<List<PointDto>>(points, "Success", "Kriterlere göre kayıtlar getirildi.");
        }
    }
}
