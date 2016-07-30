using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Utility.Helper
{
    /// <summary>
    /// 签名帮助类
    /// </summary>
    public class SignHelper
    {
        /// <summary>
        /// 签名认证
        /// </summary>
        /// <param name="paramDic">签名参数</param>
        /// <param name="secret">签名秘钥</param>
        /// <returns></returns>
        public static string Sign(Dictionary<string, string> paramDic, string secret)
        {
            //第一步：把所有参数Key按照字母顺序排序
            var sortedParamDic = paramDic.OrderBy(p => p.Key).ToDictionary(p => p.Key, q => q.Value);
            //第二步：把所有排序后的参数拼接成字符串
            StringBuilder paramBuiler = new StringBuilder();
            foreach (var item in sortedParamDic)
            {
                paramBuiler.Append(item.Key).Append(item.Value);
            }

            return SecurityHelper.SHA1Sign(paramBuiler.ToString(), secret, Encoding.UTF8);
        }
    }
}
