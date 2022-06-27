using ATApi.Repo.ConnectionFactory;
using System.Data;
using Dapper;
using ATApi.Data.Models;
using ATApi.Service.Data;

namespace ATApi.Repo.Repositories
{
    public interface IErrorRepository
    {
        void Log(Exception exception, string friendlyMessage, string methodName, string className, Priority priority);
    }

    public class ErrorRepository : IErrorRepository 
    {
        private readonly IConnFactory _conn;        
        public ErrorRepository(IConnFactory conn)
        {
            _conn = conn;            
        }

        public async void Log(Exception exception, string friendlyMessage, string methodName, string className, Priority priority)
        {
            var sql = $@"insert into errorlogging (exception, friendlymessage, methodname, classname, priority) 
                         values ('{exception}', '{friendlyMessage}', '{methodName}', '{className}', '{priority}');";

            using (IDbConnection conn = _conn.Connection())
            {
                conn.Open();
                await conn.ExecuteAsync(sql, new Error
                {
                    
                });
            }
        }
    }
}
