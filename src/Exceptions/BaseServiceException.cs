using System;
using System.Net;

namespace BakuchiApi.StatusExceptions
{
    public abstract class BaseServiceException : Exception
    {
        public BaseServiceException()
        {
        }

        public BaseServiceException(string message) : base(message)
        {
        }

        public HttpStatusCode Status { get; set; } =
            HttpStatusCode.InternalServerError;
    }
}