﻿using System.Security.Cryptography;
using System.Text;

namespace GoodLawSoftware.Helpers
{
    public class EncryptionManager
    {
        private readonly static string key = "EngineerMohamedSamy871ASPNetLove";
        public static string Encrypt(string pass)
        {
            byte[] iv=new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key,aes.IV);
                using (MemoryStream ms  = new MemoryStream())
                {
                    using(CryptoStream cryptoStream = new CryptoStream((Stream)ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(pass);   
                        }
                        array = ms.ToArray();   
                    }   
                } 
            }
            return Convert.ToBase64String(array);
        }
        public static string Decrypt(string pass)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(pass);
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Read ))
                    {
                        using (StreamReader sr = new StreamReader(cryptoStream))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}
