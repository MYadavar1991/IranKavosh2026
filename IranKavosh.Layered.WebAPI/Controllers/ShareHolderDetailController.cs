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

    public class ShareHolderDetailController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ShareHolderDetailController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<ShareHolderDetail>>> Any()
        {
            var ShareHolderDetails = await _unitOfWork.ShareHolderDetailRepository.Any();
            return Ok(ShareHolderDetails);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ShareHolderDetail>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var ShareHolderDetails = await _unitOfWork.ShareHolderDetailRepository.GetById(id);
            return Ok(ShareHolderDetails);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ShareHolderDetail>>> Get()
        {
            var data = await _unitOfWork.ShareHolderDetailRepository.GetAll();
           // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }
    
        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<ShareHolderDetail>> AddOrUpdate([FromBody] string data)
        //{
            
        //    var data2 = JsonConvert.DeserializeObject<ShareHolderDetail>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.ShareHolderDetailRepository.AddOrUpdate(data2);
      
        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<ShareHolderDetail>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<ShareHolderDetail>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.ShareHolderDetailRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.ShareHolderDetailRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }
            else
            {
               // getPerson.item.Code = entity.Code;
            }

            getPerson.item.PersonalId = entity.PersonalId;
            getPerson.item.ShareHolderHeaderId = entity.ShareHolderHeaderId;
            getPerson.item.Stocks = entity.Stocks;

            await _unitOfWork.ShareHolderDetailRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<ShareHolderDetail>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.ShareHolderDetailRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
            return NoContent();
        }
     
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
           // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.ShareHolderDetailRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.ShareHolderDetailRepository.Update(getPerson);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}