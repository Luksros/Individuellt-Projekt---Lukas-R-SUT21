using System;
using System.Collections.Generic;

namespace Individuellt_Projekt___Lukas_R_SUT21
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            Account[] AllAccounts = { new Account ("LuRo", "BadPassword123", "Lukas", "Rose"),
                                      new Account("ErNo", "Norell95", "Erik", "Norell"), 
                                      new Account("DaLa", "Lösenord123", "David", "Larsson"),
                                      new Account("ViGu", "Bergfalk9", "Viktor", "Gunnarsson"),
                                      new Account("ToLa", "H4ck3rm4n", "Tobias", "Landén") };

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
                        Console.WriteLine("Tryck Enter för att återvända.");
                        Console.ReadLine();
                        break;
                    }
                case 2:
                    {
                        transferFunds(LoggedInUser);
                        Console.WriteLine("Tryck Enter för att återvända.");
                        Console.ReadLine();
                        break;
                    }
                case 3:
                    {
                        withdrawFunds(LoggedInUser);
                        Console.WriteLine("Tryck Enter för att återvända");
                        Console.ReadLine();
                        break;
                    }
                case 4:
                    {
                        addFunds(LoggedInUser);
                        Console.WriteLine("Tryck Enter för att återvända");
                        Console.ReadLine();
                        break;
                    }
                case 5:
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

            string[] menuChoices = { "[1] Se dina konton och saldo", "[2] Överföring mellan konton", "[3] Ta ut pengar", "[4] Sätta in pengar", "[5] Logga ut" };
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

        public static void checkBalance(Account loggedInUser, string print = "KONTOÖVERSIKT")
        {
            Console.Clear();
            Console.WriteLine(print);
            Console.WriteLine();
            for (int i = 0; i < loggedInUser.accountNames.Count; i++)
            {
                Console.WriteLine("["+(i+1)+"]" + loggedInUser.accountNames[i] + ": " + loggedInUser.accounts[i]);
            }
            Console.WriteLine();
        }

        public static void transferFunds(Account loggedInUser)
        {
            Console.Clear();
            checkBalance(loggedInUser, "KONTON");
            Console.WriteLine("Vilket konto vill du föra över pengar ifrån?");          
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 0) || (select > loggedInUser.accounts.Count))
            {
                Menu.ClearLine();
            }

            checkBalance(loggedInUser, ("Föra över från " + loggedInUser.accountNames[select-1] + " till vilket konto?"));

            int select2 = 0;
            while ((!int.TryParse(Console.ReadLine(), out select2)) || (select2 < 0) || (select2 > loggedInUser.accounts.Count) || (select2 == select))
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
            Console.WriteLine();
        }
        public static void withdrawFunds(Account loggedInUser)
        {
            Console.Clear();
            checkBalance(loggedInUser, "KONTON");
            Console.WriteLine("Vilket konto vill du ta ut pengar ifrån?");
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 0) || (select > loggedInUser.accounts.Count))
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
        public static void addFunds(Account loggedInUser)
        {
            Console.Clear();
            checkBalance(loggedInUser, "KONTON");
            Console.WriteLine("Vilket konto vill du sätta in pengar på?");
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 0) || (select > loggedInUser.accounts.Count))
            {
                Menu.ClearLine();
            }
            Console.WriteLine("Hur mycket vill du sätta in?");
            decimal transferAmt = 0.00m;
            while (!Decimal.TryParse(Console.ReadLine(), out transferAmt) || (transferAmt > loggedInUser.accounts[select - 1]))
            {
                Menu.ClearLine();
            }
            loggedInUser.accounts[select - 1] = loggedInUser.accounts[select - 1] + transferAmt;
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
