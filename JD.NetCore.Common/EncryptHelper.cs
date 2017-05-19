using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace JD.NetCore.Common
{
    public class EncryptHelper
    {
        /// <summary>
        /// netcore下的实现MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string MD5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}
