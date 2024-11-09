using MappingApp.Interfaces;
using MappingApp.Models;
using MappingApp.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MappingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PointController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<Response<List<PointDto>>> GetAll()
        {
            var response = _unitOfWork.Points.GetAll();
            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddPoint([FromBody] PointDto point)
        {
            var response = _unitOfWork.Points.Add(point);
            _unitOfWork.Complete(); 
            return CreatedAtAction(nameof(GetById), new { id = response.Values.Id }, response);
        }

        [HttpGet("{id}")]
        public ActionResult<Response<PointDto>> GetById(int id)
        {
            var response = _unitOfWork.Points.GetById(id);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public ActionResult<Response<PointDto>> UpdatePoint(int id, PointDto updatePoint)
        {
            updatePoint.Id = id; 
            var response = _unitOfWork.Points.Update(updatePoint);
            _unitOfWork.Complete(); 
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public ActionResult<Response<bool>> DeletePoint(int id)
        {
            var response = _unitOfWork.Points.Delete(id);
            _unitOfWork.Complete();
            return Ok(response);
        }
    }
}
