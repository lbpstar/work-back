using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMTCost
{
public class EncryptUtility
{
        //默认密钥向量
        #region DES
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="code">加密字符串</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string DesEncrypt(string code, string key)
    {
        string iv = Reverse(key.Substring(0, 8));
        return DesEncrypt(code, key, iv);
    }

    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="code">加密字符串</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">初始化向量</param>
    /// <returns></returns>
    public static string DesEncrypt(string code, string key, string iv)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Encoding.UTF8.GetBytes(code);
        des.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
        des.IV = Encoding.UTF8.GetBytes(iv);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return Convert.ToBase64String(ms.ToArray());
    }


    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="code">解密字符串</param>
    /// <param name="key">密钥</param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string DesDecrypt(string code, string key)
    {
        string iv = Reverse(key.Substring(0, 8));
        return DesDecrypt(code, key, iv);
    }


    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="code">解密字符串</param>
    /// <param name="key">密钥</param>
    /// <param name="iv">初始化向量</param>
    /// <returns></returns>
    public static string DesDecrypt(string code, string key, string iv)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray = Convert.FromBase64String(code);

        des.Key = Encoding.UTF8.GetBytes(key.Substring(0, 8));
        des.IV = Encoding.UTF8.GetBytes(iv);
        MemoryStream ms = new MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        cs.Dispose();
        return Encoding.UTF8.GetString(ms.ToArray());
        }
        #endregion
        /// <summary>
        /// 字符串倒序
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Reverse(string text)
        {
            char[] charArray = text.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
