using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Access;
using IranKavosh.Layered.WebAPI.Middleware;
using IranKavosh.Layered.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultimediaHeaderController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultimediaHeaderController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any/{id}")]
        public async Task<ActionResult<bool>> Any(Guid id)
        {
            var items = await _unitOfWork.MultimediaHeaderRepository.Any(a => a.Id == id);
            return Ok(items);
        }
        //[HttpGet("GetImage/{locationId}/{num}")]
        //public IActionResult GetImage(Guid locationId, long num)

        [HttpGet("GetImage")]
        public IActionResult GetImage()
        {
            // var path = $"\\\\192.168.1.160\\Share\\{locationId}\\{num}\\Image.jpg";
            // var path = @"\\Img\Video4\2670\Image.jpg";
            var path = @"E:\Video4\2670\Image.jpg";
            if (!System.IO.File.Exists(path))
                return NotFound();

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpeg");
        }

        [HttpGet("GetImage2")]
        public async Task<IActionResult> GetImage2()
        {
            var path = @"E:\Video4\2670\Image.jpg";
            if (!System.IO.File.Exists(path))
                return NotFound();

            var lastModified = System.IO.File.GetLastWriteTimeUtc(path);
            var etag = $"\"{lastModified.Ticks}\"";

            Response.Headers["ETag"] = etag;

            if (Request.Headers["If-None-Match"] == etag)
                return StatusCode(304); // Not Modified

            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpeg");
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultimediaHeader>> GetById(Guid id)
        {
            var item = await _unitOfWork.MultimediaHeaderRepository.GetById(id);
            return Ok(item);
        }

        [HttpGet("GetChapterCount/{id}")]
        public async Task<ActionResult<ModelMultimediaHeader>> GetChapterCount(Guid id)
        {
            //var item = await _unitOfWork.MultimediaHeaderRepository.GetById(id);
            var getCntChapter =
                (await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => f.MultimediaHeaderId == id)).ToList();

            var getEndChapter = getCntChapter.Max(m => m.Code);

            var getMaxIdHeaderChapter =
                (await _unitOfWork.MultimediaHeaderChapterRepository.Find(f =>
                    f.MultimediaHeaderId == id && f.Code == getEndChapter)).ToList();


            var endChapterDetailsCount = getMaxIdHeaderChapter.First().MultimediaChapterDetails.Count;

            return Ok(new ModelMultimediaHeader
            {
                ChapterCount = getEndChapter,
                EndChapterDetailsCount = endChapterDetailsCount
            });
        }
        //
        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<ModelMultiHeader>>> Get()
        {
            //IEnumerable<ModelMultiHeader> data;
            var data = await _unitOfWork.MultimediaHeaderRepository.GetAll();
            return Ok(data.Select(s => new ModelMultiHeader
            {
                
                Id = s.Id,
                IMDB = s.IMDB,
                IsAnime = s.IsAnime,
                MultimediaGroupId = s.MultimediaGroupId,
                IsAnimated = s.IsAnimated,
                ActorsId = s.ActorsId,
                AgeId = s.AgeId,
                CountryId = s.CountryId,
                Created = s.Created,
                DayOfWeek = s.DayOfWeek,
                LocationId = s.LocationId,
                MultiGenreLists = s.MultiGenreLists,
                NameEnglish = s.NameEnglish,
                NamePersian = s.NamePersian,
                Num = s.Num,
                Description = s.Description,
                PriceId = s.PriceId,
                DateTime = s.DateTime,
                IsDelete = s.IsDelete,
                IsActive = s.IsActive,
                
            }));
        }

        //[HttpGet("Get")]
        //public async Task<ActionResult<IEnumerable<MultimediaHeader>>> Get()
        //{
        //    var data = await _unitOfWork.MultimediaHeaderRepository.GetAll();
        //    return Ok(data);
        //}
        [HttpGet("GetMaxCode")]
        public async Task<ActionResult> GetMaxCode()
        {
            var data = await _unitOfWork.MultimediaHeaderRepository.GetAll();

            return Ok(data.Select(s => s.Num).DefaultIfEmpty(0).Max() + 1);
        }

        //class ModelDir
        //{
        //    public string Dir { get; set; }

        //    public int Count { get; set; }
        //}
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultimediaHeader>> AddOrUpdate([FromBody] string data)
        {

            #region بررسی محل ذخیره فیلم ها

            //List<ModelDir> lstDirs = new List<ModelDir>();
            //var getDirs = (await _unitOfWork.MultiLocationRepository.GetAll()).ToList();
            //foreach (MultiLocation l in getDirs)
            //{
            //    lstDirs.Add(new ModelDir
            //    {
            //        Dir = l.DirShare_PCName,
            //        Count = Directory.GetDirectories(l.Root).Length
            //    });
            //}

            #endregion

            var entity = JsonConvert.DeserializeObject<MultimediaHeader>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultimediaHeaderRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultimediaHeader>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultimediaHeaderRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("UpdateDescription/{id}")]
        public async Task<IActionResult> UpdateDescription(Guid id, [FromBody] string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return BadRequest();

            var entity = JsonConvert.DeserializeObject<string>(data, ClsJason.IsoDateTimeConverter());

            if (entity == null)
                return BadRequest();

            var header = await _unitOfWork.MultimediaHeaderRepository.GetById(id);
            if (header == null)
                return NotFound();

            header.Description = entity;
            await _unitOfWork.MultimediaHeaderRepository.Update(header);
             _unitOfWork.Save();

            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _unitOfWork.MultimediaHeaderRepository.GetById(id);
            item.IsDelete = true;
            await _unitOfWork.MultimediaHeaderRepository.Update(item);
            return Ok(_unitOfWork.Save());
        }

        [HttpGet("LocationForImage/{id}")]
        public async Task<IActionResult> LocationForImage(Guid id)
        {
            var item = await _unitOfWork.MultimediaHeaderRepository.GetById(id);
            var item2 = await _unitOfWork.MultiLocationRepository.Find(f => f.Id == item.LocationId);
            var entity = JsonConvert.SerializeObject(item2.First().DirShare_PCIP, ClsJason.IsoDateTimeConverter());
            return Ok(entity);

            // return NoContent();
        }

        //[HttpGet("Image/{id}")]   
        //public async Task<IActionResult> Image(Guid id)
        //{
        //    try
        //    {
        //        var item = await _unitOfWork.MultimediaHeaderRepository.GetById(id);
        //        var item2 = await _unitOfWork.MultiLocationRepository.Find(f => f.Id == item.LocationId);
        //        var path = Path.Combine(item2.First().Root, item.Num.ToString(), "Image.jpg");
        //        Console.WriteLine(path);
        //        var bytes = await System.IO.File.ReadAllBytesAsync(path);
        //        return File(bytes, "image/jpeg");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return BadRequest(e.Message.ToString());
        //    }

        //}
        [HttpGet("Image/{id}")]
        public async Task<IActionResult> Image(Guid id)
        {
            try
            {
                var item = await _unitOfWork.MultimediaHeaderRepository.GetById(id);
                if (item == null)
                    return NotFound("Header not found");

                var item2 = await _unitOfWork.MultiLocationRepository.Find(f => f.Id == item.LocationId);
                if (item2 == null || !item2.Any())
                    return NotFound("Location not found");

                var path = Path.Combine(item2.First().Root, item.Num.ToString(), "Image.jpg");
                if (!System.IO.File.Exists(path))
                    return NotFound($"File not found: {path}");

                var lastModified = System.IO.File.GetLastWriteTimeUtc(path);
                var etag = $"\"{lastModified.Ticks}\"";

                Response.Headers["ETag"] = etag;

                if (Request.Headers["If-None-Match"] == etag)
                    return StatusCode(304); // Not Modified

                var bytes = await System.IO.File.ReadAllBytesAsync(path);
                return File(bytes, "image/jpeg");
            }
            catch (Exception e)
            {
                Console.WriteLine($"خطا در دریافت تصویر: {e.Message}");
                return BadRequest("خطای داخلی سرور");
            }
        }

        [HttpGet("GetByHeaderTranslate/{id}")]
        public async Task<ActionResult<List<ModelChapterDetailsFull>>> GetByHeaderTranslate(Guid id)
        {
            //var lstChapterDetailsIds = JsonConvert.DeserializeObject<List<ModelGuidFaEn>>(data, ClsJason.IsoDateTimeConverter());


            var getHeader = await _unitOfWork.MultimediaHeaderRepository.GetById(id);


            var getHeaderChapter = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => f.MultimediaHeaderId == id);
            var lstChapterDetailsIds = await _unitOfWork.MultimediaChapterDetailRepository.Find(f => getHeaderChapter.Any(a => a.Id == f.HeaderChapterId));
            var dataLocation = await _unitOfWork.MultiLocationRepository.GetAll();
            var chapterDetailIds = lstChapterDetailsIds.Select(x => x.Id).ToList();
            var allChapterDetails = await _unitOfWork.MultimediaChapterDetailRepository.Find(f => chapterDetailIds.Contains(f.Id));
            var allHeaderChapterIds = allChapterDetails.Select(x => x.HeaderChapterId).Distinct().ToList();
            var allHeaderChapters = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => allHeaderChapterIds.Contains(f.Id));

            List<ModelChapterDetailsFull> detailsFulls = [];

            foreach (var chapterDetail in allChapterDetails)
            {
                var headerChapter = allHeaderChapters.FirstOrDefault(h => h.Id == chapterDetail.HeaderChapterId);
                if (headerChapter == null) continue;

                //var header = getHeader.FirstOrDefault(h => h.Id == headerChapter.MultimediaHeaderId);
                //var header = getHeader.FirstOrDefault(h => h.Id == headerChapter.MultimediaHeaderId);
                //if (header == null) continue;

                var location = dataLocation.FirstOrDefault(l => l.Id == getHeader.LocationId);
                if (location == null) continue;

                var original = lstChapterDetailsIds.FirstOrDefault(x => x.Id == chapterDetail.Id);
                if (original == null) continue;

                detailsFulls.Add(new ModelChapterDetailsFull
                {
                    ChapterDetailsId = chapterDetail.Id,
                    HeaderId = getHeader.Id,
                    HeaderChapterId = headerChapter.Id,

                    HeaderName = getHeader.NamePersian,
                    Type = "Fa || En",// Fa || En
                    HeaderCode = getHeader.Num,
                    ChapterCode = chapterDetail.Code,
                    HeaderChapterCode = headerChapter.Code,
                    AddressFile = Path.Combine(location.DirShare_PCIP, getHeader.Num.ToString(), headerChapter.Code.ToString(), chapterDetail.Code.ToString()),
                    ImageDir = Path.Combine(location.DirShare_PCIP, getHeader.Num.ToString(), "Image.jpg"),
                });
            }


            return Ok(detailsFulls);
        }

    }
}
