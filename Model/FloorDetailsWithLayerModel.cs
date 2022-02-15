using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Id
    {
        public string Did { get; set; }
    }

    public class LayerList
    {
        public string Did { get; set; }
        public string Name { get; set; }
        public string BaseBuilding { get; set; }
        public string OfficeManagement { get; set; }
    }
   public  class FloorDetailsWithLayerModel
    {
        public Id _id { get; set; }
        public string Did { get; set; }
        public string Floor { get; set; }
        public string LayerOrder { get; set; }
        public List<LayerList> LayerList { get; set; }
    }
}
