using ATApi.Data.Models;
using ATApi.Repo.ConnectionFactory;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ATApi.Repo.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<Vehicle>> GetAll();
        Task<Vehicle> GetById(int id);
        Task<IEnumerable<Image>> GetImagesAll();
        Task<Dealer> GetDealerById(int id);
        Task<IEnumerable<Image>> GetImagesById(int id);
        Task<int> GetCountOfVehicles();
    }

    public class VehicleRepository : IVehicleRepository
    {
        private readonly IConnFactory _conn;
        private readonly IConfiguration _config;
        private readonly string _connectionstring;
        public VehicleRepository(IConnFactory conn, IConfiguration config)
        {
            _conn = conn;
            _config = config;
            _connectionstring = _config.GetConnectionString("VehicleDb");
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            var sql = @"select * from vehicles v
                    inner join engineinfo e on e.vehicleid = v.vehicleid
                    inner join standardequipment s on s.vehicleid = v.vehicleid
                    inner join dealers d on d.dealerid = v.dealerid";

            using (IDbConnection conn = _conn.Connection())
            {
                conn.Open();
                var vehicle = await conn.QueryAsync<Vehicle, EngineInfo, StandardEquipment, Dealer, Vehicle>(
                    sql,
                    (vehicle, engineInfo, standardEquipment, dealer) =>
                    {
                        vehicle.Dealer = dealer;
                        vehicle.EngineInfo = engineInfo;
                        vehicle.StandardEquipment = standardEquipment;
                        return vehicle;
                    }, splitOn: "EngineInfoId, StandardEquipmentId, DealerId");
                return vehicle;
            }
        }

        public async Task<IEnumerable<Image>> GetImagesAll()
        {
            using (IDbConnection conn = _conn.Connection())
            {
                conn.Open();
                return await conn.QueryAsync<Image>(@"select * from images");
            }
        }

        public async Task<IEnumerable<Image>> GetImagesById(int id)
        {
            using (IDbConnection conn = _conn.Connection())
            {
                conn.Open();
                return await conn.QueryAsync<Image>($"select * from images where vehicleid = {id}");
            }
        }

        public async Task<Vehicle> GetById(int id)
        {
            var lookup = new Dictionary<int, Vehicle>();

            using (IDbConnection conn = _conn.Connection())
            {
                var sql = @$"select * from vehicles v
                              inner join engineinfo e on e.vehicleid = v.vehicleid
                              inner join standardequipment s on s.vehicleid = v.vehicleid
                              inner join dealers d on d.dealerid = v.dealerid 
                              inner join images i on i.vehicleid = v.vehicleid
                              where v.vehicleid = {id}";

                conn.Open();
                var vehicle = await conn.QueryAsync<Vehicle, EngineInfo, StandardEquipment, Dealer, Image, Vehicle>(
                    sql,
                    (v, e, s, d, i) =>
                    {
                        Vehicle vehicle;
                        if (!lookup.TryGetValue(v.VehicleID, out vehicle))
                        {
                            lookup.Add(v.VehicleID, vehicle = v);
                        }
                        if (vehicle.Images == null)
                        {
                            vehicle.Images = new List<Image>();
                        }
                        vehicle.EngineInfo = e;
                        vehicle.StandardEquipment = s;
                        vehicle.Dealer = d;
                        vehicle.Images.Add(i);
                        return v;
                    }, splitOn: "EngineInfoId, StandardEquipmentId, DealerId, ImageId");

                return vehicle.First();
            }
        }

        public async Task<Dealer> GetDealerById(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                var dealer = new Dealer() { };
                var cmd = new SqlCommand("GetDealerById", conn);

                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = await cmd.ExecuteReaderAsync();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dealer.DealerID = dr.GetInt32(0);
                        dealer.Name = dr.GetString(1);
                        dealer.AddressLine1 = dr.GetString(2);
                        dealer.AddressLine2 = dr.GetString(3);
                        dealer.City = dr.GetString(4);
                        dealer.PostCode = dr.GetString(5);
                    }
                    dr.Close();
                }
                conn.Close();
                return dealer;
            }
        }

        public async Task<int> GetCountOfVehicles()
        {
            using (IDbConnection conn = _conn.Connection())
            {
                conn.Open();
                return await conn.QuerySingleAsync<int>("select count(*) from vehicles");
            }
        }
    }
}

