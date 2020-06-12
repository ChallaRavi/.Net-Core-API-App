using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;

namespace ParkyWeb.Controllers
{
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository _nationalParkRepo;

        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
            _nationalParkRepo = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }

        public async Task<IActionResult> GetAllNationalPark() 
        {
            return Json(new { data = await _nationalParkRepo.GetAllAsync(SD.NationalParkAPIPath) });
        }
    }
}
