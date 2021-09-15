using System.Net;

namespace BakuchiApi.StatusExceptions
{
    public class NotFoundException : BaseServiceException
    {  
        public NotFoundException(string message = null): base(message)
        {
            Status = HttpStatusCode.NotFound;
        }
    }

    public class ConflictException : BaseServiceException
    {
        public ConflictException(string message = null): base(message) 
        {
            Status = HttpStatusCode.Conflict;
        }
    }

    public class BadRequestException : BaseServiceException
    {
        public BadRequestException(string message = null): base(message) 
        {
            Status = HttpStatusCode.BadRequest;
        }
    }

    public class ForbiddenException : BaseServiceException
    {
        public ForbiddenException(string message = null): base(message)
        {
            Status = HttpStatusCode.Forbidden;
        }
    }
}