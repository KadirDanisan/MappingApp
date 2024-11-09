using MappingApp.Interfaces;
using MappingApp.Models;
using MappingApp.UnitOfWorks;
using System.Collections.Generic;

namespace MappingApp.Services
{
    public class EFPointService : IPointService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EFPointService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Response<List<PointDto>> GetAll()
        {
            return _unitOfWork.Points.GetAll();
        }

        public Response<PointDto> GetById(int id)
        {
            return _unitOfWork.Points.GetById(id);
        }

        public Response<PointDto> AddPoint(PointDto point)
        {
            var response = _unitOfWork.Points.Add(point);
            _unitOfWork.Complete(); 
            return response;
        }

        public Response<PointDto> UpdatePoint(int id, PointDto updatePoint)
        {
            updatePoint.Id = id;
            var response = _unitOfWork.Points.Update(updatePoint);
            _unitOfWork.Complete(); 
            return response;
        }

        public Response<bool> DeletePoint(int id)
        {
            var response = _unitOfWork.Points.Delete(id);
            _unitOfWork.Complete(); 
            return response;
        }
    }
}
