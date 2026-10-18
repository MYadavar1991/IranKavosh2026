using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HddMemoryPersonController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public HddMemoryPersonController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<HddMemoryPerson>>> Any()
        {
            var list = await _unitOfWork.HddMemoryPersonRepository.Any();
            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<HddMemoryPerson>> GetById(Guid id)
        {
            var entity = await _unitOfWork.HddMemoryPersonRepository.GetById(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<HddMemoryPerson>>> Get()
        {
            var data = await _unitOfWork.HddMemoryPersonRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<HddMemoryPerson>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<HddMemoryPerson>(data, ClsJason.IsoDateTimeConverter());
            if (entity == null) return BadRequest();
            await _unitOfWork.HddMemoryPersonRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<HddMemoryPerson>> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<HddMemoryPerson>(data, ClsJason.IsoDateTimeConverter());
            if (entity == null) return BadRequest();
            await _unitOfWork.HddMemoryPersonRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _unitOfWork.HddMemoryPersonRepository.GetById(id);
            if (entity == null) return NotFound();
            entity.IsDelete = true;
            await _unitOfWork.HddMemoryPersonRepository.Update(entity);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
