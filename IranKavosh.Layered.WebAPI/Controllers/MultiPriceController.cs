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
    public class MultiPriceController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultiPriceController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<MultiPrice>>> Any()
        {
            var multiPrices = await _unitOfWork.MultiPriceRepository.Any();
            return Ok(multiPrices);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultiPrice>> GetById(Guid id)
        {
            var multiPrice = await _unitOfWork.MultiPriceRepository.GetById(id);
            return Ok(multiPrice);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MultiPrice>>> Get()
        {
            var data = await _unitOfWork.MultiPriceRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultiPrice>> AddOrUpdate([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<MultiPrice>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiPriceRepository.AddOrUpdate(data2);
            _unitOfWork.Save();
            return Ok(data2);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<MultiPrice>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiPriceRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var getMultiPrice = await _unitOfWork.MultiPriceRepository.GetById(id);
            getMultiPrice.IsDelete = true;
            await _unitOfWork.MultiPriceRepository.Update(getMultiPrice);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}