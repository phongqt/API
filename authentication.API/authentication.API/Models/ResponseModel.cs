using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace authentication.API.Models
{
    public class ResponseModel
    {
        public HttpStatusCode code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}