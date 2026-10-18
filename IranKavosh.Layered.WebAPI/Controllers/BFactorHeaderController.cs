using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Access;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using IranKavosh.Layered.Model.Tables.BOT;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BFactorHeaderController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

         

        public BFactorHeaderController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<BFactorHeader>>> Any()
        {
            var BFactorHeaders = await _unitOfWork.BFactorHeaderRepository.Any();
            return Ok(BFactorHeaders);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BFactorHeader>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var BFactorHeaders = await _unitOfWork.BFactorHeaderRepository.GetById(id);
            return Ok(BFactorHeaders);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<BFactorHeader>>> Get()
        {
            var data = await _unitOfWork.BFactorHeaderRepository.GetAll();
            // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }

        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<BFactorHeader>> AddOrUpdate([FromBody] string data)
        //{

        //    var data2 = JsonConvert.DeserializeObject<BFactorHeader>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.BFactorHeaderRepository.AddOrUpdate(data2);

        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<BFactorHeader>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<BFactorHeader>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.BFactorHeaderRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.BFactorHeaderRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getPerson.item.Id = entity.Id;
            getPerson.item.BuyerId = entity.BuyerId;
            getPerson.item.Date = entity.Date;
            getPerson.item.Header = entity.Header;

            await _unitOfWork.BFactorHeaderRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<BFactorHeader>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.BFactorHeaderRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
            return NoContent();
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.BFactorHeaderRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.BFactorHeaderRepository.Update(getPerson);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}