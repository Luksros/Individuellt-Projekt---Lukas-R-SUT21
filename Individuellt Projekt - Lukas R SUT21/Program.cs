using System;
using System.Collections.Generic;

namespace Individuellt_Projekt___Lukas_R_SUT21
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            Account[] AllAccounts = new Account[5];
            Account Lukas = new Account("LuksRos", "BadPassword123", "Lukas", "Rose");
            Account David = new Account("Daviddd", "Lösenord123", "David", "Larsson");
            AllAccounts[0] = Lukas;
            for (int i = 1; i < 5; i++)
            {
                AllAccounts[i] = David;
            }


            //Login
            int accountIndex = 10;
            int loginTries = 0;
            bool loggedIn = false;
            while (!loggedIn && loginTries < 3)
            {
                loggedIn = Menu.Login(AllAccounts, ref accountIndex, ref loginTries);
                while (loggedIn)
                {
                    Menu.MainMenu(AllAccounts[accountIndex], ref loggedIn);
                }
            }
            if (loginTries > 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine("Du har misslyckats att logga in för många gånger. Programmet är nu spärrat.");
            }
            Console.ReadLine();
        }
    }
    public static class Menu
    {
        public static void MainMenu(Account LoggedInUser, ref bool tempLoggedIn)
        {
            PrintMenu(LoggedInUser.name, LoggedInUser.surName);
            int selector = 0;
            while (!int.TryParse(Console.ReadLine(), out selector))
            {

            }
            switch (selector)
            {
                case 1:
                    {
                        checkBalance(LoggedInUser);
                        break;
                    }
                case 2:
                    {
                        transferFunds(LoggedInUser);
                        break;
                    }
                case 3:
                    {
                        withdrawFunds(LoggedInUser);
                        break;
                    }
                case 4:
                    {
                        tempLoggedIn = false;
                        break;
                    }
            }
        }

        public static void PrintMenu(string tempUserName, string tempUserSurName)
        {
            Console.Clear();
            Console.WriteLine("Välkommen, {0} {1}", tempUserName, tempUserSurName);
            Console.WriteLine();

            string[] menuChoices = { "1. Se dina konton och saldo", "2. Överföring mellan konton", "3. Ta ut pengar", "4. Logga ut" };
            for (int i = 0; i < menuChoices.Length; i++)
            {
                Console.WriteLine(menuChoices[i]);
            }
        }
        public static bool Login(Account[] AccountList, ref int indexTarget, ref int loginCounter)
        {
            for (int i = 0; i < 3; i++)
            {
                loginPrint();
                Console.SetCursorPosition(14, 2);
                string usernameInput = Console.ReadLine();
                Console.SetCursorPosition(10, 3);
                string passInput = Console.ReadLine();

                for (int k = 0; k < AccountList.Length; k++)
                {
                    if ((AccountList[k].userName == usernameInput) && (AccountList[k].password == passInput))
                    {
                        indexTarget = k;
                        loginCounter = 0;
                        return true;
                    }
                }
                loginCounter++;
            }
            return false;
        }

        public static void loginPrint()
        {
            Console.Clear();
            string[] loginScreen = { "Vänligen fyll i användarnamn och lösenord.", " ", "Användarnamn: ", "Lösenord: " };
            for (int j = 0; j < loginScreen.Length; j++)
            {
                Console.WriteLine(loginScreen[j]);
            }
        }

        public static void checkBalance(Account loggedInUser)
        {
            //string[] strings = { "Kontoöversikt: ", " ", loggedInUser.accountNames[0], " ", Decimal.ToString(loggedInUser.accounts[0]),   };
            Console.Clear();
            Console.WriteLine("KONTOÖVERSIKT");
            Console.WriteLine();
            Console.Write(loggedInUser.accountNames[0] + ": " + loggedInUser.accounts[0]);
            Console.WriteLine();
            Console.Write(loggedInUser.accountNames[1] + ": " + loggedInUser.accounts[1]);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Tryck Enter för att återvända.");
            Console.ReadLine();
        }

        public static void transferFunds(Account loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("KONTON");
            Console.WriteLine();
            Console.WriteLine("Vilket konto vill du föra över pengar ifrån?");
            Console.WriteLine();
            Console.Write("[1] " + loggedInUser.accountNames[0] + ": " + loggedInUser.accounts[0]);
            Console.WriteLine();
            Console.Write("[2] " + loggedInUser.accountNames[1] + ": " + loggedInUser.accounts[1]);
            Console.WriteLine();
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || ((select != 1) && (select != 2)))
            {
                Menu.ClearLine();
            }


            Console.Clear();
            Console.WriteLine("Föra över från " + loggedInUser.accountNames[select - 1] + " till vilket konto?");
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("[1] " + loggedInUser.accountNames[0] + ": " + loggedInUser.accounts[0]);
            Console.WriteLine();
            Console.Write("[2] " + loggedInUser.accountNames[1] + ": " + loggedInUser.accounts[1]);
            Console.WriteLine();

            int select2 = 0;
            while ((!int.TryParse(Console.ReadLine(), out select2)) || ((select2 != 1) && (select2 != 2)) || (select2 == select))
            {
                Menu.ClearLine();
            }
            Console.WriteLine("Hur mycket vill du föra över?");
            decimal transferAmt = 0.00m;
            while (!Decimal.TryParse(Console.ReadLine(), out transferAmt) || (transferAmt > loggedInUser.accounts[select-1]))
            {
                Menu.ClearLine();
            }
            loggedInUser.accounts[select - 1] = loggedInUser.accounts[select - 1] - transferAmt;
            loggedInUser.accounts[select2 - 1] = loggedInUser.accounts[select2 - 1] + transferAmt;
            checkBalance(loggedInUser);
        }
        public static void withdrawFunds(Account loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("KONTON");
            Console.WriteLine();
            Console.WriteLine("Vilket konto vill du ta ut pengar ifrån?");
            Console.WriteLine();
            Console.Write("[1] " + loggedInUser.accountNames[0] + ": " + loggedInUser.accounts[0]);
            Console.WriteLine();
            Console.Write("[2] " + loggedInUser.accountNames[1] + ": " + loggedInUser.accounts[1]);
            Console.WriteLine();
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || ((select != 1) && (select != 2)))
            {
                Menu.ClearLine();
            }
            Console.WriteLine("Hur mycket vill du ta ut?");
            decimal transferAmt = 0.00m;
            while (!Decimal.TryParse(Console.ReadLine(), out transferAmt) || (transferAmt > loggedInUser.accounts[select - 1]))
            {
                Menu.ClearLine();
            }
            loggedInUser.accounts[select - 1] = loggedInUser.accounts[select - 1] - transferAmt;
            checkBalance(loggedInUser);
        }
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
    public class Account
    {
        public string userName;
        public string password;
        public string name;
        public string surName;
        public List<decimal> accounts = new List<decimal> { 10000.00m, 100000.00m };
        public List<string> accountNames = new List<string> { "Lönekonto", "Sparkonto" };

        public Account(string userNameInput, string passwordInput, string nameInput, string surNameInput)
        {
            userName = userNameInput;
            password = passwordInput;
            name = nameInput;
            surName = surNameInput;
        }
    }
}
