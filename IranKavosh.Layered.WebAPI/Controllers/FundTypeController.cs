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
    public class FundTypeController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public FundTypeController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<FundType>>> Any()
        {
            var FundTypes = await _unitOfWork.FundTypeRepository.Any();
            return Ok(FundTypes);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<FundType>> GetById(Guid id)
        {
            var FundType = await _unitOfWork.FundTypeRepository.GetById(id);
            return Ok(FundType);
        }
        //Description Get By Title    خطا دارد
        //[HttpGet("GetByTitle/{data}")]
        //public async Task<ActionResult<FundType>> GetByTitle(string data)
        //{
        //    var getDes = ClassEncryptDecrypt.Decrypt(data);
        //    //var title = JsonConvert.DeserializeObject<string>(getDes, ClsJason.IsoDateTimeConverter());
        //    var fund = await _unitOfWork.FundTypeRepository.FindFirst(f => f.Title == getDes);
        //    return Ok(fund);
        //}

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<FundType>>> Get()
        {
            var data = await _unitOfWork.FundTypeRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<FundType>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<FundType>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.FundTypeRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<FundType>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.FundTypeRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var FundType = await _unitOfWork.FundTypeRepository.GetById(id);
            if (FundType == null) return NotFound();
            FundType.IsDelete = true;
            await _unitOfWork.FundTypeRepository.Update(FundType);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
