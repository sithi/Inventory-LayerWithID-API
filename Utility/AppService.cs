//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Options;
//using MongoDB.Bson.Serialization;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Utility
//{
//    public class AppModel
//    {
//        public string AppId { get; set; }
//        public string AppKey { get; set; }
//    }
//	public class Configuration
//    {
//		public string ConnectionString { get; set; }
//        public string Database { get; set; }
//    }

	
//	public static class PremiseGlobalServicesConfiguration
//	{
		
//		public static void AddPremiseService(this IServiceCollection services, Action<Configuration> configure)
//		{
//			var defaultOptions = new Configuration();
//			configure.Invoke(defaultOptions);


//            services.AddTransient(p=>new AppDal(defaultOptions));
//		}
//	}

//	public class MyService
//    {

//    }
//    public class AppDal
//    {
//        private readonly IMongoCollection<AppModel> _appCollection;
//        private readonly string _collectionName = "App";
//        public AppDal(Configuration config)
//        {
//            var mongoClient = new MongoClient(
//                config.ConnectionString);

//            var mongoDatabase = mongoClient.GetDatabase(
//                config.Database);

//            BsonClassMap.RegisterClassMap<AppModel>(cm =>
//            {
//                cm.AutoMap();
//                cm.SetIgnoreExtraElements(true);
//            });

//            _appCollection = mongoDatabase.GetCollection<AppModel>(
//               _collectionName);
//        }

//        public async Task<List<AppModel>> GetAsync(string buildingID)
//        {
//            try
//            {
//                var result = await _appCollection.FindAsync(x => x. == buildingID);
//                return await result.ToListAsync();
//            }
//            catch
//            {
//                throw;
//            }
//        }
//        public async Task<List<ZoneModel>> GetAsync(string buildingID, string sensorType)
//        {
//            try
//            {
//                var result = await _zoneCollection.FindAsync(x => x.BuildingID == buildingID && x.SensorType == sensorType);
//                return await result.ToListAsync();
//            }
//            catch
//            {
//                throw;
//            }
//        }

//    }
//}
