using DatabaseLayer.DBContext;
using Microsoft.Extensions.Options;
using Model;
using Model.DBModel.MongoModel;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public class BuildingDal
    {
        private readonly IDBContext _dbContext;
        public BuildingDal(IDBContext dbContext)
        {
            _dbContext = dbContext;
            if (!BsonClassMap.IsClassMapRegistered(typeof(BuildingModel)))
            {
                BsonClassMap.RegisterClassMap<BuildingModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }

        public async Task<BuildingModel> GetAsync(string collectionName,string did)
        {
            try
            {
                var _buildingCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<BuildingModel>(collectionName);
                var result = await _buildingCollection.FindAsync(x => x.Did == did);
                return await result.FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }

    }
}
