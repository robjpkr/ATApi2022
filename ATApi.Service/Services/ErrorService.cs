using ATApi.Repo.Repositories;
using ATApi.Service.Data;

namespace ATApi.Service.Services
{
    public interface IErrorService
    {
        void Log(Exception exception, string friendlyMessage, string methodName, string className, Priority priority);
    }

    public class ErrorService : IErrorService
    {
        private readonly IErrorRepository _errorRepository;

        public ErrorService(IErrorRepository errorRepository)
        {
            _errorRepository = errorRepository;
        }

        public void Log(Exception exception, string friendlyMessage, string methodName, string className, Priority priority)
        {
            _errorRepository.Log(exception = null, friendlyMessage, methodName, className, priority);

        }
    }

}
