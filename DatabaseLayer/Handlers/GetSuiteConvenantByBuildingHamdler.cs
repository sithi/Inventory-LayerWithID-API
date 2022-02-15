using DatabaseLayer.Dal;
using DatabaseLayer.Queries;
using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseLayer.Handlers
{
     class GetSuiteConvenantByBuildingHamdler : IRequestHandler<GetSuiteConvenantByBuilding, IEnumerable<SuiteCovenantModel>>
    {
        private SuiteConvenantDal _suiteConvenantDal;

        public GetSuiteConvenantByBuildingHamdler(SuiteConvenantDal suiteConvenantDal)
        {
            _suiteConvenantDal = suiteConvenantDal;
        }
        public async Task<IEnumerable<SuiteCovenantModel>> Handle(GetSuiteConvenantByBuilding request, CancellationToken cancellationToken)
        {
            try
            {
                return await _suiteConvenantDal.GetAsync(request.BuildingID);

            }
            catch
            {
                throw;
            }

        }
    }
}
