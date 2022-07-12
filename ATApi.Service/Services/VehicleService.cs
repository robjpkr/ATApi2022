using ATApi.Data.Models;
using ATApi.Repo.Repositories;

namespace ATApi.Service.Services
{
    public interface IVehicleService<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(int id);
        Task<List<Vehicle>> GetMainPageNewVehicles();
        Task<int> GetCountOfVehicles();
    }
    public class VehicleService : IVehicleService<Vehicle>
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            var vehicles = new List<Vehicle>();
            var images = new List<Image>();

            var tasks = new List<Task>();
            tasks.Add(Task.Run(() =>
            {
                vehicles = _vehicleRepository.GetAll().Result.ToList();
                images = _vehicleRepository.GetImagesAll().Result.ToList();
            }));

            await Task.WhenAll(tasks);

            return vehicles;
        }

        public async Task<int> GetCountOfVehicles()
        {
            return await _vehicleRepository.GetCountOfVehicles();
        }

        public async Task<Vehicle> GetById(int id)
        {
            return await _vehicleRepository.GetById(id);
        }

        public async Task<List<Vehicle>> GetMainPageNewVehicles()
        {
            var numbers = GenerateFourRandomNumbers();
            var vehicles = new List<Vehicle>();
            var tasks = new List<Task>();

            numbers.Result.ForEach(a => 
                            tasks.Add(Task.Run(async () => 
                            vehicles.Add(await _vehicleRepository.GetById(a)))));


           await Task.WhenAll(tasks);

            if (vehicles.Count != 4)
            {
                await GetCountOfVehicles();  //This is pony! Why is the repo call inconsistent??
            }

            return vehicles;

        }

        private async Task<List<int>> GenerateFourRandomNumbers()
        {
            var numbers = new List<int>();
            var random = new Random();
            int number;
            var vehicleCount = await _vehicleRepository.GetCountOfVehicles();

            do
            {
                number = random.Next(1, vehicleCount);
                if (numbers.Contains(number))
                {
                    continue;
                }
                else
                {
                    numbers.Add(number);
                }
            } while (numbers.Count != 4);

            return numbers;
        }
    }
}
