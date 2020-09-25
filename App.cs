﻿using System;

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
                int userChoice = Menu.Selection(menuItems, Console.CursorLeft+1, Console.CursorTop, menuMessage); 

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
            throw new NotImplementedException();
        }

        private void EstablishAccount()
        {
            throw new NotImplementedException();
        }
    }
}