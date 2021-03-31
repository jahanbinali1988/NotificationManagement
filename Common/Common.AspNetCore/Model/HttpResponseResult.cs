using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common.AspNetCore.Model
{
    public class HttpResponseResult<T>
    {
        public T Body { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
    }

    public class HttpResponseResult
    {
        public string Body { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ContentType { get; set; }
    }
}
