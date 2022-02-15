using Microsoft.Extensions.Options;
using Model;
using Model.DBModel.MongoModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public class SuiteConvenantDal
    {
        private readonly IMongoCollection<SuiteCovenantModel> _suiteCovenantCollection;
        private readonly string _collectionName = "QuadReal_Inventory_Suite_Covenant";
        public SuiteConvenantDal(IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(
                mongoDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDBSettings.Value.DatabaseName);

            _suiteCovenantCollection = mongoDatabase.GetCollection<SuiteCovenantModel>(
               _collectionName);
        }

        public async Task<IEnumerable<SuiteCovenantModel>> GetAsync(string buildingId)
        {
            var result = await _suiteCovenantCollection.FindAsync(x => x.BuildingId == buildingId);
            return await result.ToListAsync();
        }
    }
}
