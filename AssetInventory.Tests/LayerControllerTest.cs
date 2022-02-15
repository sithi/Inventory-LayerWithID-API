using AssetInventory.Controllers;
using MediatR;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using PremiseGlobalLibery;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using Model;
using System.Collections.Generic;

namespace AssetInventory.Tests
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayerControllerTest 
    {
       

        [Fact]
        public async Task TestGetLayerByFloors()    
        {
           
            var requestStr = File.ReadAllText("./SampleRequests/LayerController-Get.json");
            string[] requestIds = (string[])JsonConvert.DeserializeObject(requestStr);
            // Act
           // var okResult = await _layerController.GetLayerByFloors(requestIds);

            // Assert
           // var items = Assert.IsType<List<LayerModel>>(okResult);
           // Assert.Equal(3, items.Count);
        }
    }
}
