using MappingApp.Interfaces;
using MappingApp.Models;
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
        public ActionResult<Response<List<PointDto>>> GetAll()
        {
            var response = _pointService.GetAll();
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddPoint([FromBody] PointDto point)
        {
            var response = _pointService.AddPoint(point);
            return CreatedAtAction(nameof(GetById), new { id = response.Values.Id }, response);
        }

        [HttpGet("{id}")]
        public ActionResult<Response<PointDto>> GetById(int id)
        {
            var response = _pointService.GetById(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public ActionResult<Response<PointDto>> UpdatePoint(int id, PointDto updatePoint)
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
