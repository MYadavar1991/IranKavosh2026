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
    public class FundController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public FundController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<Fund>>> Any()
        {
            var funds = await _unitOfWork.FundRepository.Any();
            return Ok(funds);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Fund>> GetById(Guid id)
        {
            var fund = await _unitOfWork.FundRepository.GetById(id);
            return Ok(fund);
        }

        [HttpGet("GetByGlobalId/{id}")]
        public async Task<ActionResult<Fund>> GetByGlobalId(Guid id)
        {
            var fund = await _unitOfWork.FundRepository.FindFirst(f => f.GlobalId == id);
            return Ok(fund);
        }
        [HttpPost("GetByDate")]
        public async Task<ActionResult<IEnumerable<Fund>>> GetByDate([FromBody] string data)
        {
            var dateTime = JsonConvert.DeserializeObject<DateTime>(data, ClsJason.IsoDateTimeConverter());
            var fund = (await _unitOfWork.FundRepository.GetAll()).AsEnumerable().Where(f => f.DateTime.ToString("yyyy/MM/dd") == dateTime.ToString("yyyy/MM/dd")).AsEnumerable();
            return Ok(fund.OrderByDescending(o => o.Code));
        }
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<Fund>>> Get()
        {
            var data = await _unitOfWork.FundRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<Fund>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<Fund>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.FundRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.FundRepository.GetAll();
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Code = maxCode;
            }

            getPerson.item.Count = entity.Count;
            getPerson.item.Calculation = entity.Calculation;
            getPerson.item.TableName = entity.TableName;
            getPerson.item.GlobalId = entity.GlobalId;
            getPerson.item.PriceReceived = entity.PriceReceived;
            getPerson.item.SellType = entity.SellType;
            getPerson.item.PriceTotal = entity.PriceTotal;
            getPerson.item.DateTime = entity.DateTime;
            getPerson.item.RegisterId = entity.RegisterId;

            await _unitOfWork.FundRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPost("AddByGroupId")]
        public async Task<ActionResult<Fund>> AddByGroupId([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<Fund>(data, ClsJason.IsoDateTimeConverter());

            var getGroup = await _unitOfWork.FundRepository.GetOrNew(g => g.GlobalId == entity.GlobalId);
            var getMaxCode = await _unitOfWork.FundRepository.GetAll();

            #region New Get Price Total

            var getCount = _unitOfWork.CopiedFilmToMemoryRepository.GetAll().Result.Where(w => w.GroupId == entity.GlobalId);
            var getPriceNew = getCount.Sum(s => s.Price);

            #endregion

            if (getGroup.gNew) // جدید است
            {
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getGroup.item.Code = maxCode;
            }
            else
            {
                //getGroup.item.Code = entity.Code;
            }

            getGroup.item.RegisterId = entity.RegisterId;
            getGroup.item.GlobalId = entity.GlobalId;
            //getGroup.item.FundTypeId = Guid.Parse("5231521f-b2c0-4d4e-8b8e-d10fdf439158");
            getGroup.item.SellType = entity.SellType;
            getGroup.item.TableName = entity.TableName;
            // getGroup.item.Calculation = false;
            getGroup.item.PriceReceived += entity.PriceReceived;
            if (entity.SellType == null)
            {
                //var maxCount = getMaxCode.Where(s => s.GlobalId == entity.GlobalId).Select(s => s.Count).DefaultIfEmpty(0).Max() + 1;
                //getGroup.item.Count = maxCount;
                getGroup.item.Count = getCount.Count();
                //getGroup.item.PriceTotal = entity.PriceTotal * maxCount;
                getGroup.item.PriceTotal = getPriceNew;
            }
            else
                getGroup.item.PriceTotal = entity.PriceTotal;

            await _unitOfWork.FundRepository.AddOrUpdate(getGroup.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("UpdateCalc")]
        public async Task<IActionResult> UpdateCalc([FromBody] string date)
        {
            var entity = JsonConvert.DeserializeObject<DateTime>(date, ClsJason.IsoDateTimeConverter());

            var getCalc = (await _unitOfWork.FundRepository.GetAll()).Where(
                w => w.DateTime.Date <= entity.Date && !w.Calculation
                ).ToList();
            getCalc.ForEach(f => f.Calculation = true);

            await _unitOfWork.FundRepository.UpdateRange(getCalc);
            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<Fund>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.FundRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fund = await _unitOfWork.FundRepository.GetById(id);
            if (fund == null) return NotFound();
            fund.IsDelete = true;
            await _unitOfWork.FundRepository.Update(fund);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
