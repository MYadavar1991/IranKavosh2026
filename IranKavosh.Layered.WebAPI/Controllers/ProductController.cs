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

    public class ProductController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ProductController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<Product>>> Any()
        {
            var Products = await _unitOfWork.ProductRepository.Any();
            return Ok(Products);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var Products = await _unitOfWork.ProductRepository.GetById(id);
            return Ok(Products);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var data = await _unitOfWork.ProductRepository.GetAll();
           // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }
    
        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<Product>> AddOrUpdate([FromBody] string data)
        //{
            
        //    var data2 = JsonConvert.DeserializeObject<Product>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.ProductRepository.AddOrUpdate(data2);
      
        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<Product>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<Product>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.ProductRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.ProductRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }
            else
            {
               // getPerson.item.Code = entity.Code;
            }

            getPerson.item.NameApp = entity.NameApp;
            getPerson.item.NamePrint = entity.NamePrint;
            getPerson.item.BarCode = entity.BarCode;
            getPerson.item.Company = entity.Company;
            getPerson.item.Inventory = entity.Inventory;
            getPerson.item.Model = entity.Model;
            //getPerson.item.SaleTypeId = entity.SaleTypeId;
            getPerson.item.UnitId = entity.UnitId;
            getPerson.item.Description = entity.Description;
            getPerson.item.FundTypeId = entity.FundTypeId;

            await _unitOfWork.ProductRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<Product>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.ProductRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
          
        }
     
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
           // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.ProductRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.ProductRepository.Update(getPerson);

            _unitOfWork.Save();
            return Ok();
        }
    }
}