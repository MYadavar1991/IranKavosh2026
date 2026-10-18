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
    public class MultimediaHeaderChapterController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultimediaHeaderChapterController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<MultimediaHeaderChapter>>> Any()
        {
            var chapters = await _unitOfWork.MultimediaHeaderChapterRepository.Any();
            return Ok(chapters);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultimediaHeaderChapter>> GetById(Guid id)
        {
            var chapter = await _unitOfWork.MultimediaHeaderChapterRepository.GetById(id);
            return Ok(chapter);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MultimediaHeaderChapter>>> Get()
        {
            var chapters = await _unitOfWork.MultimediaHeaderChapterRepository.GetAll();
            return Ok(chapters);
        }
        [HttpGet("GetHeaderChapter/{id}")]
        public async Task<ActionResult<IEnumerable<MultimediaHeaderChapter>>> GetHeaderChapter(Guid id)
        {
            var chapters = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => f.MultimediaHeaderId == id);
            return Ok(chapters);
        }
        //[HttpPost("AddOrUpdate")]
        //public async Task<ActionResult<MultimediaHeaderChapter>> AddOrUpdate([FromBody] string data)
        //{
        //    var chapter = JsonConvert.DeserializeObject<MultimediaHeaderChapter>(data, ClsJason.IsoDateTimeConverter());
        //    await _unitOfWork.MultimediaHeaderChapterRepository.AddOrUpdate(chapter);
        //    _unitOfWork.Save();
        //    return Ok(chapter);
        //}
        
        
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultimediaHeaderChapter>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultimediaHeaderChapter>(data, ClsJason.IsoDateTimeConverter());
            var getPerson = await _unitOfWork.MultimediaHeaderChapterRepository.GetOrNew(g => g.Id == entity.Id);

            if (getPerson.gNew) // جدید است
            {
                var getMaxCode = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f=>f.MultimediaHeaderId== entity.MultimediaHeaderId);
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getPerson.item.Id = entity.Id;
                getPerson.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getPerson.item.Title = entity.Title;
            getPerson.item.Created = entity.Created;
            getPerson.item.Description = entity.Description;
            getPerson.item.MultimediaHeaderId = entity.MultimediaHeaderId;

            await _unitOfWork.MultimediaHeaderChapterRepository.AddOrUpdate(getPerson.item);

            _unitOfWork.Save();
            return Ok(entity);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var chapter = JsonConvert.DeserializeObject<MultimediaHeaderChapter>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultimediaHeaderChapterRepository.Update(chapter);
            _unitOfWork.Save();
            return Ok(chapter);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var chapter = await _unitOfWork.MultimediaHeaderChapterRepository.GetById(id);
            chapter.IsDelete = true;
            await _unitOfWork.MultimediaHeaderChapterRepository.Update(chapter);
            _unitOfWork.Save();
            return NoContent();
        }
        [HttpGet("GetByHeader/{id}")]
        public async Task<ActionResult<List<MultimediaHeaderChapter>>> GetByHeader(Guid id)
        {
            var chapter = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => f.MultimediaHeaderId == id);
            return Ok(chapter);
        }
    }
}
