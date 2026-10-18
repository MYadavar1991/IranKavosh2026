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
    public class MultiLocationController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;

        public MultiLocationController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("Any")]
        public async Task<ActionResult<IEnumerable<MultiLocation>>> Any()
        {
            var locations = await _unitOfWork.MultiLocationRepository.Any();
            return Ok(locations);
        }

        [HttpGet("GetSizeHDD")]
        public async Task<ActionResult<IEnumerable<ModelSizeHDD>>> GetSizeHDD()
        {

            var data = await _unitOfWork.MultiLocationRepository.GetAll();
            IEnumerable<DriveInfo> drives = DriveInfo.GetDrives();

            // استخراج حروف درایوها از دیتا
            var driveLetters = data.Select(s => s.Root?.Substring(0, 1).ToUpper()).Distinct().ToList();

            // گرفتن درایوهایی که حرفشان در لیست باشد
            var getD2 = drives
                .Where(d => d.IsReady && driveLetters.Contains(d.Name.Substring(0, 1).ToUpper())).Select(s => new ModelSizeHDD
                {
                    Drive = s.Name.Substring(0, 1),
                    Total = (float)s.TotalSize / 1024 / 1024 / 1024,
                    Free = (float)s.TotalFreeSpace / 1024 / 1024 / 1024,
                });

            //var getSize = data.Select(s => new { }).ToList();
            return Ok(getD2);
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<MultiLocation>> GetById(Guid id)
        {
            var location = await _unitOfWork.MultiLocationRepository.GetById(id);
            return Ok(location);
        }

        [HttpGet("Get")]
        public async Task<ActionResult<IEnumerable<MultiLocation>>> Get()
        {
            var data = await _unitOfWork.MultiLocationRepository.GetAll();
            return Ok(data);
        }

        [HttpPost("AddOrUpdate")]
        public async Task<ActionResult<MultiLocation>> AddOrUpdate([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultiLocation>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiLocationRepository.AddOrUpdate(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] string data)
        {
            var entity = JsonConvert.DeserializeObject<MultiLocation>(data, ClsJason.IsoDateTimeConverter());
            await _unitOfWork.MultiLocationRepository.Update(entity);
            _unitOfWork.Save();
            return Ok(entity);
        }

        [HttpPut("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var entity = await _unitOfWork.MultiLocationRepository.GetById(id);
            entity.IsDelete = true;
            await _unitOfWork.MultiLocationRepository.Update(entity);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}
