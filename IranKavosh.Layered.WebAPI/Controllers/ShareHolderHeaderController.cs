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

    public class ShareHolderHeaderController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ShareHolderHeaderController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<ShareHolderHeader>>> Any()
        {
            var ShareHolderHeaders = await _unitOfWork.ShareHolderHeaderRepository.Any();
            return Ok(ShareHolderHeaders);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ShareHolderHeader>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var ShareHolderHeaders = await _unitOfWork.ShareHolderHeaderRepository.GetById(id);
            return Ok(ShareHolderHeaders);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ShareHolderHeader>>> Get()
        {
            var data = await _unitOfWork.ShareHolderHeaderRepository.GetAll();
           // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }
    
        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<ShareHolderHeader>> AddOrUpdate([FromBody] string data)
        //{
            
        //    var data2 = JsonConvert.DeserializeObject<ShareHolderHeader>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.ShareHolderHeaderRepository.AddOrUpdate(data2);
      
        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<ShareHolderHeader>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<ShareHolderHeader>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.ShareHolderHeaderRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.ShareHolderHeaderRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }
            else
            {
               // getPerson.item.Code = entity.Code;
            }

            getPerson.item.Title = entity.Title;

            await _unitOfWork.ShareHolderHeaderRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<ShareHolderHeader>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.ShareHolderHeaderRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
            return NoContent();
        }
     
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
           // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.ShareHolderHeaderRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.ShareHolderHeaderRepository.Update(getPerson);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}