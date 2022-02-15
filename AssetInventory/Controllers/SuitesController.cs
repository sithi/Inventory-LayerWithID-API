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
    public class SuitesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SuitesController> _logger;
        private IPremiseService _premiseService;
        public SuitesController(IPremiseService premiseService, IMediator mediator, ILogger<SuitesController> logger)
        {
            _premiseService = premiseService;
            _mediator = mediator;
            _logger = logger;
        }       

        [Authorize]
        [Route("{buildingid}")]
        [HttpGet]
        public async Task<IActionResult> Get(string buildingid)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string appKey = identity.FindFirst("AppKey").Value;
                var returnData = new ApiResponseSuccess<IEnumerable<SuiteModel>>();

                string cachKey = appKey + "_" + buildingid + "_suites";
                var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<SuiteModel>>(cachKey);

                if (cachData is null  || cachData.Count()==0)
                {                    
                    var result = await _mediator.Send(new GetSuiteByBuilding { AppKey = appKey, BuildingID = buildingid });
                    await _premiseService.StroeRecordInCacheAsync<IEnumerable<SuiteModel>>(cachKey, result, TimeSpan.FromMinutes(5));
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
