using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using IranKavosh.Layered.WebAPI.Models;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PriceListController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PriceListController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<PriceList>>> Any()
        {
            var PriceLists = await _unitOfWork.PriceListRepository.Any();
            return Ok(PriceLists);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<PriceList>> GetById(Guid id)
        {
            var PriceList = await _unitOfWork.PriceListRepository.GetById(id);
            return Ok(PriceList);
        }

        [HttpGet("GetFullPriceList")]
        public async Task<ActionResult<IEnumerable<ModelFullPriceList>>> GetFullPriceList([FromBody] string? data = null)
        {
            List<Guid>? lstPaper = null;
            if (data != null)
            {
                lstPaper = JsonConvert.DeserializeObject<List<Guid>>(data, ClsJason.IsoDateTimeConverter());
            }

            var getProduct = (await _unitOfWork.ProductRepository.GetAll()).Select(s => new { s.Id, s.NameApp, s.FundTypeId });
            var getPriceList = (await _unitOfWork.PriceListRepository.Find(w => (lstPaper == null || lstPaper.Any(a => a == w.ProductId))))
                .Select(s => new { s.Id, s.ProductId, s.Title, s.Price });
            var getFoundType = (await _unitOfWork.FundTypeRepository.GetAll()).Select(s => new { s.Id, s.Title });
            var getPriceListDiscount = (await _unitOfWork.PriceListDiscountRepository.GetAll()).Select(s => new { s.Id, s.PriceListId, s.Count, s.Discount });

            var getOutPut = getPriceList.Select(s =>
            {
                var getProductFirst = getProduct.First(f => f.Id == s.ProductId);
                var getFoundTypeFirst = getFoundType.First(f => f.Id == getProductFirst.FundTypeId);
                var getPriceListDiscountFull = getPriceListDiscount.Where(w => w.PriceListId == s.Id);
                return new ModelFullPriceList
                {
                    ProductId = s.ProductId,
                    PriceListId = s.Id,
                    Title = s.Title,
                    Price = s.Price,
                    NameApp = getProductFirst.NameApp,
                    FullTitle = getProductFirst.NameApp + " - " + s.Title,
                    FuntType = getFoundTypeFirst.Title,
                    Count_Discount = getPriceListDiscountFull.Select(s2 => new ModelFullPriceListDiscount { Count = s2.Count, Discount = s2.Discount }).ToList(),

                };
            }).AsEnumerable();

            return Ok(getOutPut);
        }
        
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<PriceList>>> Get()
        {
            var data = await _unitOfWork.PriceListRepository.GetAll();
            return Ok(data);
        }
        
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<PriceList>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<PriceList>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.PriceListRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.PriceListRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }

            getPerson.item.Price = entity.Price;
            getPerson.item.ProductId = entity.ProductId;
            getPerson.item.Title = entity.Title;
            getPerson.item.DateTime = entity.DateTime;

            await _unitOfWork.PriceListRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<PriceList>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.PriceListRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }
        
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var PriceList = await _unitOfWork.PriceListRepository.GetById(id);
            if (PriceList == null) return NotFound();
            PriceList.IsDelete = true;
            await _unitOfWork.PriceListRepository.Update(PriceList);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
