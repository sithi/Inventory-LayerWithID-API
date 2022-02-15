using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FloorObj
    {
        public string label { get; set; }
        public string value { get; set; }
    }   
    public class SpecificFloorList
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class SubLayer
    {
        public string ServiceLayerName { get; set; }
        public List<string> SubLayerList { get; set; }
    }
    public class SettingsInfo
    {
        public string initiallyLoad { get; set; }
        public FloorObj floorObj { get; set; }
        public bool isHideStackView { get; set; }
        public bool isShowCORE { get; set; }
        public string labelAttr { get; set; }
        public string labelHoverAttr { get; set; }
        public bool isShowEODReport { get; set; }
        public bool isShowSecondaryLayer { get; set; }
        public List<object> layerListOnFloorPlan { get; set; }
        public List<SpecificFloorList> specificFloorList { get; set; }
        public string Name { get; set; }
        public string StreetName { get; set; }
        public string Address { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public string img { get; set; }
        public List<SubLayer> SubLayers { get; set; }
        public List<string> ServiceLayers { get; set; }
        public bool isActive { get; set; }
        public int peopleOnFloorMax { get; set; }
        public string initialRouteFromMap { get; set; }
        public string buildingTimeZone { get; set; }
        public bool showAdvancedAnalytics { get; set; }
        public List<object> DefaultSelectedLayer { get; set; }
        public int buildingOccupancyMax { get; set; }
        public string building_tz_Name { get; set; }
        public string atlasen_embed_key { get; set; }
        public int towerOccupancyLimit { get; set; }
        public int officeOccupancyLimit { get; set; }
        public bool display_social_distance_alert { get; set; }
        public string MQTTTopic { get; set; }
        public string displayTenantInfoOnStackingView { get; set; }
        public int deskOccupancyLimit { get; set; }
    }
    public class GroundFloorBuildings
    {
        public string BuildingId { get; set; }
        public SettingsInfo settingsInfo { get; set; }
    }
}
