using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.IRepository;
using ParkyAPI.Models;
using ParkyAPI.Models.Dtos;

namespace ParkyAPI.Controllers
{
    [Route("api/v{version:apiVersion}/nationalparks")]
    [ApiVersion("2.0")]
    [ApiController]
    //[ApiExplorerSettings(GroupName = "ParkyOpenApiSpecForNP")]
    public class NationalParkV2Controllers : ControllerBase
    {
        private readonly INationalParkRepository _nationalParkRepository;
        private readonly IMapper _mapper;

        public NationalParkV2Controllers(INationalParkRepository nationalPark, IMapper mapper)
        {
            _nationalParkRepository = nationalPark;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List Of Parks
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<NationalParkDTO>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetNationalParks()
        {
            var obj = _nationalParkRepository.GetNationalParks().FirstOrDefault();

            return Ok(_mapper.Map<NationalParkDTO>(obj));
        }
    }
}