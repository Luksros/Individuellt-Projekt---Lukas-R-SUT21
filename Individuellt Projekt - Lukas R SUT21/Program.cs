using System;

namespace Individuellt_Projekt___Lukas_R_SUT21
{
    class Program
    {
        static void Main(string[] args)
        {
            //Setup
            Account[] AllAccounts = new Account[5];
            Account Lukas = new Account ("LuksRos", "BadPassword123", "Lukas", "Rose");
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
                if (loggedIn)
                {
                    Menu.MainMenu(AllAccounts[accountIndex], ref loggedIn);
                }
            }
            if (loginTries > 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Du har misslyckats att logga in för många gånger. Programmet är nu spärrat.");
            }

            

            Console.ReadLine();
        }
    }
    public static class Menu
    {
        public static void loginPrint()
        {
            Console.Clear();
            string[] loginScreen = { "Vänligen fyll i användarnamn och lösenord.", " ", "Användarnamn: ", "Lösenord: " };
            for (int j = 0; j < loginScreen.Length; j++)
            {
                Console.WriteLine(loginScreen[j]);
            }
        }
        

        public static bool Login (Account[] AccountList, ref int indexTarget, ref int loginCounter)
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
                        Console.Clear();
                        indexTarget = k;
                        loginCounter = 0;
                        return true;
                    }
                }

                loginCounter++;
            }
            return false;
            
        }

        public static void PrintMenu(string tempUserName, string tempUserSurName)
        {           
            Console.WriteLine("Välkommen, {0} {1}", tempUserName, tempUserSurName);
            Console.WriteLine();

            string[] menuChoices = { "1. Se dina konton och saldo", "2. Överföring mellan konton", "3. Ta ut pengar", "4. Logga ut" };
            for (int i = 0; i < menuChoices.Length; i++)
            {
                Console.WriteLine(menuChoices[i]);
            }
        }

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
                        //Method for account overview
                        break;
                    }
                case 2:
                    {
                        //Method for transferring funds between accounts
                        break;
                    }
                case 3:
                    {
                        //Method for money withdrawals
                        break;
                    }
                case 4:
                    {
                        tempLoggedIn = false;
                        break;
                    }
            }             
            }
    }
    public class Account
    {
        public string userName;
        public string password;
        public string name;
        public string surName;
        public decimal[] accounts = new decimal[2];
        public string[] accountNames = new string[2];
        
        public Account(string userNameInput, string passwordInput, string nameInput, string surNameInput)
        {
            userName = userNameInput;
            password = passwordInput;
            name = nameInput;
            surName = surNameInput;
        }
    }
}
