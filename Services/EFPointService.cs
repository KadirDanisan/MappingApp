using MappingApp.Interfaces;
using MappingApp.Models;
using MappingApp.Repository.Point;
using System.Collections.Generic;

namespace MappingApp.Services
{
    public class EFPointService : IPointService
    {
        private readonly IPointRepository _pointRepository;

        public EFPointService(IPointRepository pointRepository)
        {
            _pointRepository = pointRepository;
        }

        public Response<List<PointDto>> GetAll()
        {
            return _pointRepository.GetAll();
        }

        public Response<PointDto> GetById(int id)
        {
            return _pointRepository.GetById(id);
        }

        public Response<PointDto> AddPoint(PointDto point)
        {
            return _pointRepository.Add(point);
        }

        public Response<PointDto> UpdatePoint(int id, PointDto updatePoint)
        {
            updatePoint.Id = id; 
            return _pointRepository.Update(updatePoint);
        }

        public Response<bool> DeletePoint(int id)
        {
            return _pointRepository.Delete(id);
        }
    }
}
