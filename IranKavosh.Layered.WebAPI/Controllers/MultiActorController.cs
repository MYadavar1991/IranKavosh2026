using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IranKavosh.Layered.WebAPI.Middleware;
using IranKavosh.Layered.WebAPI.Models;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultiActorController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultiActorController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<MultiActor>>> Any()
        {
            var actors = await _unitOfWork.MultiActorRepository.Any();
            return Ok(actors);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultiActor>> GetById(Guid id)
        {
            var actor = await _unitOfWork.MultiActorRepository.GetById(id);
            return Ok(actor);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MultiActor>>> Get()
        {
            var data = await _unitOfWork.MultiActorRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultiActor>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultiActor>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiActorRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] MultiActor entity)
        {
            if (entity == null)
                return BadRequest();

            await _unitOfWork.MultiActorRepository.Update(entity);
             _unitOfWork.Save();
            return Ok(entity);
        }

        //[HttpPut("Update")]
        //public async Task<IActionResult> Update([FromBody] string data)
        //{
        //    var entity = JsonConvert.DeserializeObject<MultiActor>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.MultiActorRepository.Update(entity);
        //    _unitOfWork.Save();
        //    return Ok(entity);
        //}

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var actor = await _unitOfWork.MultiActorRepository.GetById(id);
            actor.IsDelete = true;
            await _unitOfWork.MultiActorRepository.Update(actor);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpPost("AddIfNew")]
        public async Task<ActionResult<MultiActor>> AddIfNew([FromBody] string data)
        {
            List<string> entity = JsonConvert.DeserializeObject<List<string>>(data, ClsJason.IsoDateTimeConverter());

            List<string> getActor = _unitOfWork.MultiActorRepository.GetAll().Result.Select(s => s.ActorName).ToList();

            // حذف رشته‌های مشترک
            List<string> uniqueEntities = entity
                .Where(e => !getActor.Contains(e))
                .ToList();

            foreach (var item in uniqueEntities)
            {
                MultiActor multiActor = new MultiActor
                {
                    ActorName = "",
                    ActorNameEn = item
                };
                if (!_unitOfWork.MultiActorRepository.Any(a => a.ActorNameEn == item).Result)
                {
                    await _unitOfWork.MultiActorRepository.AddOrUpdate(multiActor);
                }

            }

            //  await _unitOfWork.MultiActorRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPost("AddIfNewFull")]
        public async Task<ActionResult<MultiActor>> AddIfNewFull([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<List<ModelActor>>(data, ClsJason.IsoDateTimeConverter());

            var getActor = (await _unitOfWork.MultiActorRepository.GetAll()).Select(s => s.ActorName).ToList();

            // حذف رشته‌های مشترک
            var uniqueEntities = entity.Where(e => !getActor.Contains(e.En)).ToList();
            //var uniqueEntities = entity.Where(e => !getActor.Contains(e)).ToList();

            foreach (var item in uniqueEntities)
            {
                MultiActor multiActor = new MultiActor
                {
                    ActorName = item.Fa,
                    ActorNameEn = item.En
                };
                if (!_unitOfWork.MultiActorRepository.Any(a => a.ActorNameEn == item.En).Result)
                {
                    await _unitOfWork.MultiActorRepository.AddOrUpdate(multiActor);
                }

            }

            //  await _unitOfWork.MultiActorRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }
    }
}
