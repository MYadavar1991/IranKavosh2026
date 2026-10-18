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
    public class MultiGenreController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultiGenreController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<MultiGenre>>> Any()
        {
            var genres = await _unitOfWork.MultiGenreRepository.Any();
            return Ok(genres);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultiGenre>> GetById(Guid id)
        {
            var genre = await _unitOfWork.MultiGenreRepository.GetById(id);
            return Ok(genre);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MultiGenre>>> Get()
        {
            var data = await _unitOfWork.MultiGenreRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultiGenre>> AddOrUpdate([FromBody] string data)
        {
            var genre = JsonConvert.DeserializeObject<MultiGenre>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiGenreRepository.AddOrUpdate(genre);
            _unitOfWork.Save();
            return Ok(genre);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var genre = JsonConvert.DeserializeObject<MultiGenre>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiGenreRepository.Update(genre);
            _unitOfWork.Save();
            return Ok(genre);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var genre = await _unitOfWork.MultiGenreRepository.GetById(id);
            genre.IsDelete = true;
            await _unitOfWork.MultiGenreRepository.Update(genre);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpPost("AddIfNew")]
        public async Task<ActionResult<MultiActor>> AddIfNew([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<List<ModelGenre>>(data, ClsJason.IsoDateTimeConverter());

            var getActor = (await _unitOfWork.MultiGenreRepository.GetAll()).Select(s => s.Title.Trim()).ToList();

            // حذف رشته‌های مشترک
            var uniqueEntities = entity.Where(e => !getActor.Contains(e.TitleEn)).ToList();
            //var uniqueEntities = entity.Where(e => !getActor.Contains(e)).ToList();

            foreach (var item in uniqueEntities)
            {
                MultiGenre multiGenre = new MultiGenre
                {
                    Title = item.Title.Trim(),
                    TitleEn = item.TitleEn.Trim()
                };
                if (!_unitOfWork.MultiGenreRepository.Any(a => a.TitleEn.Trim() == item.TitleEn.Trim()).Result)
                {
                    await _unitOfWork.MultiGenreRepository.AddOrUpdate(multiGenre);
                }

            }

            //  await _unitOfWork.MultiActorRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }
    }
}
