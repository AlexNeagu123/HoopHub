namespace HoopHub.API.Utility
{
    [AttributeUsage(AttributeTargets.Method)]
    public class FileResultContentTypeAttribute(string contentType) : Attribute
    {
        public string ContentType { get; } = contentType;
    }
}
