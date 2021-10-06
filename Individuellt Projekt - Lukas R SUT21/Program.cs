using System;
using System.Collections.Generic;

namespace Individuellt_Projekt___Lukas_R_SUT21
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            User[] AllUsers = { new User ("LuRo", "BadPassword123", "Lukas", "Rose"),
                                new User("ErNo", "Norell95", "Erik", "Norell"), 
                                new User("DaLa", "Lösenord123", "David", "Larsson"),
                                new User("ViGu", "Bergfalk9", "Viktor", "Gunnarsson"),
                                new User("ToLa", "H4ck3rm4n", "Tobias", "Landén") };

            //Login
            int accountIndex = 10;
            int loginTries = 0;
            bool loggedIn = false;
            while (!loggedIn && loginTries < 3)
            {
                loggedIn = Menu.Login(AllUsers, ref accountIndex, ref loginTries);
                while (loggedIn)
                {
                    Menu.MainMenu(AllUsers[accountIndex], ref loggedIn);
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
        public static void MainMenu(User LoggedInUser, ref bool tempLoggedIn)
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
                        manageAccounts(LoggedInUser);
                        break;
                    }
                case 3:
                    {
                        transferFunds(LoggedInUser);
                        Console.WriteLine("Tryck Enter för att återvända.");
                        Console.ReadLine();
                        break;
                    }
                case 4:
                    {
                        withdrawFunds(LoggedInUser);
                        Console.WriteLine("Tryck Enter för att återvända");
                        Console.ReadLine();
                        break;
                    }
                case 5:
                    {
                        addFunds(LoggedInUser);
                        Console.WriteLine("Tryck Enter för att återvända");
                        Console.ReadLine();
                        break;
                    }
                case 6:
                    {
                        logOut();
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

            string[] menuChoices = { "[1] Se dina konton och saldo", "[2] Hantera konton","[3] Överföring mellan konton", "[4] Ta ut pengar", "[5] Sätta in pengar", "[6] Logga ut" };
            for (int i = 0; i < menuChoices.Length; i++)
            {
                Console.WriteLine(menuChoices[i]);
            }
        }
        public static bool Login(User[] AccountList, ref int indexTarget, ref int loginCounter)
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
                        Console.Clear();
                        Console.Write("Loggar in ");
                        Console.Beep(261, 150);
                        Console.Write(". ");                      
                        Console.Beep(329, 150);
                        Console.Write(". ");
                        Console.Beep(392, 150);
                        Console.Write(". ");
                        Console.Beep(522, 250);
                        return true;
                    }
                }
                loginCounter++;
            }
            Console.Beep(247, 300);
            Console.Beep(174, 300);
            return false;          
        }
        public static void logOut()
        {
            Console.Clear();
            Console.Write("Loggar ut ");
            Console.Beep(522, 250);           
            Console.Write(". ");
            Console.Beep(392, 150);
            Console.Write(". ");
            Console.Beep(329, 150);           
            Console.Write(". ");
            Console.Beep(261, 150);
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

        public static void checkBalance(User loggedInUser, string print = "KONTOÖVERSIKT")
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

        public static void transferFunds(User loggedInUser)
        {
            Console.Clear();
            checkBalance(loggedInUser, "KONTON");
            Console.WriteLine("Vilket konto vill du föra över pengar ifrån?");          
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 1) || (select > loggedInUser.accounts.Count))
            {
                Menu.ClearLine();
            }

            checkBalance(loggedInUser, ("Föra över från " + loggedInUser.accountNames[select-1] + " till vilket konto?"));

            int select2 = 0;
            while ((!int.TryParse(Console.ReadLine(), out select2)) || (select2 < 1) || (select2 > loggedInUser.accounts.Count) || (select2 == select))
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(transferAmt + " Kr fördes över från " + loggedInUser.accountNames[select - 1] + " till " + loggedInUser.accountNames[select2 - 1]);
            Console.ResetColor();
        }
        public static void withdrawFunds(User loggedInUser)
        {
            Console.Clear();
            checkBalance(loggedInUser, "KONTON");
            Console.WriteLine("Vilket konto vill du ta ut pengar ifrån?");
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 1) || (select > loggedInUser.accounts.Count))
            {
                Menu.ClearLine();
            }
            ClearLine();
            Console.Write("Skriv in hur mycket du vill ta ut: ");
            decimal transferAmt = 0.00m;
            while (!Decimal.TryParse(Console.ReadLine(), out transferAmt) || (transferAmt > loggedInUser.accounts[select - 1]))
            {
                Menu.ClearLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Ogiltig input. ");
                Console.ResetColor();
                Console.Write("Skriv in hur mycket du vill ta ut: ");
            }
            loggedInUser.accounts[select - 1] = loggedInUser.accounts[select - 1] - transferAmt;
            checkBalance(loggedInUser);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(transferAmt + " Kr togs ut från " + loggedInUser.accountNames[select -1]);
            Console.ResetColor();
        }
        public static void addFunds(User loggedInUser)
        {
            Console.Clear();
            checkBalance(loggedInUser, "KONTON");
            Console.WriteLine("Vilket konto vill du sätta in pengar på?");
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 1) || (select > loggedInUser.accounts.Count))
            {
                Menu.ClearLine();
            }
            ClearLine();
            Console.WriteLine("Hur mycket vill du sätta in?");
            decimal transferAmt = 0.00m;
            while (!Decimal.TryParse(Console.ReadLine(), out transferAmt))
            {
                Menu.ClearLine();
            }
            loggedInUser.accounts[select - 1] = loggedInUser.accounts[select - 1] + transferAmt;
            checkBalance(loggedInUser);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(transferAmt + " Kr sattes in på " + loggedInUser.accountNames[select - 1]);
            Console.ResetColor();
        }

        public static void manageAccounts (User loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("Vill du lägga till eller ta bort ett konto?");
            Console.WriteLine();
            Console.WriteLine("[1] Lägga till");
            Console.WriteLine("[2] Ta bort");
            int select = 0;
            while ((!int.TryParse(Console.ReadLine(), out select)) || ((select != 1) && (select != 2)))
            {
                Menu.ClearLine();
            }
            if (select == 1)
            {
                Console.Clear();
                Console.WriteLine("Vad skall det nya kontot heta?");
                loggedInUser.accounts.Add(0.00m);
                loggedInUser.accountNames.Add(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Kontot " + loggedInUser.accountNames[loggedInUser.accountNames.Count - 1] + " har skapats.");
            }
            else
            {
                if (loggedInUser.accounts.Count < 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ogiltigt val. Du måste ha minst ett konto.");
                    Console.ResetColor();
                }
                else
                {
                    checkBalance(loggedInUser, "KONTON");
                    Console.WriteLine("Vilket konto vill du ta bort?");
                    while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 1) || (select > loggedInUser.accounts.Count))
                    {
                        Menu.ClearLine();
                    }
                    Console.Clear();
                    Console.WriteLine(loggedInUser.accountNames[select - 1] + " har tagits bort.");
                    loggedInUser.accounts.RemoveAt(select - 1);
                    loggedInUser.accountNames.RemoveAt(select - 1);
                    for (int i = select-1; i < loggedInUser.accounts.Count - 2; i++)
                    {
                        loggedInUser.accounts[i] = loggedInUser.accounts[i + 1];
                        loggedInUser.accountNames[i] = loggedInUser.accountNames[i + 1];
                    }
                }

            }
            Console.WriteLine();
            Console.WriteLine("Tryck Enter för att återvända.");
            Console.ReadLine();
        }
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }
    }
    public class User
    {
        public string userName;
        public string password;
        public string name;
        public string surName;
        public List<decimal> accounts = new List<decimal> { 10000.00m, 100000.00m };
        public List<string> accountNames = new List<string> { "Lönekonto", "Sparkonto" };

        public User(string userNameInput, string passwordInput, string nameInput, string surNameInput)
        {
            userName = userNameInput;
            password = passwordInput;
            name = nameInput;
            surName = surNameInput;
        }
    }
}
