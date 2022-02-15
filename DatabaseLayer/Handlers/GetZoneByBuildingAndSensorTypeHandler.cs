using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Options;
using Model;
using PremiseGlobalLibery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseLayer.Handlers
{
    class GetZoneByBuildingAndSensorTypeHandler : IRequestHandler<GetZoneByBuildingAndSensorType, IEnumerable<ZoneModel>>
    {
        private ZoneDal _zoneDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetZoneByBuildingAndSensorTypeHandler(ZoneDal zoneDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _zoneDal = zoneDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;
        }
        public async Task<IEnumerable<ZoneModel>> Handle(GetZoneByBuildingAndSensorType request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Zone);
                return await _zoneDal.GetAsync(collectionName, request.BuildingID,request.SensorType);
            }
            catch
            {
                throw;
            }

        }
    }
}
