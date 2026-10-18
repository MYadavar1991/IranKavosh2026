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

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BuySellDetailController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public BuySellDetailController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<BuySellDetail>>> Any()
        {
            var BuySellDetails = await _unitOfWork.BuySellDetailRepository.Any();
            return Ok(BuySellDetails);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BuySellDetail>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var BuySellDetails = await _unitOfWork.BuySellDetailRepository.GetById(id);
            return Ok(BuySellDetails);
        }
        [HttpGet("GetByGroupId/{id}")]
        public async Task<ActionResult<IEnumerable<BuySellDetail>>> GetByGroupId(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var BuySellDetails = await _unitOfWork.BuySellDetailRepository.Find(f => f.GroupId == id);
            return Ok(BuySellDetails);
        }
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<BuySellDetail>>> Get()
        {
            var data = await _unitOfWork.BuySellDetailRepository.GetAll();
            // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }

        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<BuySellDetail>> AddOrUpdate([FromBody] string data)
        //{

        //    var data2 = JsonConvert.DeserializeObject<BuySellDetail>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.BuySellDetailRepository.AddOrUpdate(data2);

        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<BuySellDetail>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<BuySellDetail>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.BuySellDetailRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.BuySellDetailRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getPerson.item.PriceListId = entity.PriceListId;
            getPerson.item.Count = entity.Count;
            getPerson.item.GroupId = entity.GroupId;
            getPerson.item.PriceBuy = entity.PriceBuy;
            getPerson.item.PriceFinal = entity.PriceFinal;
            getPerson.item.PriceSell = entity.PriceSell;
            getPerson.item.Sell = entity.Sell;

            await _unitOfWork.BuySellDetailRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<BuySellDetail>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.BuySellDetailRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
            return NoContent();
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.BuySellDetailRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.BuySellDetailRepository.Update(getPerson);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}