using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Access;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonalGroupController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public PersonalGroupController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<PersonalGroup>>> Any()
        {
            var groups = await _unitOfWork.PersonalGroupRepository.Any();
            return Ok(groups);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<PersonalGroup>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var groups = await _unitOfWork.PersonalGroupRepository.GetById(id);
            return Ok(groups);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<PersonalGroup>>> Get()
        {
            var data = await _unitOfWork.PersonalGroupRepository.GetAll();
            var json = JsonConvert.SerializeObject(data);
            return Ok(json);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<PersonalGroup>> AddOrUpdate([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<PersonalGroup>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.PersonalGroupRepository.AddOrUpdate(data2);
            _unitOfWork.Save();
            return Ok(data2);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<PersonalGroup>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.PersonalGroupRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getGroup = _unitOfWork.PersonalGroupRepository.GetById(id).Result;
            getGroup.IsDelete = true;
            await _unitOfWork.PersonalGroupRepository.Update(getGroup);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
