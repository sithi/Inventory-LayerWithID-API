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
    class GetFloorByBuildingHandler : IRequestHandler<GetFloorByBuilding, IEnumerable<FloorModel>>
    {
        private FloorDal _floorDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetFloorByBuildingHandler(FloorDal floorDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _floorDal = floorDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;

        }
        public async Task<IEnumerable<FloorModel>> Handle(GetFloorByBuilding request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Floor);
                return await _floorDal.GetAsync(collectionName,request.BuildingID);

            }
            catch
            {
                throw;
            }

        }
    }
}


