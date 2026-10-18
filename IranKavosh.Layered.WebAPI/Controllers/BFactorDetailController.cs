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

    public class BFactorDetailController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public BFactorDetailController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<BFactorDetail>>> Any()
        {
            var BFactorDetails = await _unitOfWork.BFactorDetailRepository.Any();
            return Ok(BFactorDetails);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<BFactorDetail>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var BFactorDetails = await _unitOfWork.BFactorDetailRepository.GetById(id);
            return Ok(BFactorDetails);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<BFactorDetail>>> Get()
        {
            var data = await _unitOfWork.BFactorDetailRepository.GetAll();
            // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }

        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<BFactorDetail>> AddOrUpdate([FromBody] string data)
        //{

        //    var data2 = JsonConvert.DeserializeObject<BFactorDetail>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.BFactorDetailRepository.AddOrUpdate(data2);

        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<BFactorDetail>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<BFactorDetail>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.BFactorDetailRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.BFactorDetailRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getPerson.item.Count = entity.Count;
            getPerson.item.PriceSell = entity.PriceSell;
            getPerson.item.PriceUnit = entity.PriceUnit;
            getPerson.item.ProductId = entity.ProductId;

            await _unitOfWork.BFactorDetailRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPost("AddRange/{id}")]
        public async Task<ActionResult<BFactorDetail>> AddRange([FromBody] string data, Guid id)
        {
            var entity = JsonConvert.DeserializeObject<IEnumerable<BFactorDetail>>(data, ClsJason.IsoDateTimeConverter());
            var getDetails = await _unitOfWork.BFactorDetailRepository.Find(g => g.FactorHeaderId == id);
            await _unitOfWork.BFactorDetailRepository.RemoveRange(getDetails);
            _unitOfWork.BFactorDetailRepository.AddRange(entity);

            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<BFactorDetail>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.BFactorDetailRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);

        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.BFactorDetailRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.BFactorDetailRepository.Update(getPerson);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}