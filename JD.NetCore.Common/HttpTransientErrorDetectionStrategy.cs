using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JD.NetCore.Common
{
    public class HttpTransientErrorDetectionStrategy : ITransientErrorDetectionStrategy
    {
        public bool IsTransient(Exception ex)
        {
            if (ex != null)
            {
                HttpRequestExceptionWithStatus httpException;

                if ((httpException = ex as HttpRequestExceptionWithStatus) != null)
                {
                    if (httpException.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        return true;
                    }

                    return false;
                }
            }

            return false;
        }
    }
}
