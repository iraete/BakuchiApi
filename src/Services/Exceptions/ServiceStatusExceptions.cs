using System;

namespace BakuchiApi.StatusExceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() {}
    }

    public class ConflictException : Exception
    {
        public ConflictException() {}
    }
}