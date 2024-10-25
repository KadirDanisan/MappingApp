using MappingApp.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MappingApp.Services
{
    public class PointService : IPointService
    {
        private static readonly List<Point> _pointList = new List<Point>();
        private Response<T> CreateResponse<T>(T values, string status, string message)
        {
            return new Response<T>(values, status, message);
        }

        public Response<List<Point>> GetAll()
        {
            return CreateResponse(_pointList, "200", "Liste getirildi!");
        }
        public Response<Point> AddPoint(Point point)
        {
            _pointList.Add(point);
            return CreateResponse(point, "201", "Listeye ekleme işlemi gerçekleşdi !");
           
        }

        public Response<Point> GetById(int id)
        {
            var point = _pointList.FirstOrDefault(p => p.Id == id);
            if (point != null)
            {
                return CreateResponse(point, "200", "İstenilen ID bulundu !");
          
            }
            //NULL KULLANAMADIK DEFAULT ATIK BURAYA
            return CreateResponse(default(Point), "400", "İstenilen ID bulunamadı!");       
        }

        public Response<Point> UpdatePoint(int id, Point updatePoint)
        {
            var point = _pointList.FirstOrDefault(p => p.Id == id);
            if (point != null)
            {
                point.PointX = updatePoint.PointX;
                point.PointY = updatePoint.PointY;
                point.Name = updatePoint.Name;
                point.Description = updatePoint.Description;

                return CreateResponse(point, "200", "İstenilen güncelleme gerçekleşti !");
            }

            return CreateResponse(default(Point), "400", "İstenilen güncelleme gerçekleşmedi !");
        }

        public Response<bool> DeletePoint(int id)
        {
            var point = _pointList.FirstOrDefault(p => p.Id == id);
            if (point != null)
            {
                _pointList.Remove(point);
                return CreateResponse(true, "200", "Silme işlemi gerçekleşti !");
 
            }

            return CreateResponse(false, "400", "Silme işlemi gerçekleşmedi !");
        }
    }
}
// response class ayırılacak ve enpointslerde ayrılacak
// service açılcak database değişiklikleri için servis
//db baglıycaz sql elle yazılcak 