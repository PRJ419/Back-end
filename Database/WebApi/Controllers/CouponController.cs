using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Database;
using Database.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.DTOs.Coupon;
using WebApi.Utility;

namespace WebApi.Controllers
{
    /// <summary>
    /// Web Api Controller for coupons.<para/>
    /// Returns CouponDto objects
    /// </summary>
    [Route("api/bars/{barName}/coupons")]
    [ApiController]
    public class CouponController : ControllerBase
    {

        /// <summary>
        /// Reference to unit of work, used for database access. 
        /// </summary>
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// Field to store IMapper implementation.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor for the controller. <para/>
        /// Gets a IUnitOfWork by dependency injection (configured in Startup.cs)
        /// </summary>
        /// <param name="unitOfWork">
        /// UnitOfWork implementation used for database access. 
        /// </param>
        /// <param name="mapper">
        /// IMapper implementation used to map Dto object to model objects and vice versa. 
        /// </param>
        public CouponController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all Coupons associated with the bar.
        /// Authorization: None
        /// </summary>
        /// <returns>
        /// Ok (200) and a List&lt;CouponDto&gt; of all the Coupons linked to the bar<para></para>
        /// NotFound (404) if no Coupons were found. <para></para>
        /// </returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(List<CouponDto>), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult GetCoupons([FromRoute] string barName)
        {
            var coupons = _unitOfWork.CouponRepository.Find(c => c.BarName == barName);
            var couponDtoList = Converter.GenericListConvert
                <Coupon, CouponDto>(coupons, _mapper);

            if (couponDtoList.Any())
            {
                return Ok(couponDtoList);
            }
            else
                return NotFound();
        }

        /// <summary>
        /// Returns a CouponDto found by couponId and barName.
        /// Authorization: None
        /// </summary>
        /// <param name="couponId">
        /// is a string and one part of the key for a Coupon
        /// </param>
        /// <param name="barName">
        /// is a string and one part of the key for a Coupon 
        /// </param>
        /// <returns>
        /// Ok (200) and a CouponDto object equivalent of the Coupon saved in the database if found.<para></para>
        /// NotFound (404) if the Coupon was not found. 
        /// </returns>
        [HttpGet("{couponId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CouponDto), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        public IActionResult GetCoupon(string couponId, string barName)
        {
            var coupon = _unitOfWork.CouponRepository.Get(new object[] {barName, couponId});

            if (coupon == null)
                return NotFound();

            return Ok(_mapper.Map<CouponDto>(coupon));
        }

        /// <summary>
        /// Adds a Coupon to the database. 
        /// Authorization: Admin, BarRepresentative
        /// </summary>
        /// <param name="couponDto">
        /// is a CouponDto object. Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if Coupon was added. <para></para>
        /// BadRequest (400) if model requirements weren't.. Body will contain string: "Duplicate Key"
        /// if request failed because of duplicate key sql exception <para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// </returns>
        [HttpPost]
        [Authorize( Roles = "Admin,BarRep")]
        [ProducesResponseType(typeof(CouponDto), StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult AddCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _unitOfWork.CouponRepository.Add(coupon);
                _unitOfWork.Complete(); // TODO: Wrong route discovered in unit tests, corrected now. 
                return Created(string.Format($"api/bars/{coupon.BarName}/coupons/{coupon.CouponID}"), couponDto);
            }
            catch (Exception e)
            {
                if (e.InnerException is SqlException exception && exception.Number == 2627)
                {
                    return BadRequest("Duplicate Key");
                }
                return BadRequest();
            }
        }

        /// <summary>
        /// Edits an Coupon.
        /// Authorization: Admin, BarRepresentative.
        /// </summary>
        /// <param name="couponDto">
        /// is a CouponDto which holds edited data. <para></para>
        /// Must hold a key (CouponId, BarName) which can be found in the database.  <para></para>
        /// Must match property attribute rules. 
        /// </param>
        /// <returns>
        /// Created (201) if edit was successful. <para></para>
        /// BadRequest (400) if edit was unsuccessful. See parameter requirements.<para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// </returns>
        [HttpPut]
        [Authorize( Roles = "Admin,BarRep")]
        [ProducesResponseType(typeof(CouponDto), StatusCodes.Status201Created)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult EditCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                _unitOfWork.CouponRepository.Edit(coupon);
                _unitOfWork.Complete();
                return Created(string.Format($"api/bars/{coupon.BarName}/coupons/{coupon.CouponID}"), couponDto);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes an Coupon. 
        /// Authorization: Admin, BarRepresentative
        /// </summary>
        /// <param name="couponId">
        /// is a string holding the couponId part of the key
        /// </param>
        /// <param name="barName">
        /// is a string holding the barName part of the key
        /// </param>
        /// <returns>
        /// Ok (200) if deletion was successful. <para></para>
        /// BadRequest (400) if deletion was unsuccessful. <para></para>
        /// Unauthorized (401) if authentication is unsuccessful. <para></para>
        /// </returns>
        [HttpDelete("{couponId}")]
        [Authorize(Roles = "Admin,BarRep")]
        [ProducesResponseType(typeof(Nullable), StatusCodes.Status200OK)]
        [SwaggerResponse(StatusCodes.Status401Unauthorized)]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteCoupon(string couponId, [FromRoute] string barName)
        {
            try
            {
                _unitOfWork.CouponRepository.Delete(new object[]{barName,couponId});
                _unitOfWork.Complete();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}