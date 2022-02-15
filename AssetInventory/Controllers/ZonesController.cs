using AssetInventory.Extensions;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
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
    public class ZonesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ZonesController> _logger;
        private readonly IPremiseService _premiseService;

        public ZonesController(IPremiseService premiseService, IMediator mediator, ILogger<ZonesController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        [Route("{buildingid}/sensors/{sensortype}")]
        public async Task<IActionResult> GetAsync(string buildingid,string sensortype)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string appKey = identity.FindFirst("AppKey").Value;
                var returnData = new ApiResponseSuccess<IEnumerable<ZoneModel>>();
                string cachKey = appKey + "_" + buildingid + "_"+sensortype+"_zone";
                var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<ZoneModel>>(cachKey);

                if (cachData is null || cachData.Count()==0)
                {
                    var result = await _mediator.Send(new GetZoneByBuildingAndSensorType { AppKey = appKey, BuildingID = buildingid, SensorType = sensortype });
                    await _premiseService.StroeRecordInCacheAsync<IEnumerable<ZoneModel>>(cachKey, result, TimeSpan.FromMinutes(5));
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

        [Authorize]
        [HttpGet]
        [Route("{buildingid}")]
        public async Task<IActionResult> GetAsync(string buildingid)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string appKey = identity.FindFirst("AppKey").Value;
                var returnData = new ApiResponseSuccess<IEnumerable<ZoneModel>>();
                string cachKey = appKey + "_" + buildingid + "_" + "_zone";
                var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<ZoneModel>>(cachKey);

                if (cachData is null || cachData.Count() == 0 )
                {
                    var result = await _mediator.Send(new GetZoneByBuilding { AppKey = appKey, BuildingID = buildingid });
                    await _premiseService.StroeRecordInCacheAsync<IEnumerable<ZoneModel>>(cachKey, result, TimeSpan.FromMinutes(5));
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
