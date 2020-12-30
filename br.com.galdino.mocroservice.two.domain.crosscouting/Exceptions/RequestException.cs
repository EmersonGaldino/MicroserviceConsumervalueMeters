using System;

namespace br.com.galdino.mocroservice.two.domain.crosscouting.Exceptions
{
    public class RequestException : Exception
    {
        public RequestException(int statusCode, string message)
        {
            StatusCode = statusCode;
        }
        public int StatusCode { get; }

    }
}
