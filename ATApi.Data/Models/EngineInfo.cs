using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ATApi.Data.Models
{
    public class EngineInfo
    {
        public int EngineInfoID { get; set; }
        public string Engine { get; set; }
        public string DriveTrain { get; set; }
        public string Power { get; set; }
        public string MilesPerTank { get; set; }
        public string FuelCapacity { get; set; }
        public string EngineSizeCC { get; set; }
        public int Cylinders { get; set; }
        public int Valves { get; set; }
        public string Gearbox { get; set; }
        public string FuelType { get; set; }
        public string Transmition { get; set; }
        public string FuelEconomy { get; set; }
        public string TopSpeed { get; set; }
        public string ZeroToSixty { get; set; }
    }
}
