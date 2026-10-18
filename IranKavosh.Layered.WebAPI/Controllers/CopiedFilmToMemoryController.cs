using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CopiedFilmToMemoryController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public CopiedFilmToMemoryController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<CopiedFilmToMemory>>> Any()
        {
            var list = await _unitOfWork.CopiedFilmToMemoryRepository.Any();
            return Ok(list);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CopiedFilmToMemory>> GetById(Guid id)
        {
            var entity = await _unitOfWork.CopiedFilmToMemoryRepository.GetById(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<CopiedFilmToMemory>>> Get()
        {
            var data = await _unitOfWork.CopiedFilmToMemoryRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<CopiedFilmToMemory>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<CopiedFilmToMemory>(data, ClsJason.IsoDateTimeConverter());
            if (entity == null) 
                return BadRequest();

            #region Get Price

            //var getPrice = _unitOfWork.MultimediaHeaderRepository.GetAll().Result
            //    .Where(s =>
            //    {
            //        bool Predicate(MultimediaHeaderChapter w)
            //        {
            //            var any = w.MultimediaChapterDetails.Any(a => a.Id == entity.ChapterDetailId);
            //            return any;
            //        }

            //        return s.HeaderChapters.Any(Predicate);
            //    }).First().Price.Price;

            var getChapterDetails = _unitOfWork.MultimediaChapterDetailRepository.GetById(entity.ChapterDetailId).Result;
            var getHeaderChapter = _unitOfWork.MultimediaHeaderChapterRepository.GetById(getChapterDetails.HeaderChapterId).Result;
            var getHeader = _unitOfWork.MultimediaHeaderRepository.GetById(getHeaderChapter.MultimediaHeaderId).Result;
            var getPrice = _unitOfWork.MultiPriceRepository.GetById(getHeader.PriceId).Result;
           
            #endregion

            entity.Price = getPrice.Price;
            await _unitOfWork.CopiedFilmToMemoryRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<CopiedFilmToMemory>> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<CopiedFilmToMemory>(data, ClsJason.IsoDateTimeConverter());
            if (entity == null) return BadRequest();
            await _unitOfWork.CopiedFilmToMemoryRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _unitOfWork.CopiedFilmToMemoryRepository.GetById(id);
            if (entity == null) return NotFound();
            entity.IsDelete = true;
            await _unitOfWork.CopiedFilmToMemoryRepository.Update(entity);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}