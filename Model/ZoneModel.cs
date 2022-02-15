using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ZoneModel
    {
       
        
        public string Did { get; set; }
            public string Active { get; set; }
            public string LayerID { get; set; }
            public string FloorID { get; set; }
            public string BuildingID { get; set; }
            public string PropertyID { get; set; }
            public string Name { get; set; }
            public string SVG { get; set; }
            public string Tag { get; set; }
            public double X1 { get; set; }
            public double Y1 { get; set; }
            public double X2 { get; set; }
            public double Y2 { get; set; }
            public string BasicAttributes { get; set; }
            public string Matrix { get; set; }
            public string API { get; set; }
            public string AggregateZones { get; set; }
            public string APIProcessor { get; set; }
            public string APIType { get; set; }
            public string APIEventName { get; set; }
            public string IsObstacle { get; set; }
            public string AllRelations { get; set; }
            public string Settings { get; set; }
            public string Threshold { get; set; }
            public string SerialNumber { get; set; }
            public string SensorType { get; set; }
            public string ZoneType { get; set; }
            public string DisplayName { get; set; }
            public string SuiteId { get; set; }
            public string SpaceType { get; set; }
            public string Gateway { get; set; }
            public string Layer { get; set; }
            public string ImmediateAPI { get; set; }
            public string Extra1 { get; set; }

        }
    
}
