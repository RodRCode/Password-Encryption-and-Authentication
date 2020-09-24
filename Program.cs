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
            int initialConsoleWidth = Console.WindowWidth;
            int hashLength = 128;

            Console.CursorTop = hashLength / initialConsoleWidth + 2;
            Console.WriteLine($"Enter your text below to see it hashed in real time:\n");

            string toScreen = GetKeyPresses();

        }

        private static void PrintRealTimeHashToScreen(string inputString)
        {
            int currentX = Console.CursorLeft;
            int currentY = Console.CursorTop;

            string hashedString = GetHashedString(inputString);

            Console.CursorTop = 0;
            Console.CursorLeft = 0;
            var originalBackgroundColor = Console.BackgroundColor;
            var originalForegroundColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(hashedString);
            Console.BackgroundColor = originalBackgroundColor;
            Console.ForegroundColor = originalForegroundColor;

            Console.CursorLeft = currentX;
            Console.CursorTop = currentY;
        }

        private static string GetHashedString(string inputString)
        {
            byte[] toBeHased = Encoding.ASCII.GetBytes(inputString);
            HashAlgorithm sha = SHA512.Create();
            byte[] hashedResult = sha.ComputeHash(toBeHased);
            string hashedString = BitConverter.ToString(hashedResult).Replace("-", "").ToLower();
            return hashedString;
        }

        private static string GetKeyPresses()
        {
            // Configure console.
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

                        inputString = inputString.Substring(0, oldLength - 1);
                        Console.CursorLeft = 0;
                        Console.Write(inputString + new String(' ', oldLength - inputString.Length));
                        Console.CursorLeft = cursorCol;
                        PrintRealTimeHashToScreen(inputString);
                    }
                    continue;
                }
                // Handle Escape key.
                if (keyInfo.Key == ConsoleKey.Escape) break;
                // Handle key by adding it to input string.
                Console.Write(keyInfo.KeyChar);
                inputString += keyInfo.KeyChar;
                PrintRealTimeHashToScreen(inputString);
            } while (keyInfo.Key != ConsoleKey.Enter);
            return inputString;
        }
    }
}
