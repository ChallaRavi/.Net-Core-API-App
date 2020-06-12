using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.IRepository;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;

namespace ParkyAPI.Controllers
{
    //[Route("api/Trails")]
    [Route("api/v{version:apiVersion}/trails")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenApiSpecForTrails")]
    public class TrailsControllers : ControllerBase
    {
        private readonly ITrailRepository _trailRepository;
        private readonly IMapper _mapper;

        public TrailsControllers(ITrailRepository trailRepo, IMapper mapper)
        {
            _trailRepository = trailRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List Of Trails
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type =typeof(List<TrailDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetTrails()
        {
            var objList = _trailRepository.GetTrails();
            var objDTO = new List<TrailDTO>();
            foreach (var obj in objList)
            {
                objDTO.Add(_mapper.Map<TrailDTO>(obj));
            }
            return Ok(objDTO);
        }
        /// <summary>
        /// Get Individual Trailss
        /// </summary>
        /// <param name="trailId">Takes The Id of the trail</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name ="GetTrail")]
        [ProducesResponseType(200, Type = typeof(TrailDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrail(int trailId)
        {
            var obj = _trailRepository.GetTrail(trailId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDTO = _mapper.Map<TrailDTO>(obj);

            return Ok(objDTO);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDTO))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailCreateDTO trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_trailRepository.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "Trail Exists!");
                return StatusCode(404, ModelState);
            }
            var trailObj = _mapper.Map<Trail>(trailDto);
            if (!_trailRepository.CreateTrail(trailObj))
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {trailObj.Name}");
                return StatusCode(500, ModelState);
            }
            return CreatedAtRoute("GetTrail", new { trailId = trailObj.Id }, trailObj);
        }
    }
}