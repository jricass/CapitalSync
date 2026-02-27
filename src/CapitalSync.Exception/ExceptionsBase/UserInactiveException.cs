using System.Net;

namespace CapitalSync.Exception.ExceptionsBase;

public class UserInactiveException : CapitalSyncException
{
    public UserInactiveException() : base(ResourceErrorMessages.USER_INACTIVE) { }
    
    public override int StatusCode => (int)HttpStatusCode.Forbidden;
    
    public override List<string> GetErrors()
    {
        return [Message];
    }
}