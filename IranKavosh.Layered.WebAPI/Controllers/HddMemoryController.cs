using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HddMemoryController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public HddMemoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<HddMemory>>> Any()
        {
            var list = await _unitOfWork.HddMemoryRepository.Any();
            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<HddMemory>> GetById(Guid id)
        {
            var entity = await _unitOfWork.HddMemoryRepository.GetById(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }



        [HttpPost("AddBySerialStable")]
        public async Task<ActionResult> AddBySerialStable([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<HddMemory>(data, ClsJason.IsoDateTimeConverter());
            if (entity == null) 
                return BadRequest();

            if (!await _unitOfWork.HddMemoryRepository.Any(a => a.StableSerial == entity.StableSerial))
            {
                // var exist = await _unitOfWork.HddMemoryRepository.FindFirst(a => a.StableSerial == entity.StableSerial);

                await _unitOfWork.HddMemoryRepository.AddOrUpdate(entity);
                _unitOfWork.Save();
            }

            return Ok();
        }
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<HddMemory>>> Get()
        {
            var data = await _unitOfWork.HddMemoryRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<HddMemory>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<HddMemory>(data, ClsJason.IsoDateTimeConverter());
            if (entity == null) return BadRequest();
            await _unitOfWork.HddMemoryRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<HddMemory>> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<HddMemory>(data, ClsJason.IsoDateTimeConverter());
            if (entity == null) return BadRequest();
            await _unitOfWork.HddMemoryRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _unitOfWork.HddMemoryRepository.GetById(id);
            if (entity == null) return NotFound();
            entity.IsDelete = true;
            await _unitOfWork.HddMemoryRepository.Update(entity);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
