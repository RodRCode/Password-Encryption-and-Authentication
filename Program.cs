using System;
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
            string toScreen = ConvertByteToString(result);

            Console.WriteLine($"The text \"{stringToHash}\" once hashed by SHA512 looks like this: \n\n");
            Console.WriteLine(toScreen);
        }

        private static string ConvertByteToString(byte[] result)
        {
            throw new NotImplementedException();
        }
    }
}
