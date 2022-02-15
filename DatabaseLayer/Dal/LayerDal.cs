using DatabaseLayer.DBContext;
using Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Dal
{
    public class LayerDal
    {
        private readonly IDBContext _dbContext;
        public LayerDal(IDBContext dBContext)
        {
            _dbContext=dBContext;

            if (!BsonClassMap.IsClassMapRegistered(typeof(LayerModel)))
            {
                BsonClassMap.RegisterClassMap<LayerModel>(m => {
                    m.AutoMap();
                    m.SetIgnoreExtraElements(true);
                });  
            }
        }

        public async Task<List<LayerModel>> GetAsync(string collectionName, string[] floorIds)
        {
            try
            {
                var _suiteCollection =  _dbContext.GetDataBase<IMongoDatabase>().GetCollection<LayerModel>(collectionName);
                var filter = Builders<LayerModel>.Filter.In(m =>m.FloorID,floorIds);
                var result =await _suiteCollection.FindAsync(filter);
                return  await result.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
