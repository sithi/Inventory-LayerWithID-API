using DatabaseLayer.DBContext;
using Microsoft.Extensions.Options;
using Model;
using Model.DBModel.MongoModel;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using PremiseGlobalLibery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public class SuiteDal
    {
        private readonly IDBContext _dbContext;

        public SuiteDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(SuiteModel)))
            {
                BsonClassMap.RegisterClassMap<SuiteModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }         
        }
        public async Task<List<SuiteModel>> GetAsync(string collectionName,string buildingID)
        {
            try
            {
                var _suiteCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<SuiteModel>(collectionName);
                var result = await _suiteCollection.FindAsync(x => x.BuildingId == buildingID);
                return await result.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
