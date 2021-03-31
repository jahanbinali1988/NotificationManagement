using System;
using System.Net;

namespace Common.AspNetCore.Model
{
    public class ApiResponse
    {
        public object Data { get; set; }
        public MetaApiResponse Meta { get; set; }
    }
    public class ApiResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
