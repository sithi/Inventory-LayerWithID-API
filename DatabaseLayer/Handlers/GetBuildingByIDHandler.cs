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
    class GetBuildingByIDHandler : IRequestHandler<GetBuildingByID, BuildingModel>
    {
        private BuildingDal _buildingDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetBuildingByIDHandler(BuildingDal buildingDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _buildingDal = buildingDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;

        }
        public async Task<BuildingModel> Handle(GetBuildingByID request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Building);
                return await _buildingDal.GetAsync(collectionName, request.BuildingID);

            }
            catch
            {
                throw;
            }

        }
    }
}



