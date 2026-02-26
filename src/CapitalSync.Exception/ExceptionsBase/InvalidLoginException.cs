using System.Net;

namespace CapitalSync.Exception.ExceptionsBase;

public class InvalidLoginException : CapitalSyncException
{
    public InvalidLoginException() : base(ResourceErrorMessages.INVALID_CREDENTIALS) {}
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public override List<string> GetErrors()
    {
        return [Message];
    }
}