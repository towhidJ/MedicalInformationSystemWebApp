using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MedicalInformationSystemWebApp.Models
{
    public class PasswordHelper
    {
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
    }
}