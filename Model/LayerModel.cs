using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LayerModel
    {
        public string Active { get; set; }
        public string FloorID { get; set; }
        public string BuildingID { get; set; }
        public string PropertyID { get; set; }
        public string Name { get; set; }
        public string ParentLayerDid { get; set; }
        public string SubLayerOrder { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string TypeDetails { get; set; }
        public string SocketOrAPI { get; set; }
        public string LayerSettings { get; set; }
        public string BaseBuilding { get; set; }
        public string OfficeManagement { get; set; }
        public bool IsSystemLayer { get; set; }
        public string Image { get; set; }
        
    }
}
