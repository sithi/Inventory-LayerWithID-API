using AssetInventory.Extensions;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Model;
using PremiseGlobalLibery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AssetInventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FloorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<FloorsController> _logger;
        private IPremiseService _premiseService;

        public FloorsController(IPremiseService premiseService, IMediator mediator, ILogger<FloorsController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        [Route("{buildingid}")]
        public async Task<IActionResult> GetAsync(string buildingid)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string appKey = identity.FindFirst("AppKey").Value;
                var returnData = new ApiResponseSuccess<IEnumerable<FloorModel>>();
                string cachKey = appKey + "_" + buildingid + "_floor";
                var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<FloorModel>>(cachKey);

                if (cachData is null || cachData.Count() == 0 )
                {
                    var result = await _mediator.Send(new GetFloorByBuilding { AppKey = appKey, BuildingID = buildingid });
                    await _premiseService.StroeRecordInCacheAsync<IEnumerable<FloorModel>>(cachKey, result, TimeSpan.FromMinutes(5));
                    returnData.data = result;
                }
                else
                {
                    returnData.data = cachData;
                }
                returnData.status = "success";
                returnData.statusCode = 200;
                return StatusCode(200, returnData);
            }
            catch(Exception ex)
            {
                var errorData = new ApiResponseError<Dictionary<string, object>>();
                errorData.status = "error";
                errorData.message = ex.Message;
                errorData.statusCode = 500;
                return StatusCode(500, errorData);
            }
        }

        [Authorize]
        [Route("{buildingid}/floordetailswithlayer")]
        [HttpGet]
        public async Task<IActionResult> GetFloorDetailsWithLayerAsync(string buildingid)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string appKey = identity.FindFirst("AppKey").Value;

                var returnData = new ApiResponseSuccess<IEnumerable<Dictionary<string, object>>>();
                string cachKey = appKey + "_" + buildingid + "_floordetailswithlayer";
                var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey);

                if (cachData is null || cachData.Count()==0)
                {
                    var result = await _mediator.Send(new GetFloorDetailsWithLayer { AppKey = appKey, BuildingID = buildingid });
                    await _premiseService.StroeRecordInCacheAsync<IEnumerable<Dictionary<string, object>>>(cachKey, result, TimeSpan.FromMinutes(5));
                    returnData.data = result;
                }
                else
                {
                    returnData.data = cachData;
                }
                returnData.status = "success";
                returnData.statusCode = 200;
                return StatusCode(200, returnData);
            }
            catch (Exception ex)
            {
                var errorData = new ApiResponseError<Dictionary<string, object>>();
                errorData.status = "error";
                errorData.message = ex.Message;
                errorData.statusCode = 500;
                return StatusCode(500, errorData);
            }
        }
    }
}
