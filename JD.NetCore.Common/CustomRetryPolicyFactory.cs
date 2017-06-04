using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;
using System;
using System.Collections.Generic;
using System.Text;

namespace JD.NetCore.Common
{
    public static class CustomRetryPolicyFactory
    {
        public static Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling.RetryPolicy MakeHttpRetryPolicy()
        {
            var strategy = new HttpTransientErrorDetectionStrategy();
            return Exponential(strategy);
        }

        private static Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling.RetryPolicy Exponential(ITransientErrorDetectionStrategy strategy)
        {
            var retryCount = 3;
            var minBackoff = TimeSpan.FromSeconds(1);
            var maxBackoff = TimeSpan.FromSeconds(10);
            var deltaBackoff = TimeSpan.FromSeconds(5);

            var exponentialBackoff = new ExponentialBackoff(retryCount, minBackoff, maxBackoff, deltaBackoff);

            return new Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling.RetryPolicy(strategy, exponentialBackoff);
        }
    }
}
