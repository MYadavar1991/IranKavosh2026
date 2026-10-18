using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Middleware;
using IranKavosh.Layered.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefinitiveAccountController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public DefinitiveAccountController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<DefinitiveAccount>>> Any()
        {
            var accounts = await _unitOfWork.DefinitiveAccountRepository.Any();
            return Ok(accounts);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<DefinitiveAccount>> GetById(Guid id)
        {
            var account = await _unitOfWork.DefinitiveAccountRepository.GetById(id);
            return Ok(account);
        }

        [HttpGet("GetByPersonId/{id}")]
        public async Task<ActionResult<IEnumerable<DefinitiveAccount>>> GetByPersonId(Guid id)
        {
            var account = await _unitOfWork.DefinitiveAccountRepository.Find(f => f.PersonalId == id);
            return Ok(account);
        }
        [HttpGet("GetByPersonCalc/{id}")]
        public async Task<ActionResult<long>> GetByPersonCalc(Guid id)
        {
            var account = await _unitOfWork.DefinitiveAccountRepository.Find(f => f.PersonalId == id);
            var getCalc = account.Sum(s => s.Price);
            return Ok(getCalc);
        }
        [HttpGet("GetByDebtor")]
        public async Task<ActionResult<IEnumerable<ModelDebtor>>> GetByDebtor()
        {
            var account = await _unitOfWork.DefinitiveAccountRepository.GetAll();
            var getDebtor = account.GroupBy(g => g.PersonalId)
                .Select(s => new ModelDebtor { PersonId = s.Key, Price = s.Sum(u => u.Price) })
                .Where(w => w.Price < 0);
            return Ok(getDebtor);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<DefinitiveAccount>>> Get()
        {
            var data = await _unitOfWork.DefinitiveAccountRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<DefinitiveAccount>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<DefinitiveAccount>(data, ClsJason.IsoDateTimeConverter());

            var getDefinitiveAccount = await _unitOfWork.DefinitiveAccountRepository.GetOrNew(g => g.Id == entity.Id);
            var getMaxCode = await _unitOfWork.DefinitiveAccountRepository.GetAll();

            if (getDefinitiveAccount.gNew) // جدید است
            {
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getDefinitiveAccount.item.Code = maxCode;
            }

            getDefinitiveAccount.item.DateCustom = entity.DateCustom;
            getDefinitiveAccount.item.Debtor = entity.Debtor;
            getDefinitiveAccount.item.PersonalId = entity.PersonalId;
            getDefinitiveAccount.item.DocNumber = entity.DocNumber;
            getDefinitiveAccount.item.Price = !entity.Debtor ? entity.Price : entity.Price * (-1);
            getDefinitiveAccount.item.Description = entity.Description;

            await _unitOfWork.DefinitiveAccountRepository.AddOrUpdate(getDefinitiveAccount.item);

            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<DefinitiveAccount>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.DefinitiveAccountRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var account = await _unitOfWork.DefinitiveAccountRepository.GetById(id);
            if (account == null) return NotFound();
            account.IsDelete = true;
            await _unitOfWork.DefinitiveAccountRepository.Update(account);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}