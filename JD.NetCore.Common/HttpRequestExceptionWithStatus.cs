﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace JD.NetCore.Common
{
    public class HttpRequestExceptionWithStatus : HttpRequestException
    {
        public HttpRequestExceptionWithStatus() : base() { }

        public HttpRequestExceptionWithStatus(string message) : base(message) { }

        public HttpRequestExceptionWithStatus(string message, Exception inner) : base(message, inner) { }

        public HttpStatusCode StatusCode { get; set; }

        public int CurrentRetryCount { get; set; }
    }
}
