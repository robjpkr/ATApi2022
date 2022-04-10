using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATApi.Data.Models
{
    public class StandardEquipment
    {
        public int StandardEquipmentID { get; set; }
        public bool ThreeXThreePointRearSeatBelts { get; set; }
        public bool ABS { get; set; }
        public bool Alarm { get; set; }
        public bool AlloyWheels { get; set; }
        public bool BodyColouredBumpers { get; set; }
        public bool CruiseControl { get; set; }
        public bool DriversAirbags { get; set; }
        public bool ElectricDriversSeat { get; set; }
        public bool ElectricMirrors { get; set; }
        public bool ElectricPassengerSeat { get; set; }
        public bool ElectricRoof { get; set; }
        public bool FoldingRearSeats { get; set; }
        public bool FrontElectricWindows { get; set; }
        public bool HeightAdjustableDriversSeat { get; set; }
        public bool IsofixChildSeatAnchorPoints { get; set; }
        public bool LeatherSeatTrim { get; set; }
        public bool ParkingSensors { get; set; }
        public bool PAS { get; set; }
        public bool PassengersAirbags { get; set; }
        public bool Remote { get; set; }
        public bool RearElectricWindows { get; set; }
        public bool RemoteLocking { get; set; }
        public bool SatNav { get; set; }
        public bool SideAirbags { get; set; }
        public bool StreeingWheelRakeAdjustment { get; set; }
        public bool SteeringWheelReachAdjustment { get; set; }
    }
}
