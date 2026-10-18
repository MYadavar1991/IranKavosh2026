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

    public class LoginController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;


        public LoginController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<Login>>> Any()
        {
            var Logins = await _unitOfWork.LoginRepository.Any();
            return Ok(Logins);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Login>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var Logins = await _unitOfWork.LoginRepository.GetById(id);
            return Ok(Logins);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Login>>> Get()
        {
            var data = await _unitOfWork.LoginRepository.GetAll();
            // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }

        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<Login>> AddOrUpdate([FromBody] string data)
        //{

        //    var data2 = JsonConvert.DeserializeObject<Login>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.LoginRepository.AddOrUpdate(data2);

        //    _unitOfWork.Save();
        //    return Ok(data2);
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<Login>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<Login>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.LoginRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.LoginRepository.GetAll();
                //var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                //getPerson.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getPerson.item.WinUser = entity.WinUser;
            getPerson.item.PersonalId = entity.PersonalId;


            await _unitOfWork.LoginRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<Login>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.LoginRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
            return NoContent();
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.LoginRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.LoginRepository.Update(getPerson);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}