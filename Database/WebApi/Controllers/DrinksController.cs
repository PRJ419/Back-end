using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/bars/{BarName}/Drinks")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public DrinksController(IUnitOfWork UnitOfWork)
        {
            _unitOfWork = UnitOfWork;
        }

        [HttpGet]
        public async Task<string> GetDrinks(string BarName)
        {
            var u = new UnitOfWork;
            u.
        }
    }
}