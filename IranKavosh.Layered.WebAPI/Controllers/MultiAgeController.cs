using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IranKavosh.Layered.WebAPI.Middleware;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultiAgeController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultiAgeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<MultiAge>>> Any()
        {
            var items = await _unitOfWork.MultiAgeRepository.Any();
            return Ok(items);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultiAge>> GetById(Guid id)
        {
            var item = await _unitOfWork.MultiAgeRepository.GetById(id);
            return Ok(item);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MultiAge>>> Get()
        {
            var data = await _unitOfWork.MultiAgeRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultiAge>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultiAge>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiAgeRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultiAge>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiAgeRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _unitOfWork.MultiAgeRepository.GetById(id);
            item.IsDelete = true;
            await _unitOfWork.MultiAgeRepository.Update(item);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
