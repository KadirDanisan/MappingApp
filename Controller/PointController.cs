using MappingApp.Interfaces;
using MappingApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MappingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IPointService _pointService;


        public PointController(IPointService pointService)
        {
            _pointService = pointService;
        }

        [HttpGet]
        public ActionResult<Response<List<Point>>> GetAll()
        {
            var response = _pointService.GetAll();
            return Ok(response); 
        }
        [HttpPost]
        public ActionResult<Response<Point>> AddPoint([FromBody] Point point)
        {
            var response = _pointService.AddPoint(point);

             return CreatedAtAction(nameof(GetById), new { id = point.Id }, response);
          
        }

        [HttpGet("{id}")]
        public ActionResult<Response<Point>> GetById(int id)
        {
            var response = _pointService.GetById(id);
     
            return Ok(response); 

        }

        [HttpPut("{id}")]
        public ActionResult<Response<Point>> UpdatePoint(int id, Point updatePoint)
        {
            var response = _pointService.UpdatePoint(id, updatePoint);
             return Ok(response); 

        }

        [HttpDelete("{id}")]
        public ActionResult<Response<bool>> DeletePoint(int id)
        {
            var response = _pointService.DeletePoint(id);
            return Ok(response);       
        }
    }
}
// CONTROLLER İÇERİSİNDE SADELEŞME İNTERFACE OLUŞTURUP SERVİCE DOSYASI OLUŞTUR 
// response adı alan 3 parametre alan value status error.message 

//htpp["(action)"] ? 
// IActionResult & ActionResult çalışma mantık farkı ?  kendin araşatır 
//private readonly List<blabla> ? 
