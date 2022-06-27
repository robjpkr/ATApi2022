namespace ATApi.Data.Models
{
    public class Error
    {
        public Exception Exception { get; set; }
        public string FriendlyName { get; set; }
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public Enum Priority { get; set; }

    }
}
