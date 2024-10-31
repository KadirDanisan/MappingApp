using MappingApp.Interfaces;
using MappingApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MappingApp.Services
{
    public class PointService : IPointService
    {
        private readonly MappingAppDbContext _dbContext;

        public PointService(MappingAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Response<T> CreateResponse<T>(T values, string status, string message)
        {
            return new Response<T>(values, status, message);
        }

        public Response<List<PointDto>> GetAll()
        {
            var points = _dbContext.Points.ToList(); // Veritabanından tüm noktaları al
            return CreateResponse(points, "200", "Liste getirildi!");
        }

        public Response<PointDto> AddPoint(PointDto point)
        {
            _dbContext.Points.Add(point); // Yeni noktayı ekle
            _dbContext.SaveChanges(); // Değişiklikleri kaydet
            return CreateResponse(point, "201", "Listeye ekleme işlemi gerçekleşti!");
        }

        public Response<PointDto> GetById(int id)
        {
            var point = _dbContext.Points.FirstOrDefault(p => p.Id == id);
            if (point != null)
            {
                return CreateResponse(point, "200", "İstenilen ID bulundu!");
            }

            return CreateResponse<PointDto>(null, "404", "İstenilen ID bulunamadı!"); // Type argument belirttik
        }

        public Response<PointDto> UpdatePoint(int id, PointDto updatePoint)
        {
            var point = _dbContext.Points.FirstOrDefault(p => p.Id == id);
            if (point != null)
            {
                point.PointX = updatePoint.PointX;
                point.PointY = updatePoint.PointY;
                point.Name = updatePoint.Name;
                point.Description = updatePoint.Description;

                _dbContext.SaveChanges(); // Değişiklikleri kaydet
                return CreateResponse(point, "200", "İstenilen güncelleme gerçekleşti!");
            }

            return CreateResponse<PointDto>(null, "404", "İstenilen güncelleme gerçekleşmedi!"); // Type argument belirttik
        }

        public Response<bool> DeletePoint(int id)
        {
            var point = _dbContext.Points.FirstOrDefault(p => p.Id == id);
            if (point != null)
            {
                _dbContext.Points.Remove(point);
                _dbContext.SaveChanges(); // Değişiklikleri kaydet
                return CreateResponse(true, "200", "Silme işlemi gerçekleşti!");
            }

            return CreateResponse<bool>(false, "404", "Silme işlemi gerçekleşmedi!"); // Type argument belirttik
        }
    }
}
