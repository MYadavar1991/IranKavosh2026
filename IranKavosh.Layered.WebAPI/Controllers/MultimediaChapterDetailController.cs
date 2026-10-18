using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Repository;
using IranKavosh.Layered.WebAPI.Middleware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IranKavosh.Layered.WebAPI.Models;

namespace IranKavosh.Layered.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MultimediaChapterDetailController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultimediaChapterDetailController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<MultimediaChapterDetail>>> Any()
        {
            var items = await _unitOfWork.MultimediaChapterDetailRepository.Any();
            return Ok(items);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultimediaChapterDetail>> GetById(Guid id)
        {
            var item = await _unitOfWork.MultimediaChapterDetailRepository.GetById(id);
            return Ok(item);
        }

        [HttpGet("GetPrice/{id}")]
        public async Task<ActionResult<long>> GetPrice(Guid id)
        {
            var getChapterDetails = _unitOfWork.MultimediaChapterDetailRepository.GetById(id).Result;
            var getHeaderChapter = _unitOfWork.MultimediaHeaderChapterRepository.GetById(getChapterDetails.HeaderChapterId).Result;
            var getHeader = _unitOfWork.MultimediaHeaderRepository.GetById(getHeaderChapter.MultimediaHeaderId).Result;
            var getPrice = _unitOfWork.MultiPriceRepository.GetById(getHeader.PriceId).Result;


            //var getPrice = _unitOfWork.MultimediaHeaderRepository.GetAll().Result
            //    .Where(s =>
            //    {
            //        bool Predicate(MultimediaHeaderChapter w)
            //        {
            //            var any = w.MultimediaChapterDetails.Any(a => a.Id == id);
            //            return any;
            //        }

            //        return s.HeaderChapters.Any(Predicate);
            //    }).First().Price.Price;

            return Ok(getPrice.Price);
        }

        //public async Task<ActionResult<long>> GetPrice(Guid id)
        //{
        //    var getPrice = _unitOfWork.MultimediaHeaderRepository.GetAll().Result
        //        .Where(s =>
        //        {
        //            bool Predicate(MultimediaHeaderChapter w)
        //            {
        //                var any = w.MultimediaChapterDetails.Any(a => a.Id == id);
        //                return any;
        //            }

        //            return s.HeaderChapters.Any(Predicate);
        //        }).First().Price.Price;

        //    return Ok(getPrice);
        //}

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MultimediaChapterDetail>>> Get()
        {
            var items = await _unitOfWork.MultimediaChapterDetailRepository.GetAll();
            return Ok(items);
        }

        [HttpPost("AddChapterCounts/{headerChapterId}/{chapterCount}")]
        public async Task<ActionResult<IEnumerable<MultimediaChapterDetail>>> AddChapterCounts(Guid headerChapterId, int chapterCount) //3
        {
            var getData =
                (await _unitOfWork.MultimediaChapterDetailRepository.Find(f => f.HeaderChapterId == headerChapterId)).ToList();// f => f.HeaderChapterId == headerChapterId);
            var getMaxCode = getData.Select(s => s.Code).DefaultIfEmpty(0).Max();
            var lstEntity = new List<MultimediaChapterDetail>();

            for (long i = getMaxCode; i < (chapterCount + getMaxCode); i++) // i = 4 ; 4 < (7)
            {
                lstEntity.Add(new MultimediaChapterDetail
                {
                    Id = Guid.NewGuid(),
                    HeaderChapterId = headerChapterId,
                    Code = i + 1,
                });
            }

            _unitOfWork.MultimediaChapterDetailRepository.AddRange(lstEntity);
            _unitOfWork.Save();
            var getSer = JsonConvert.SerializeObject(lstEntity);
            return Ok(getSer);
        }
        
        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultimediaChapterDetail>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultimediaChapterDetail>(data, ClsJason.IsoDateTimeConverter());
            var getChapterDetail = await _unitOfWork.MultimediaChapterDetailRepository.GetOrNew(g => g.Id == entity.Id);

            if (getChapterDetail.gNew) // جدید است
            {
                //var getMaxCode = await _unitOfWork.MultimediaChapterDetailRepository.GetAll();
                var getMaxCode = await _unitOfWork.MultimediaChapterDetailRepository.Find(f => f.HeaderChapterId == entity.HeaderChapterId);
                var maxCode = getMaxCode.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                getChapterDetail.item.Code = maxCode;
            }
            else
            {
                // getPerson.item.Code = entity.Code;
            }

            getChapterDetail.item.ChapterTypeId = entity.ChapterTypeId;
            getChapterDetail.item.Extension = entity.Extension;
            getChapterDetail.item.HeaderChapterId = entity.HeaderChapterId;
            getChapterDetail.item.IsHDTS = entity.IsHDTS;
            getChapterDetail.item.IsWebDl = entity.IsWebDl;
            getChapterDetail.item.Description = entity.Description;

            await _unitOfWork.MultimediaChapterDetailRepository.AddOrUpdate(getChapterDetail.item);

            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultimediaChapterDetail>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultimediaChapterDetailRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _unitOfWork.MultimediaChapterDetailRepository.GetById(id);
            item.IsDelete = true;
            await _unitOfWork.MultimediaChapterDetailRepository.Update(item);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpGet("GetByHeaderChapter/{id}")]
        public async Task<ActionResult<List<MultimediaChapterDetail>>> GetByHeaderChapter(Guid id)
        {
            var chapter = await _unitOfWork.MultimediaChapterDetailRepository.Find(f => f.HeaderChapterId == id);
            return Ok(chapter);
        }

        [HttpGet("GetByHeader/{id}")]
        public async Task<ActionResult<List<MultimediaChapterDetail>>> GetByHeader(Guid id)
        {
            var header = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => f.MultimediaHeaderId == id);
            List<MultimediaChapterDetail> chapterDetail = new List<MultimediaChapterDetail>();

            foreach (MultimediaHeaderChapter hc in header)
            {
                var chapter = await _unitOfWork.MultimediaChapterDetailRepository.Find(f => f.HeaderChapterId == hc.Id);
                chapterDetail.AddRange(chapter);
            }

            return Ok(chapterDetail);
        }

        [HttpGet("GetByHeaderTranslate2")]
        public async Task<ActionResult<List<ModelChapterDetailsFull>>> GetByHeaderTranslate2([FromBody] string data)
        {
            var lstChapterDetailsIds = JsonConvert.DeserializeObject<List<ModelGuidFaEn>>(data, ClsJason.IsoDateTimeConverter());

            var getHeader = await _unitOfWork.MultimediaHeaderRepository.GetAll();
            var dataLocation = await _unitOfWork.MultiLocationRepository.GetAll();

            List<ModelChapterDetailsFull> detailsFulls = new();

            foreach (var getChapterDetails in lstChapterDetailsIds)
            {
                var dataChapterDetails1 = await _unitOfWork.MultimediaChapterDetailRepository.Find(f => f.Id == getChapterDetails.ChapterDetailId);
                //var dataHeaderChapter = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => f.Id == headerChapterId);

                var dataChapterDetails11 = dataChapterDetails1.ToList();
                foreach (var chapterDetails in dataChapterDetails11)
                {
                    var getHeaderChapter2 = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => f.Id == chapterDetails.HeaderChapterId);

                    var multimediaHeaderChapters = getHeaderChapter2.ToList();
                    foreach (var headerChapter in multimediaHeaderChapters)
                    {
                        var dateHeader = getHeader.First(f => f.Id == headerChapter.MultimediaHeaderId);
                        var getLocation = dataLocation.First(f => f.Id == dateHeader.LocationId);
                        var detailsFull = new ModelChapterDetailsFull
                        {
                            ChapterDetailsId = getChapterDetails.ChapterDetailId,
                            HeaderName = dateHeader.NamePersian,
                            Type = getChapterDetails.Type,
                            HeaderCode = dateHeader.Num, // کد فیلم
                            ChapterCode = chapterDetails.Code, // قسمت
                            HeaderChapterCode = headerChapter.Code, // فصل

                            AddressFile = Path.Combine(getLocation.DirShare_PCIP, dateHeader.Num.ToString(), headerChapter.Code.ToString(), chapterDetails.Code.ToString())

                        };
                        detailsFulls.Add(detailsFull);
                    }
                }
            }

            return Ok(detailsFulls);
        }

        [HttpGet("GetByHeaderTranslate")]
        public async Task<ActionResult<List<ModelChapterDetailsFull>>> GetByHeaderTranslate([FromBody] string data)
        {
            var lstChapterDetailsIds = JsonConvert.DeserializeObject<List<ModelGuidFaEn>>(data, ClsJason.IsoDateTimeConverter());

            var getHeader = await _unitOfWork.MultimediaHeaderRepository.GetAll();
            var dataLocation = await _unitOfWork.MultiLocationRepository.GetAll();

            var chapterDetailIds = lstChapterDetailsIds.Select(x => x.ChapterDetailId).ToList();
            var allChapterDetails = await _unitOfWork.MultimediaChapterDetailRepository.Find(f => chapterDetailIds.Contains(f.Id));
            var allHeaderChapterIds = allChapterDetails.Select(x => x.HeaderChapterId).Distinct().ToList();
            var allHeaderChapters = await _unitOfWork.MultimediaHeaderChapterRepository.Find(f => allHeaderChapterIds.Contains(f.Id));

            List<ModelChapterDetailsFull> detailsFulls = [];

            foreach (var chapterDetail in allChapterDetails)
            {
                var headerChapter = allHeaderChapters.FirstOrDefault(h => h.Id == chapterDetail.HeaderChapterId);
                if (headerChapter == null) continue;

                var header = getHeader.FirstOrDefault(h => h.Id == headerChapter.MultimediaHeaderId);
                if (header == null) continue;

                var location = dataLocation.FirstOrDefault(l => l.Id == header.LocationId);
                if (location == null) continue;

                var original = lstChapterDetailsIds.FirstOrDefault(x => x.ChapterDetailId == chapterDetail.Id);
                if (original == null) continue;

                detailsFulls.Add(new ModelChapterDetailsFull
                {
                    ChapterDetailsId = chapterDetail.Id,
                    HeaderId = header.Id,
                    HeaderChapterId = headerChapter.Id,

                    HeaderName = header.NamePersian,
                    Type = original.Type,
                    HeaderCode = header.Num,
                    ChapterCode = chapterDetail.Code,
                    HeaderChapterCode = headerChapter.Code,
                    AddressFile = Path.Combine(location.DirShare_PCIP, header.Num.ToString(), headerChapter.Code.ToString(), chapterDetail.Code.ToString()),
                    ImageDir = Path.Combine(location.DirShare_PCIP, header.Num.ToString(), "Image.jpg"),
                });
            }


            return Ok(detailsFulls);
        }

        [HttpGet("GetMaxCode/{id}")]
        public async Task<ActionResult<List<MultimediaChapterDetail>>> GetMaxCode(Guid id)
        {
            var chapter = await _unitOfWork.MultimediaChapterDetailRepository.Find(f => f.HeaderChapterId == id);
            var getMaxCode = chapter.Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
            return Ok(getMaxCode);
        }
  
        [HttpPut("IsWebDl/{id}")]
        public async Task<ActionResult> IsWebDl(Guid id, [FromBody] bool webDl)
        {
            //   var _webDl = JsonConvert.DeserializeObject<bool>(webDl, ClsJason.IsoDateTimeConverter());
            var chapter = await _unitOfWork.MultimediaChapterDetailRepository.GetById(id);
            chapter.IsWebDl = webDl;

            await _unitOfWork.MultimediaChapterDetailRepository.Update(chapter);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpGet("PlayVideo2")]
        public async Task<IActionResult> PlayVideo2([FromQuery] string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return BadRequest("Address is required.");

            var decodedPath = Uri.UnescapeDataString(address);
            if (!System.IO.File.Exists(decodedPath))
                return NotFound("File not found.");

            var fileInfo = new FileInfo(decodedPath);
            var totalLength = fileInfo.Length;
            var request = HttpContext.Request;

            // بررسی وجود Range header
            if (!request.Headers.ContainsKey("Range"))
            {
                var stream = System.IO.File.OpenRead(decodedPath);
                Response.Headers.Add("Accept-Ranges", "bytes");
                return File(stream, "video/mp4", enableRangeProcessing: true);
            }

            // پردازش Range header
            var rangeHeader = request.Headers["Range"].ToString(); // e.g. "bytes=1000-"
            var range = rangeHeader.Replace("bytes=", "").Split('-');
            var start = long.Parse(range[0]);
            var end = range.Length > 1 && !string.IsNullOrEmpty(range[1]) ? long.Parse(range[1]) : totalLength - 1;
            var length = end - start + 1;

            var buffer = new byte[64 * 1024]; // 64KB buffer
            var streamPart = new FileStream(decodedPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            streamPart.Seek(start, SeekOrigin.Begin);

            Response.StatusCode = 206;
            Response.ContentType = "video/mp4";
            Response.ContentLength = length;
            Response.Headers.Add("Content-Range", $"bytes {start}-{end}/{totalLength}");
            Response.Headers.Add("Accept-Ranges", "bytes");
            Response.Headers.Add("Cache-Control", "no-store");
            Response.Headers.Add("Content-Disposition", "inline");

            long remaining = length;
            while (remaining > 0)
            {
                int read = await streamPart.ReadAsync(buffer, 0, (int)Math.Min(buffer.Length, remaining));
                if (read == 0) break;
                await Response.Body.WriteAsync(buffer, 0, read);
                remaining -= read;
            }

            await Response.Body.FlushAsync();
            return new EmptyResult();
        }

        [HttpGet("PlayVideo")]
        public async Task<IActionResult> PlayVideo([FromQuery] string address)
        {
            var buffer = new byte[64 * 1024]; // 64KB buffer

            if (string.IsNullOrWhiteSpace(address))
                return BadRequest("Address is required.");

            var decodedPath = Uri.UnescapeDataString(address);
            Console.WriteLine($"Decoded path: {decodedPath}");
            if (!System.IO.File.Exists(decodedPath))
                return NotFound("File not found.");

            var fileInfo = new FileInfo(decodedPath);
            var totalLength = fileInfo.Length;
            var request = HttpContext.Request;

            long start = 0;
            long end = totalLength - 1;

            if (request.Headers.ContainsKey("Range"))
            {
                var rangeHeader = request.Headers["Range"].ToString(); // e.g. "bytes=5000000-"
                var range = rangeHeader.Replace("bytes=", "").Split('-');
                if (!long.TryParse(range[0], out start))
                    return BadRequest("Invalid Range header.");

                end = range.Length > 1 && long.TryParse(range[1], out var parsedEnd)
                    ? Math.Min(parsedEnd, totalLength - 1)
                    : totalLength - 1;

                if (start >= totalLength)
                {
                    Response.StatusCode = 416;
                    Response.Headers.Add("Content-Range", $"bytes */{totalLength}");
                    return new EmptyResult();
                }
            }

            var length = end - start + 1;
            var megabytesSent = length / (1024.0 * 1024.0);

            var stream = new FileStream(decodedPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            stream.Seek(start, SeekOrigin.Begin);

            Response.StatusCode = request.Headers.ContainsKey("Range") ? 206 : 200;
            Response.ContentType = "video/mp4";
            Response.ContentLength = length;
            Response.Headers.Add("Content-Range", $"bytes {start}-{end}/{totalLength}");
            Response.Headers.Add("Accept-Ranges", "bytes");
            Response.Headers.Add("Content-Disposition", "inline");
            Response.Headers.Add("Cache-Control", "no-store");

            // گزارش مصرف به کلاینت
            Response.Headers.Add("X-Bytes-Sent", length.ToString());
            Response.Headers.Add("X-MB-Sent", megabytesSent.ToString("F2"));

            // لاگ سمت سرور
            Console.WriteLine($"Video chunk sent: {megabytesSent:F2} MB from byte {start} to {end}");

            #region Save MB Use

            var getPerson = await _unitOfWork.PersonalRepository.GetById(Guid.Parse("c335a662-c985-45c8-a9aa-101ca258157d"));
            getPerson.MBUsable = (int)length;
            await _unitOfWork.PersonalRepository.AddOrUpdate(getPerson);
            _unitOfWork.Save();
            #endregion

            long remaining = length;
            while (remaining > 0)
            {
                int read = await stream.ReadAsync(buffer, 0, (int)Math.Min(buffer.Length, remaining));
                if (read == 0) break;
                await Response.Body.WriteAsync(buffer, 0, read);
                remaining -= read;
            }

            await Response.Body.FlushAsync();
            return new EmptyResult();
        }
    }
}
