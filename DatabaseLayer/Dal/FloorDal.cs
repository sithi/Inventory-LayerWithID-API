using DatabaseLayer.DBContext;
using Microsoft.Extensions.Options;
using Model;
using Model.DBModel.MongoModel;
using MongoDB.Bson;
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
    public class FloorDal
    {
        private readonly IDBContext _dbContext;
        public FloorDal(IDBContext dbContext)
        {
            _dbContext = dbContext;

            if (!BsonClassMap.IsClassMapRegistered(typeof(FloorModel)))
            {
                BsonClassMap.RegisterClassMap<FloorModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }           
        }
        public async Task<List<FloorModel>> GetAsync(string collectionName,string buildingID)
        {
            try
            {
                var _floorCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<FloorModel>(collectionName);
                var result = await _floorCollection.FindAsync(x => x.BuildingId == buildingID);
                return await result.ToListAsync();
            }
            catch
            {
                throw;
            }
        }
        public async Task<IEnumerable<Dictionary<string,object>>> GetFlorDetailsWithLayerListAsync(string collectionName,string buildingID)
        {
            try
            {
                var _floorCollection = _dbContext.GetDataBase<IMongoDatabase>().GetCollection<FloorModel>(collectionName);

                var pipeline = new BsonDocument[] {
                new BsonDocument{ { "$match", new BsonDocument("BuildingId", buildingID) }},
                new BsonDocument{{ "$lookup", new BsonDocument("from", "QuadReal_Inventory_Layer")
                                   .Add("localField","Did").Add("foreignField","FloorID").Add("as","Layer") }},
                new BsonDocument{{"$unwind", new BsonDocument("path", "$Layer").Add("preserveNullAndEmptyArrays", true) }},
                new BsonDocument{ { "$project", new BsonDocument {
                    {"Did","$Did"},{"Floor","$Floor" },{"LayerOrder","$LayerOrder" },{"LayerDid","$Layer.Did" },{"LayerName","$Layer.Name" },{"BaseBuilding","$Layer.BaseBuilding" },{"OfficeManagement","$Layer.OfficeManagement" }
                    }}},
                new BsonDocument{{"$group", new BsonDocument{
                    {"_id", new BsonDocument( "Did","$Did") },
                    {"Did", new BsonDocument( "$first","$Did") },
                    {"Floor", new BsonDocument( "$first","$Floor") },
                    {"LayerOrder", new BsonDocument( "$first","$LayerOrder") },
                    { "LayerList",new BsonDocument("$push",new BsonDocument
                    {
                        {"Did","$LayerDid" },{"Name","$LayerName" },{"BaseBuilding","$BaseBuilding" },{"OfficeManagement","$OfficeManagement"}
                    })}
                }}}
            };
                var m = await _floorCollection.AggregateAsync<Dictionary<string, object>>(pipeline);
                var result = await m.ToListAsync();
                return result;
            }
            catch
            {
                throw;
            }
         
        }
    }
}
