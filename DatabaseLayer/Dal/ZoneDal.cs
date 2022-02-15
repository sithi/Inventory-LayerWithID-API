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
    public class ZoneDal
    {
        private readonly IDBContext _dbContext;
        public ZoneDal(IDBContext dbContext)
        {
            _dbContext = dbContext;

            if (!BsonClassMap.IsClassMapRegistered(typeof(ZoneModel)))
            {
                BsonClassMap.RegisterClassMap<ZoneModel>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            }
        }

        public async Task<List<ZoneModel>> GetAsync(string collectionName, string buildingID)
        {
            try
            {
                var _zoneCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<ZoneModel>(collectionName);
                var result = await _zoneCollection.FindAsync(x => x.BuildingID == buildingID);
                return await result.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<List<ZoneModel>> GetAsync(string collectionName,string buildingID,string sensorType)
        {
            try
            {
                var _zoneCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<ZoneModel>(collectionName);
                var result = await _zoneCollection.FindAsync(x => x.BuildingID == buildingID && x.SensorType == sensorType);
                return await result.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        
    }
}
