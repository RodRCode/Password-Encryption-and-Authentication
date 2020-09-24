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

            FromMSFTReadKey();
            //NewThingy();

        }

        private static void NewThingy()
        {
            bool finished = false;
            do
            {
                int currentX = Console.CursorLeft;
                int currentY = Console.CursorTop;

                List<string> holdingArea = new List<string>();
            } while (!finished);
        }

        private static void FromMSFTReadKey()
        {
            // Configure console.
            Console.BufferWidth = 80;
            Console.WindowWidth = Console.BufferWidth;
            Console.TreatControlCAsInput = true;

            string inputString = String.Empty;
            ConsoleKeyInfo keyInfo;

            Console.WriteLine("Enter a string. Press <Enter> or Esc to exit.");
            do
            {
                keyInfo = Console.ReadKey(true);
                // Ignore if Alt or Ctrl is pressed.
                if ((keyInfo.Modifiers & ConsoleModifiers.Alt) == ConsoleModifiers.Alt)
                    continue;
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)
                    continue;
                // Ignore if KeyChar value is \u0000.
                if (keyInfo.KeyChar == '\u0000') continue;
                // Ignore tab key.
                if (keyInfo.Key == ConsoleKey.Tab) continue;
                // Handle backspace.
                if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    // Are there any characters to erase?
                    if (inputString.Length >= 1)
                    {
                        // Determine where we are in the console buffer.
                        int cursorCol = Console.CursorLeft - 1;
                        int oldLength = inputString.Length;
                        int extraRows = oldLength / 80;

                        inputString = inputString.Substring(0, oldLength - 1);
                        Console.CursorLeft = 0;
                        Console.CursorTop = Console.CursorTop - extraRows;
                        Console.Write(inputString + new String(' ', oldLength - inputString.Length));
                        Console.CursorLeft = cursorCol;
                    }
                    continue;
                }
                // Handle Escape key.
                if (keyInfo.Key == ConsoleKey.Escape) break;
                // Handle key by adding it to input string.
                Console.Write(keyInfo.KeyChar);
                inputString += keyInfo.KeyChar;
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine("\n\nYou entered:\n    {0}",
                              String.IsNullOrEmpty(inputString) ? "<nothing>" : inputString);
        }
    }    
}
