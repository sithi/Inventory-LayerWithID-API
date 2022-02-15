using MediatR;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLayer.Queries
{
    public class GetFloorByBuilding : IRequest<IEnumerable<FloorModel>>
    {
        public string AppKey { get; set; }
        public string BuildingID { get; set; }

    }
}
