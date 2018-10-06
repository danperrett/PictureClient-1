using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictureClient
{
    class EncoderDecoder
    {
        public string Encode(string content, byte key)
        {
            byte[] data = Encoding.ASCII.GetBytes(content.ToCharArray());
            return Convert.ToBase64String(scramble(data, key));

        }

        public string Decode(string content, byte key)
        {
            byte[] data = scramble(Convert.FromBase64String(content), key);
            return System.Text.Encoding.UTF8.GetString(data);
        }

        public string Encode(byte[] data, byte key)
        {
            return System.Text.Encoding.UTF8.GetString(scramble(data, key));
        }

        public byte[] DecodeData(string content, byte key)
        {
            return scramble(Convert.FromBase64String(content), key); 
        }

        private byte[] scramble(byte[] data, byte key)
        {
            byte[] o = new byte[data.Length];
            int count = 0;
            foreach (byte d in data)
            {
                o[count] = (byte)(d ^ key);
                count++;
            }
            return o;
        }

        public string encodePassword(string username, string password)
        {
            string pass = "";
            try
            {
                int i = 0;
                char[] _username = username.ToCharArray();
                char[] _password = password.ToCharArray();
                byte[] data = new byte[_password.Length];

                for (int n = 0; n < data.Length; n++)
                {
                    data[n] = (byte)((byte)_password[n] ^ (byte)_username[i++]);
                    if (i == _username.Length)
                    {
                        i = 0;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return pass;
        }
    }
}

