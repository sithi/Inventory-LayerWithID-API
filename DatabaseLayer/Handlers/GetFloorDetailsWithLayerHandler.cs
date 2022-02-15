using DatabaseLayer.Dal;
using DatabaseLayer.DBContext;
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
    class GetFloorDetailsWithLayerHandler : IRequestHandler<GetFloorDetailsWithLayer, IEnumerable<Dictionary<string, object>>>
    {
        private FloorDal _floorDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetFloorDetailsWithLayerHandler(FloorDal floorDal,IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _floorDal = floorDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> Handle(GetFloorDetailsWithLayer request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Floor);
                return await _floorDal.GetFlorDetailsWithLayerListAsync(collectionName,request.BuildingID);
                
            }
            catch
            {
                throw;
            }
          
        }
    }
}
