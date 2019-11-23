using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MedicalInformationSystemWebApp.Models
{
    public class PasswordHelper
    {
        static private byte[] key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
            15, 16, 17, 18, 19, 20, 21, 22, 23, 24};
        static private byte[] iv16Bit = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

        public string Encode(string password)
        {
            byte[] pa = new byte[password.Length];
            pa = System.Text.Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(pa);
        }

        public string Decode(string Encrepted)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decoder = encoder.GetDecoder();
            byte[] DecoderByte = Convert.FromBase64String(Encrepted);
            //byte[] DecoderByte = Convert.FromBase64String(Encrepted.Replace("", "+"));
            //byte[] DecoderByte = Encoding.ASCII.GetBytes(Encrepted);
            int CharCount = utf8Decoder.GetCharCount(DecoderByte, 0, DecoderByte.Length);
            char[] DecoderChar = new char[CharCount];
            utf8Decoder.GetChars(DecoderByte, 0, DecoderByte.Length, DecoderChar, 0);
            string DecryptedData = new string(DecoderChar);
            return DecryptedData;
        }


        public string AesEncryption(string dataToEncrypt)
        {
            var bytes = Encoding.Default.GetBytes(dataToEncrypt);
            using (var aes = new AesCryptoServiceProvider())
            {
                using (var ms = new MemoryStream())
                using (var encryptor = aes.CreateEncryptor(key, iv16Bit))
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    var cipher = ms.ToArray();
                    return Convert.ToBase64String(cipher);
                }
            }
        }

        public string AesDecryption(string dataToDecrypt)
        {
            var bytes = Convert.FromBase64String(dataToDecrypt);
            using (var aes = new AesCryptoServiceProvider())
            {
                using (var ms = new MemoryStream())
                using (var decryptor = aes.CreateDecryptor(key, iv16Bit))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                {
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                    var cipher = ms.ToArray();
                    return Encoding.UTF8.GetString(cipher);
                }
            }
        }


    }
}