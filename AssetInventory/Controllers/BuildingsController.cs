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
    public class BuildingsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BuildingsController> _logger;
        private IDistributedCache _cache;
        private IPremiseService _premiseService;
        public BuildingsController(IPremiseService premiseService ,IDistributedCache cache, IMediator mediator, ILogger<BuildingsController> logger)
        {
            _cache = cache;
            _mediator = mediator;
            _logger = logger;
            _premiseService = premiseService;
        }

        [Authorize]
        [Route("{buildingid}")]
        public async Task<IActionResult> Get(string buildingid)
        {
            try
            {                
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string appKey = identity.FindFirst("AppKey").Value;
                var returnData = new ApiResponseSuccess<BuildingModel>();

                string cachKey = appKey + "_" + buildingid + "_building";
                var cachData = await _premiseService.GetRecordFromCacheAsync<BuildingModel>(cachKey);

                if (cachData is null)
                {
                    var result = await _mediator.Send(new GetBuildingByID { AppKey = appKey, BuildingID = buildingid });
                   await _premiseService.StroeRecordInCacheAsync<BuildingModel>(cachKey, result, TimeSpan.FromMinutes(5));
                    returnData.data = result;
                    returnData.status = "success";
                }
                else
                {
                    returnData.status = "success";
                    returnData.data = cachData;
                }
                
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
