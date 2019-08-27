using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace ZW.Common.Properties
{
    public class DesProperties : EncryptProperties
    {
        public string DesKey { get; set; }

        public override void load(string filePath)
        {
            if (File.Exists(filePath) == false)
            {
                return;
            }
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                Stream stream = DesDecrypt(fileStream);
                LoadStream(stream);
                stream.Close();
            }
        }

        public override void save(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            using (FileStream fileStream = File.Create(filePath))
            {
                MemoryStream sm = new MemoryStream();
                saveStream(sm);
                DesEncrypt(sm, fileStream);
                sm.Close();
            }

        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private void DesEncrypt(MemoryStream srcStream, Stream dstStream)
        {
            try
            {

                TripleDESCryptoServiceProvider DESalg = new TripleDESCryptoServiceProvider();
                byte[] iv = new byte[8];
                Encoding e = Encoding.ASCII;
                byte[] key = e.GetBytes(DesKey);
                DESalg.Key = key;
                DESalg.IV = iv;
                DESalg.Padding = PaddingMode.Zeros;
                DESalg.Mode = CipherMode.ECB;
                CryptoStream cStream = new CryptoStream(dstStream, DESalg.CreateEncryptor(), CryptoStreamMode.Write);
                srcStream.WriteTo(cStream);
                cStream.FlushFinalBlock();
                cStream.Close();

            }
            catch (Exception ee)
            {
                throw new ZWException(ExceptionCode.ENCRYPT_ERROR, "加密错误:" + ee.Message);
            }

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="content">密文</param>
        /// <returns></returns>
        private Stream DesDecrypt(Stream dataStream)
        {
            try
            {
                TripleDESCryptoServiceProvider DESalg = new TripleDESCryptoServiceProvider();
                byte[] iv = new byte[8];
                Encoding e = Encoding.ASCII;
                byte[] key = e.GetBytes(DesKey);
                DESalg.Key = key;
                DESalg.IV = iv;
                DESalg.Padding = PaddingMode.Zeros;
                DESalg.Mode = CipherMode.ECB;
                CryptoStream cStream = new CryptoStream(dataStream, DESalg.CreateDecryptor(), CryptoStreamMode.Read);
                return cStream;
                //StreamReader sr = new StreamReader(cStream, Encoding.Unicode);

                //string strDesTxt = sr.ReadToEnd();
                //UnicodeEncoding ue = new UnicodeEncoding();
                //byte[] fromEncrpyt = ue.GetBytes(strDesTxt);
                //strDesTxt = ue.GetString(fromEncrpyt);
                //cStream.Close();
                //return strDesTxt;
            }
            catch (Exception ee)
            {
                throw new ZWException(ExceptionCode.DECRYPT_ERROR, "解密错误:" + ee.Message);
            }
        }
    }
}
