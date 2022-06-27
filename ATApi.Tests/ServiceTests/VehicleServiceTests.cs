using ATApi.Data.Models;
using ATApi.Repo.Repositories;
using ATApi.Service.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ATApi.ServiceTests
{
    public class VehicleServiceTests
    {
        private List<Vehicle> GetTestVehicleData()
        {
            return new List<Vehicle>()
            {
                new Vehicle
                {
                    BodyType = "BodyType",
                    Images = new List<Image>()
                    {
                        new Image
                        {
                             ImageName = "Image",
                        }
                    }
                }
            };
        }

        [Fact]
        public void TestVehicleServiceGetAll()
        {
            var vehicleRepository = new Mock<IVehicleRepository>();
            vehicleRepository.Setup(x => x.GetAll()).ReturnsAsync(GetTestVehicleData());           
            var service = new VehicleService(vehicleRepository.Object);
            var result = service.GetAll().Result;

            Assert.True(result.ToList().Count() == 1);
            Assert.True(result.ToList()[0].BodyType == "BodyType");
            Assert.True(result.ToList()[0].Images[0].ImageName == "Image");
        }
    }
}