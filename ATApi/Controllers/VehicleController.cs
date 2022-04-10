using AT.CustomExceptions;
using ATApi.Data.Models;
using ATApi.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ATApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService<Vehicle> _vehicleService;
        private readonly ILogger<VehicleController> _logger;
        private readonly IConfiguration _config;
        private readonly string _xapikey;

        public VehicleController(ILogger<VehicleController> logger, 
                                 IVehicleService<Vehicle> vehicleService, 
                                 IConfiguration config)
        {
            _vehicleService = vehicleService;
            _logger = logger;
            _config = config;
            _xapikey = _config.GetValue<string>("X-API-Key");
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll([FromHeader(Name = "X-API-Key")][Required] string xApiKey)
        {
            if (!xApiKey.Equals(_xapikey))
            {
                return Unauthorized();
            }
            else
            {
                var vehicles = await _vehicleService.GetAll();

                if (!vehicles.Any())
                {
                    var exception = new NotFoundException("Not Found", 
                                                          "Records could not be retrieved from server", 
                                                          "VehicleController-GetAll");
                    _logger.LogError($"{exception.Path},{exception.Description},{exception.Message}");
                    return NotFound(exception);
                }

                return Ok(vehicles);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById([FromHeader(Name = "X-API-Key")][Required] string xApiKey, int id)
        {
            if (!xApiKey.Equals(_xapikey))
            {
                return Unauthorized();
            }
            else
            {
                var vehicle = await _vehicleService.GetById(id);

                if (vehicle.VehicleID != id)
                {
                    var exception = new NotFoundException("Not Found", 
                                                          "Records could not be retrieved from server", 
                                                          "VehicleController-GetById");
                    _logger.LogError($"{exception.Path},{exception.Description},{exception.Message}");
                    return NotFound(exception);
                }

                return Ok(vehicle);
            }
        }

        [HttpGet]
        [Route("GetMainPageNewVehicles")]
        public async Task<IActionResult> GetMainPageNewVehicles([FromHeader(Name = "X-API-Key")][Required] string xApiKey)
        {
            if (!xApiKey.Equals(_xapikey))
            {
                return Unauthorized();
            }
            else
            {
                var vehicle =  await _vehicleService.GetMainPageNewVehicles();

                if (vehicle.Count() != Convert.ToInt32(_config.GetValue<string>("MainPageNewVehicleCount")))
                {
                    var exception = new NotFoundException("Not Found", 
                                                          "Records could not be retrieved from server", 
                                                          "VehicleController-BrandNewCarsSection");
                    _logger.LogError($"{exception.Path},{exception.Description},{exception.Message}");
                    return NotFound(exception);
                }

                return Ok(vehicle);
            }
        }
    }
}
