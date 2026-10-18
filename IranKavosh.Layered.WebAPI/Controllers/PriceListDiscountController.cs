using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceListDiscountController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PriceListDiscountController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<PriceListDiscount>>> Any()
        {
            var PriceListDiscounts = await _unitOfWork.PriceListDiscountRepository.Any();
            return Ok(PriceListDiscounts);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<PriceListDiscount>> GetById(Guid id)
        {
            var PriceListDiscount = await _unitOfWork.PriceListDiscountRepository.GetById(id);
            return Ok(PriceListDiscount);
        }
        [HttpGet("GetDiscount/{id}")]
        public async Task<ActionResult<PriceListDiscount>> GetDiscount(Guid id)
        {
            if (await _unitOfWork.PriceListDiscountRepository.Any(a => a.PriceListId == id))
            {
                var PriceList = await _unitOfWork.PriceListRepository.GetById(id);
                var PriceListDiscount = (await _unitOfWork.PriceListDiscountRepository.Find(f => f.PriceListId == id));
                return Ok(PriceListDiscount);
            }

            return Ok(null);
        }
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<PriceListDiscount>>> Get()
        {
            var data = await _unitOfWork.PriceListDiscountRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<PriceListDiscount>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<PriceListDiscount>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.PriceListDiscountRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.PriceListDiscountRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }

            getPerson.item.PriceListId = entity.PriceListId;
            getPerson.item.Discount = entity.Discount;
            getPerson.item.Count = entity.Count;
            getPerson.item.Description = entity.Description;
            getPerson.item.DateTime = entity.DateTime;

            await _unitOfWork.PriceListDiscountRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<PriceListDiscount>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.PriceListDiscountRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var PriceListDiscount = await _unitOfWork.PriceListDiscountRepository.GetById(id);
            if (PriceListDiscount == null) return NotFound();
            PriceListDiscount.IsDelete = true;
            await _unitOfWork.PriceListDiscountRepository.Update(PriceListDiscount);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
