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
    public class GetLayerByBuildingHandler : IRequestHandler<GetLayerByFloor, IEnumerable<LayerModel>>
    {
        private LayerDal _layerDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetLayerByBuildingHandler(LayerDal layerDal,IPremiseService premiseService,IOptions<DBCollections> dbCollections)
        {
            _layerDal = layerDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;
        }
        public async Task<IEnumerable<LayerModel>> Handle(GetLayerByFloor request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.InventoryLayer);
                return await _layerDal.GetAsync(collectionName, request.FloorIds);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
