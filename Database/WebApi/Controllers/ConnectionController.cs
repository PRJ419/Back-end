using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    /// <summary>
    /// Simple Controller to acknowledge IP and port
    /// </summary>
    [Route("api/Connection")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        /// <summary>
        /// Used to confirm IP and Port
        /// </summary>
        /// <returns>
        /// Returns 200 (Ok)
        /// </returns>
        [HttpGet]
        public IActionResult Confirm()
        {
            return Ok();
        }
    }
}