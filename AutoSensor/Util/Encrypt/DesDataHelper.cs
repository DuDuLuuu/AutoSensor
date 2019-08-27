using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ZW.Common.Encrypt
{
    public class DesDataHelper
    {
        public string EncryptData(string data,string key)
        {
            try
            {
                UnicodeEncoding ue = new UnicodeEncoding();
                byte[] bdata = ue.GetBytes(data);
                bdata = Encoding.Convert(ue, Encoding.GetEncoding("ASCII"), bdata);
                DESCryptoServiceProvider DESalg = new DESCryptoServiceProvider();
                byte[] iv = new byte[8];
                Encoding e = Encoding.ASCII;
                byte[] bkey = e.GetBytes(key);
                DESalg.Key = bkey;
                DESalg.IV = iv;
                DESalg.Padding = PaddingMode.Zeros;
                DESalg.Mode = CipherMode.ECB;
                MemoryStream sm = new MemoryStream();
                CryptoStream cStream = new CryptoStream(sm, DESalg.CreateEncryptor(), CryptoStreamMode.Write);
                cStream.Write(bdata, 0, bdata.Length);
                cStream.FlushFinalBlock();
                byte[] resultbyte = sm.ToArray();
                String strDesResult = "";
                for (int i = 0; i < resultbyte.Length; i++)
                {
                    String tmpBuf = resultbyte[i].ToString("X2");
                    strDesResult += tmpBuf;
                }
                sm.Close();
                cStream.Close();
                return strDesResult;
            }
            catch (Exception ee)
            {
                throw new ZWException(ExceptionCode.ENCRYPT_ERROR, "加密错误:" + ee.Message);
            }
        }
    }
}
