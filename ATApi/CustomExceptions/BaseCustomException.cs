using System;

namespace ATApi.CustomExceptions
{
    public class BaseCustomException : Exception
    {
        private int _code;
        private string _description;
        private string _path;

        public int Code
        {
            get => _code;
        }
        public string Description
        {
            get => _description;
        }

        public string Path
        {
            get => _path;
        }

        public BaseCustomException(string message, string description, string path, int code) : base(message)
        {
            _code = code;
            _description = description;
            _path = path;
        }
    }
}
