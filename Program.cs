using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
/*
using System.Security.Cryptography.Algorithms;

System.Security.Cryptography.Aes
System.Security.Cryptography.RSA
System.Security.Cryptography.RSAParameters
System.Security.Cryptography.HMACSHA1
System.Security.Cryptography.SHA256
System.Security.Cryptography.SHA1
System.Security.Cryptography.SHA512
System.Security.Cryptography.SHA384
System.Security.Cryptography.HMACSHA256
System.Security.Cryptography.MD5
System.Security.Cryptography.HMACSHA384
System.Security.Cryptography.HMACSHA512

SHA-3
System.Security.Cryptography.HashAlgorithm
*/
namespace Password_Encryption_and_Authentication
{
    class Program
    {
        static void Main(string[] args)
        {
            string stringToHash = "test";
            byte[] toBeHased = Encoding.ASCII.GetBytes(stringToHash);
            HashAlgorithm sha = SHA512.Create();
            byte[] result = sha.ComputeHash(toBeHased);
            //convert to base16 then pad to look right
            string toScreen = BitConverter.ToString(result).Replace("-", "").ToLower();

            Console.WriteLine($"The text \"{stringToHash}\" once hashed by SHA512 looks like this: \n\n");
            Console.WriteLine(toScreen);

            Console.WriteLine("\nEnter your text below to see it hashed in real time\n\n");
            bool finished = false;
            do
            {
                int currentX = Console.CursorLeft;
                int currentY = Console.CursorTop;

                List<string> holdingArea = new List<string>();

                holdingArea.Add(Console.ReadKey());

            } while (!finished);
        }
    }
}
