﻿using DatabaseLayer.Queries;
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
    public class LayerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LayerController> _logger;
        private IDistributedCache _cache;
        private IPremiseService _premiseService;

        public LayerController(IPremiseService premiseService, IDistributedCache cache, IMediator mediator, ILogger<LayerController> logger)
        {
            _premiseService = premiseService;
            _logger = logger;
            _mediator = mediator;
            _cache = cache;

        }

        [Authorize]
        [HttpPost]
        [Route("inventorylayer/{floorIds}")]
        public async Task<IActionResult> GetLayerByFloors(string[] floorIds)
        {
            try
            {
                
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                string appKey = identity.FindFirst("AppKey").Value;
                var returnData = new ApiResponseSuccess<IEnumerable<LayerModel>>();
                string cachKey = appKey + "_layers";
                var cachData = await _premiseService.GetRecordFromCacheAsync<IEnumerable<LayerModel>>(cachKey);
                if (cachData == null || cachData.Count() > 0)
                {
                    var result = await _mediator.Send(new GetLayerByFloor { AppKey = appKey, FloorIds = floorIds });
                    await _premiseService.StroeRecordInCacheAsync<IEnumerable<LayerModel>>(cachKey, result, TimeSpan.FromMinutes(5));
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
