using ATApi.CustomExceptions;
using System.Net;

namespace AT.CustomExceptions
{
    public class NotFoundException : BaseCustomException
    {
        public NotFoundException(string message, string description, string path) : base(message, description, path, (int)HttpStatusCode.NotFound)
        {
        }
    }
}
