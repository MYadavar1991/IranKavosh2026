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

    public class PersonalController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        #region Bot Bale
        [HttpGet("BotAnyUserId/{userId}")]
        public async Task<ActionResult<bool>> BotAnyUserId(long userId)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var status = (await _unitOfWork.PersonalRepository.GetAll()).Any(a => a.BotUserId == userId);
           // return false;
            return status;
        }

        [HttpPost("BotUpdateUserId")]
        public async Task<ActionResult<Personal>> BotUpdateUserId([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<Personal>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.PersonalRepository.GetOrNew(g => g.Mobile == entity.Mobile);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.PersonalRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getPerson.item.BotUserId = entity.BotUserId;
            getPerson.item.BotLoginDateTime = entity.BotLoginDateTime;

            getPerson.item.LastName = entity.LastName;
            getPerson.item.FirstName = entity.FirstName;
            getPerson.item.Mobile = entity.Mobile;
            getPerson.item.PersonalGroupId = entity.PersonalGroupId;
            getPerson.item.Username = entity.Username;
            getPerson.item.Password = entity.Password;
            getPerson.item.InventoryFree = entity.InventoryFree;
            getPerson.item.InventoryMain = entity.InventoryMain;
            getPerson.item.Address = entity.Address;
            getPerson.item.CodeMelli = "0000000000";

            await _unitOfWork.PersonalRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }

        #endregion  

        public PersonalController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<Personal>>> Any()
        {
            var personals = await _unitOfWork.PersonalRepository.Any();
            return Ok(personals);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Personal>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var personals = await _unitOfWork.PersonalRepository.GetById(id);
            return Ok(personals);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Personal>>> Get()
        {
            var data = await _unitOfWork.PersonalRepository.GetAll();
            // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }

        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<Personal>> AddOrUpdate([FromBody] string data)
        //{

        //    var data2 = JsonConvert.DeserializeObject<Personal>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.PersonalRepository.AddOrUpdate(data2);

        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<Personal>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<Personal>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.PersonalRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.PersonalRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getPerson.item.LastName = entity.LastName;
            getPerson.item.FirstName = entity.FirstName;
            getPerson.item.Mobile = entity.Mobile;
            getPerson.item.PersonalGroupId = entity.PersonalGroupId;
            getPerson.item.Username = entity.Username;
            getPerson.item.Password = entity.Password;
            getPerson.item.InventoryFree = entity.InventoryFree;
            getPerson.item.InventoryMain = entity.InventoryMain;
            getPerson.item.Address = entity.Address;
            getPerson.item.CodeMelli = "0000000000";

            await _unitOfWork.PersonalRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<Personal>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.PersonalRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
        
        }
        [HttpPost("UpdatePassword/{botUserId}/{pass}")]
        public async Task<IActionResult> UpdatePassword(long botUserId, string pass)
        {
            var getPerson =await _unitOfWork.PersonalRepository.FindFirst(f => f.BotUserId == botUserId);
            getPerson.Password = pass;
            await _unitOfWork.PersonalRepository.Update(getPerson);
            _unitOfWork.Save();
            return Ok();

        }
        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.PersonalRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.PersonalRepository.Update(getPerson);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}