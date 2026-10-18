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
using IranKavosh.Layered.Model.Table_Bot;
using static IranKavosh.Layered.WebAPI.Models.ModelBot;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ChanelMemberController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;



        public ChanelMemberController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("AddReferral")]
        public async Task<ActionResult<bool>> AddReferral([FromBody]  string data)
        {
            var referral = JsonConvert.DeserializeObject<ModelReferral>(data, ClsJason.IsoDateTimeConverter());
            var isAlreadyMember = await _unitOfWork.ChanelMemberRepository.Any(a => a.ReferralReceive == referral.ReferralReceive);
            if (isAlreadyMember)
            {
                //return BadRequest("این شخص قبلاً عضو شده و نمی‌تواند دوباره با رفرال ثبت شود.");
                return Ok(false);
            }
            var newReferral = new ChanelMember
            {
                ReferralSend = referral.ReferralSend,
                ReferralReceive = referral.ReferralReceive,
                DateTime = DateTime.Now,
                MemberDate = DateTime.Now,
            };

            await _unitOfWork.ChanelMemberRepository.AddOrUpdate(newReferral);
            _unitOfWork.Save();

            return Ok(true);

        }



        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<ChanelMember>>> Any()
        {
            var ChanelMembers = await _unitOfWork.ChanelMemberRepository.Any();
            return Ok(ChanelMembers);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ChanelMember>> GetById(Guid id)
        {
            //var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var ChanelMembers = await _unitOfWork.ChanelMemberRepository.GetById(id);
            return Ok(ChanelMembers);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ChanelMember>>> Get()
        {
            var data = await _unitOfWork.ChanelMemberRepository.GetAll();
            // var json = JsonConvert.SerializeObject(data);
            return Ok(data);
        }


        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<ChanelMember>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<ChanelMember>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.ChanelMemberRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.ChanelMemberRepository.GetAll();
                //var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                //getPerson.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getPerson.item.ReferralReceive = entity.ReferralReceive;
            getPerson.item.MemberDate = entity.MemberDate;
            getPerson.item.ReferralSend = entity.ReferralSend;


            await _unitOfWork.ChanelMemberRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var data2 = JsonConvert.DeserializeObject<ChanelMember>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.ChanelMemberRepository.Update(data2);
            _unitOfWork.Save();
            return Ok(data2);

        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // var data = JsonConvert.DeserializeObject<Guid>(id, ClsJason.IsoDateTimeConverter());
            var getPerson = _unitOfWork.ChanelMemberRepository.GetById(id).Result;
            getPerson.IsDelete = true;
            await _unitOfWork.ChanelMemberRepository.Update(getPerson);

            _unitOfWork.Save();
            return NoContent();
        }
    }
}