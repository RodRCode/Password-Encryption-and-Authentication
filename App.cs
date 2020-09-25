using System;
using System.Collections.Generic;

namespace Password_Encryption_and_Authentication
{
    internal class App
    {
        Dictionary<string, string> userAndPassword = new Dictionary<string, string>();
        public App()
        {
        }

        internal void Run()
        {
            EventLoop();
            Console.ResetColor();
            PrintUserNamesAndPasswords();
        }

        private void PrintUserNamesAndPasswords()
        {
            Console.WriteLine("\n\nHere are all the usernames and passwords entered");
            foreach (var entry in userAndPassword)
            {
                Console.WriteLine($"Username: {entry.Key}");
                Console.WriteLine($"Password: {entry.Value}");
                Console.WriteLine();
            }
        }

        private void EventLoop()
        {
            string menuMessage = "\n\nPASSWORD AUTHENTICATION SYSTEM" +
                "\nMake your choice using the arrow keys or the numbers, then (ENTER)" +
                "\nWhat would you like to do:";
            bool finished = false;
            do
            {
                ConsoleMenuPainter.TextColor();
                string[] menuItems = new string[] {
                "1) Establish an account",
                "2) Authenticate a user",
                "3) See a real time hash text as you type",
                "4) Quit" };
                Console.Clear();
                int userChoice = Menu.Selection(menuItems, Console.CursorLeft + 1, Console.CursorTop, menuMessage);

                switch (userChoice)
                {
                    case 0:
                        EstablishAccount();
                        break;
                    case 1:
                        AuthenticateUser();
                        break;
                    case 2:
                        RealTimeHashDemo();
                        break;
                    case 3:
                        finished = true;
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
            } while (!finished);

        }


        private void RealTimeHashDemo()
        {
            Console.Clear();
            int initialConsoleWidth = Console.WindowWidth;
            int hashLength = 128;

            Console.CursorTop = hashLength / initialConsoleWidth + 2;
            Console.WriteLine($"Enter your text below to see it hashed in real time:\n");

            string toScreen = CryptoStuff.GetKeyPressesRealTimeHashDisplay();
        }

        private void AuthenticateUser()
        {
            Console.Clear();
            Console.Write("\nEnter the username you want to authenticate: ");
            string testUsername = Console.ReadLine();
            if (userAndPassword.TryGetValue(testUsername, out string value))
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Username {testUsername} is a registered user.\n");
                Console.ForegroundColor = originalColor;

                Console.Write("Enter the password: ");
                string testPassword = HideTextAsEntered();
                string testHash = CryptoStuff.GetHashedString(testPassword);

                if (testHash == userAndPassword[testUsername])
                {
                    originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"User {testUsername} has been authenticated!");
                    Console.ForegroundColor = originalColor;
                }
                else
                {
                    originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"User {testUsername} has NOT been authenticated!");
                    Console.ForegroundColor = originalColor;
                }

                Console.Write("\nEnter any key to continue: ");
                Console.ReadKey();
            }
            else
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nUsername {testUsername} isn't a registered user.");
                Console.ForegroundColor = originalColor;

                Console.Write("\nEnter any key to continue: ");
                Console.ReadKey();
            }
        }

        private void EstablishAccount()
        {
            Console.Clear();
            bool done = false;
            while (!done)
            {
                string userName = "";
                Console.Write("\nEnter a user name, then press (enter): ");
                userName = Console.ReadLine();
                // if the username is unique in the dictionary
                if (!userAndPassword.ContainsKey(userName))
                {
                    userAndPassword.Add(userName, null);
                    Console.Write("\nNow the password, then press (enter): ");
                    string password = HideTextAsEntered();
                    Console.WriteLine();
                    Console.WriteLine(password);
                    password = CryptoStuff.GetHashedString(password);
                    //stores the hashed password into the dictionary
                    userAndPassword[userName] = password;
                    done = true;
                }
                else // If the username is already taken
                {
                    var originalColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThat username already exists!");
                    Console.ForegroundColor = originalColor;
                    Console.Write("\nWould you like to try again (y/n): ");
                    bool finished = false;
                    do
                    {
                        var answer = Console.ReadKey(true);
                        switch (answer.Key)
                        {
                            case ConsoleKey.Y:
                                Console.Write("y");
                                Console.WriteLine("\n\nGreat, try a different username this time!");
                                finished = true;
                                break;
                            case ConsoleKey.N:
                                Console.Write("n");
                                finished = true;
                                done = true;
                                break;
                            default:
                                break;
                        }
                    } while (!finished);
                }
            }
        }

        // Does not show the password as it is being entered character by character
        private string HideTextAsEntered()
        {
            // Configure console.

            string inputString = String.Empty;
            ConsoleKeyInfo keyInfo;
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

                        // In case we need to delete across multiple rows
                        if (cursorCol < 0)
                        {
                            cursorCol = Console.WindowWidth - 1;
                            Console.CursorTop = Console.CursorTop - 1;
                        }

                        int oldLength = inputString.Length;

                        inputString = inputString.Substring(0, oldLength - 1);
                        Console.CursorLeft = cursorCol;
                        Console.Write(" ");

                        Console.CursorLeft = cursorCol;
                    }
                    continue;
                }
                // Handle Escape key.
                if (keyInfo.Key == ConsoleKey.Escape) continue;
                // Handle key by adding it to input string.

                if (keyInfo.Key == ConsoleKey.Enter) continue;
                inputString += keyInfo.KeyChar;
                Console.Write("*");
            } while (keyInfo.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return inputString;
        }
    }
}