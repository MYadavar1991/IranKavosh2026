using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IranKavosh.Layered.WebAPI.Middleware;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultiCountryController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultiCountryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<MultiCountry>>> Any()
        {
            var countries = await _unitOfWork.MultiCountryRepository.Any();
            return Ok(countries);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultiCountry>> GetById(Guid id)
        {
            var country = await _unitOfWork.MultiCountryRepository.GetById(id);
            return Ok(country);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MultiCountry>>> Get()
        {
            var data = await _unitOfWork.MultiCountryRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultiCountry>> AddOrUpdate([FromBody] string data)
        {
            var country = JsonConvert.DeserializeObject<MultiCountry>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiCountryRepository.AddOrUpdate(country);
            _unitOfWork.Save();
            return Ok(country);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var country = JsonConvert.DeserializeObject<MultiCountry>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiCountryRepository.Update(country);
            _unitOfWork.Save();
            return Ok(country);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var country = await _unitOfWork.MultiCountryRepository.GetById(id);
            country.IsDelete = true;
            await _unitOfWork.MultiCountryRepository.Update(country);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
