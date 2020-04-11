using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Inventory.BLL
{

    
        class SecurityHelper
        {

            const string LOWER_CASE = "abcdefghijklmnopqursuvwxyz";
            const string UPPER_CAES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMBERS = "123456789";
            const string SPECIALS = @"!@£$%^&*()#€";


            public static string GeneratePassword(int passwordSize=8,bool useLowercase=true, bool useUppercase=true, bool useNumbers=true, bool useSpecial=true)
            {
                char[] _password = new char[passwordSize];
                string charSet = ""; // Initialise to blank
                System.Random _random = new Random();
                int counter;

                // Build up the character set to choose from
                if (useLowercase) charSet += LOWER_CASE;

                if (useUppercase) charSet += UPPER_CAES;

                if (useNumbers) charSet += NUMBERS;

                if (useSpecial) charSet += SPECIALS;

                for (counter = 0; counter < passwordSize; counter++)
                {
                    _password[counter] = charSet[_random.Next(charSet.Length - 1)];
                }

                return String.Join(null, _password);
            }
        public static string MD5(string password)
        {
            //Encrypt the password
            System.Security.Cryptography.MD5 md5Hasher = MD5CryptoServiceProvider.Create();
            byte[] hashedBytes;
            ASCIIEncoding encoder = new ASCIIEncoding();
            hashedBytes = md5Hasher.ComputeHash(encoder.GetBytes(password));

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++)

            {

                sb.Append(hashedBytes[i].ToString("X2"));

            }

            return sb.ToString();
        }


    }

    
}