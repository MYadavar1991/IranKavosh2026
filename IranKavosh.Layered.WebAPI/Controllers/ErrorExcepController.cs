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

    public class ErrorExcepController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public ErrorExcepController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<ErrorExcep>>> Any()
        {
            var ErrorExceps = await _unitOfWork.ErrorExcepRepository.Any();
            return Ok(ErrorExceps);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ErrorExcep>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var ErrorExceps = await _unitOfWork.ErrorExcepRepository.GetById(id);
            return Ok(ErrorExceps);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ErrorExcep>>> Get()
        {
            var data = await _unitOfWork.ErrorExcepRepository.GetAll();
           // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }
    
        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<ErrorExcep>> AddOrUpdate([FromBody] string data)
        //{
            
        //    var data2 = JsonConvert.DeserializeObject<ErrorExcep>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.ErrorExcepRepository.AddOrUpdate(data2);
      
        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<ErrorExcep>> AddOrUpdate([FromBody] string data)
        //{
        //    var entity = JsonConvert.DeserializeObject<ErrorExcep>(data, ClsJason.IsoDateTimeConverter());
        //    var getPerson = await _unitOfWork.ErrorExcepRepository.GetOrNew(g => g.Id == entity.Id);

        //    if (getPerson.gNew) // جدید است
        //    {
        //        var getMaxCode = await _unitOfWork.ErrorExcepRepository.GetAll();
        //        var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
        //        getPerson.item.Code = maxCode;
        //    }
        //    else
        //    {
        //       // getPerson.item.Code = entity.Code;
        //    }

        //    getPerson.item.LastName = entity.LastName;
        //    getPerson.item.FirstName = entity.FirstName;
        //    getPerson.item.Mobile = entity.Mobile;
        //    getPerson.item.ErrorExcepGroupId = entity.ErrorExcepGroupId;
        //    getPerson.item.Username = entity.Username;





        //    await _unitOfWork.ErrorExcepRepository.AddOrUpdate(getPerson.item);

        //    _unitOfWork.Save();
        //    return Ok(entity);
        //}
        //[HttpPut("Update")]
        //public async Task<IActionResult> Update([FromBody] string data)
        //{
        //    var data2 = JsonConvert.DeserializeObject<ErrorExcep>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.ErrorExcepRepository.Update(data2);
        //    _unitOfWork.Save();
        //    return Ok(data2);
        //    return NoContent();
        //}
     
        //[HttpPut("Delete/{id}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //   // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
        //    var getPerson = _unitOfWork.ErrorExcepRepository.GetById(id).Result;
        //    getPerson.IsDelete = true;
        //    await _unitOfWork.ErrorExcepRepository.Update(getPerson);

        //    _unitOfWork.Save();
        //    return NoContent();
        //}
    }
}