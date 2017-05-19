using System;
using System.Collections.Generic;
using System.Text;

namespace JD.NetCore.SDK
{
    public class BaseResult<T>
    {
        public bool Success { get; set; }
        public string ResultMessage { get; set; }
        public string ResultCode { get; set; }
        public T Result { get; set; }
    }
}
