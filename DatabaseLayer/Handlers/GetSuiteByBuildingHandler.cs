using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
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
    class GetSuiteByBuildingHandler : IRequestHandler<GetSuiteByBuilding, IEnumerable<SuiteModel>>
    {
        private SuiteDal _suiteDal;
        private IPremiseService _premiseService;
        private IOptions<DBCollections> _dbCollections;

        public GetSuiteByBuildingHandler(SuiteDal suiteDal, IPremiseService premiseService, IOptions<DBCollections> dbCollections)
        {
            _suiteDal = suiteDal;
            _premiseService = premiseService;
            _dbCollections = dbCollections;

        }
        public async Task<IEnumerable<SuiteModel>> Handle(GetSuiteByBuilding request, CancellationToken cancellationToken)
        {
            try
            {
                var appInfo = await _premiseService.GetAppNameAsync(request.AppKey);
                string collectionName = _premiseService.GetCollectionName(appInfo.AppName, _dbCollections.Value.Suite);
                return await _suiteDal.GetAsync(collectionName, request.BuildingID);

            }
            catch
            {
                throw;
            }

        }
    }
}
