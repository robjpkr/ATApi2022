using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ATApi.Repo.ConnectionFactory
{
    public interface IConnFactory
    {
        IDbConnection Connection();
    }
    public class ConnFactory : IConnFactory
    {
        public ConnFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        readonly IConfiguration _configuration;

        public IDbConnection Connection()
        {
            return new SqlConnection(_configuration.GetConnectionString("VehicleDB"));
        }
    }
}
