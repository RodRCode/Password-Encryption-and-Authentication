using System;

namespace Password_Encryption_and_Authentication
{
    internal class App
    {
        public App()
        {
        }

        internal void Run()
        {
            EventLoop();
            Console.ResetColor();
        }

        private void EventLoop()
        {
            string menuMessage = "\n\nPASSWORD AUTHENTICATION SYSTEM" +
                "\nWhat would you like to do:";
            bool finished = false;
            do
            {
                Console.WriteLine("HERE IS A BIG FAT TEST!!!!!!");
                ConsoleMenuPainter.TextColor();
                Console.WriteLine(menuMessage);
                string[] menuItems = new string[] {
                "1) Establish an account",
                "2) Authenticate a user",
                "3) See a real time hash text as you type",
                "4) Quit" };
                //Console.Clear();
                int userChoice = Menu.Selection(menuItems, Console.CursorLeft + 1, Console.CursorTop + 1, menuMessage); 

                switch (userChoice)
                {
                    case 0:
                        //      Console.Clear();
                        Console.WriteLine($"{menuItems[0]}\n");
                        EstablishAccount();
                        break;
                    case 1:
                        //      Console.Clear();
                        Console.WriteLine($"{menuItems[1]}\n");
                        AuthenticateUser();
                        break;
                    case 2:
                        //      Console.Clear();
                        Console.WriteLine($"{menuItems[2]}\n");
                        RealTimeHashDemo();
                        break;
                    case 3:
                        //       Console.Clear();
                        Console.WriteLine($"{menuItems[3]}");
                        finished = true;
                        break;
                    default:
                        //        Console.Clear();
                        Console.WriteLine("Default case");
                        break;
                }
            } while (!finished);

        }


        private void RealTimeHashDemo()
        {
            int initialConsoleWidth = Console.WindowWidth;
            int hashLength = 128;

            Console.CursorTop = hashLength / initialConsoleWidth + 2;
            Console.WriteLine($"Enter your text below to see it hashed in real time:\n");

            string toScreen = CryptoStuff.GetKeyPresses();
        }

        private void AuthenticateUser()
        {
            throw new NotImplementedException();
        }

        private void EstablishAccount()
        {
            throw new NotImplementedException();
        }

        //  private int DetermineWhatUserWants()
        //  {
        //      Console.WriteLine("\n\nPASSWORD AUTHENTICATION SYSTEM");
        //      Console.WriteLine("\nWhat would you like to do:");
        //      Console.WriteLine("1) Establish an account");
        //      Console.WriteLine("2) Authenticate a user");
        //      Console.WriteLine("3) See a real time has of text as you type");
        //      Console.WriteLine("4) Quit");
        //      Console.WriteLine();
        //      Console.Write("Either press the number of your choice\n" +
        //          "Or use the arrow keys and hit 'Enter'\n" +
        //          "Your choice: ");
        //
        //      int userChoice = Elicit.WholeNumber();
        //      return userChoice;
        //  }
    }
}