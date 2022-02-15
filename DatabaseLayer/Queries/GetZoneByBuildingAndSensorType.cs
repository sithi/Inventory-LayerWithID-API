using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetZoneByBuildingAndSensorType : IRequest<IEnumerable<ZoneModel>>
    {
        public string AppKey { get; set; }
        public string BuildingID { get; set; }
        public string SensorType { get; set; }
        
    }
}
