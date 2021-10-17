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
            int accountIndex = 10; //This int is set to 10 because I couldn´t leave it unassigned, and I needed a number that wouldn´t correspond with an index in User array if login fails.
            int loginTries = 0; //Will keep track of how many times a user has tried to log in. 
            bool loggedIn = false;
            while (!loggedIn && loginTries < 3) //As long as user has failed less than three times, they get to try again.
            {
                loggedIn = Menu.Login(AllUsers, ref accountIndex, ref loginTries); //Menu.Login lets the user write username and password, and returns true or false depending on the login attempt being successful or not.
                                                                                   //I am using ref parameters so I can change the values of the arguments and use those values outside the scope of the method.
                while (loggedIn) //This loop will keep typing out the main menu, and only triggers when "loggedIn" is true.
                {
                    Menu.MainMenu(AllUsers[accountIndex], ref loggedIn);
                }
            }
            if (loginTries > 2)//Once the user has failed three times, they can´t do anything else with the program
            {
                Menu.WriteRed("Du har misslyckats att logga in för många gånger. Programmet är nu spärrat.", 1);
                Console.WriteLine("Tryck Enter för att avsluta.");
                Console.Beep(247, 300);
                Console.Beep(174, 300);
            }
            Console.ReadLine();
        }
    }
    public static class Menu
    {
        public static void MainMenu(User LoggedInUser, ref bool tempLoggedIn)
        {
            PrintMenu(LoggedInUser.name, LoggedInUser.surName);
            int selector;
            while (!int.TryParse(Console.ReadLine(), out selector))
            {

            }
            switch (selector)
            {
                case 1:
                    {
                        CheckBalance(LoggedInUser);
                        Return();
                        break;
                    }
                case 2:
                    {
                        ManageAccounts(LoggedInUser);
                        break;
                    }
                case 3:
                    {
                        TransferFunds(LoggedInUser);
                        break;
                    }
                case 4:
                    {
                        WithdrawFunds(LoggedInUser);
                        break;
                    }
                case 5:
                    {
                        AddFunds(LoggedInUser);
                        break;
                    }
                case 6:
                    {
                        LogOutBeep();
                        tempLoggedIn = false;
                        break;
                    }
            }
        }

        //LOGIN - Checks users input against the username and password fields of all the users, and returns true if a match is made
        public static bool Login(User[] AccountList, ref int indexTarget, ref int loginTryCounter)
        {
            for (int i = 0; i < 3; i++)
            {
                LoginPrint();

                //User inputs their username and password
                Console.SetCursorPosition(14, 1);
                string usernameInput = Console.ReadLine();
                Console.SetCursorPosition(10, 2);
                string passInput = Console.ReadLine();

                //If matches are found for both the username and password fields on the same index of the user array, true is returned and the number of tries is reset
                for (int k = 0; k < AccountList.Length; k++)
                {
                    if ((AccountList[k].userName == usernameInput) && (AccountList[k].password == passInput))
                    {
                        indexTarget = k;
                        loginTryCounter = 0;
                        Console.Clear();
                        LogInBeep();
                        return true;
                    }
                }
                loginTryCounter++;
            }          
            return false;          
        }

        //CHECKBALANCE - Simply displays all of the users accounts and their available funds
        public static void CheckBalance(User loggedInUser, string print = "KONTOÖVERSIKT")
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

        //TRANSFERFUNDS - Used to move funds between the different accounts that a user has available
        public static void TransferFunds(User loggedInUser)
        {
            Console.Clear();
            if (loggedInUser.accounts.Count > 1)
            {
                CheckBalance(loggedInUser, "KONTON");
                Console.Write("Välj konto att flytta pengar från: ");
                int select;

                //This while-loop simultaneously protects against exceptions, and also makes sure that the input can only match the available account numbers
                while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 1) || (select > loggedInUser.accounts.Count))
                {
                    Menu.ClearLine();
                    WriteRed("Ogiltig input. ", 2);
                    Console.Write("Välj konto att flytta pengar från: ");
                }

                CheckBalance(loggedInUser, ("Föra över från " + loggedInUser.accountNames[select - 1] + " till vilket konto?"));

                int select2;

                //This while is the same as the one above, but also makes sure that the same account isn't selected twice
                while ((!int.TryParse(Console.ReadLine(), out select2)) || (select2 < 1) || (select2 > loggedInUser.accounts.Count) || (select2 == select))
                {
                    Menu.ClearLine();   
                    WriteRed("Ogiltig input. ", 2);
                    Console.Write("Välj konto att flytta pengar till: ");
                }

                Console.WriteLine("Välj summa att föra över: ");
                decimal transferAmt;
                while (!Decimal.TryParse(Console.ReadLine(), out transferAmt) || (transferAmt > loggedInUser.accounts[select - 1]))
                {
                    Menu.ClearLine();
                    WriteRed("Ogiltig input. ", 2);
                    Console.Write("Välj summa att föra över: ");
                }

                //-1 is used in the brackets because the indexes start on 0, and not 1, but it wouldn't make sense for the users accounts to start on the number 0
                loggedInUser.accounts[select - 1] = loggedInUser.accounts[select - 1] - transferAmt;
                loggedInUser.accounts[select2 - 1] = loggedInUser.accounts[select2 - 1] + transferAmt;
                CheckBalance(loggedInUser);
                WriteGreen((transferAmt + " Kr fördes över från " + loggedInUser.accountNames[select - 1] + " till " + loggedInUser.accountNames[select2 - 1]), 1);
                Return();
            }
            else
            {
                WriteRed("Du har bara ett konto. Överföring inte möjlig.", 1);
                Return();
            }
            
        }

        //WithdrawFunds - Does what it's called, really. Pretty much uses the same logic as WithdrawFunds.
        public static void WithdrawFunds(User loggedInUser)
        {
            Console.Clear();
            CheckBalance(loggedInUser, "KONTON");

            Console.WriteLine("Välj konto att ta ut pengar ifrån: ");
            int select;
            while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 1) || (select > loggedInUser.accounts.Count))
            {
                Menu.ClearLine();
                WriteRed("Ogiltig input. ", 2);
                Console.Write("Välj konto att ta ut pengar ifrån: ");
            }

            ClearLine();
            Console.Write("Skriv in hur mycket du vill ta ut: ");
            decimal transferAmt;
            while (!Decimal.TryParse(Console.ReadLine(), out transferAmt) || (transferAmt > loggedInUser.accounts[select - 1]))
            {
                Menu.ClearLine();
                WriteRed("Ogiltig input. ", 2);
                Console.Write("Skriv in hur mycket du vill ta ut: ");
            }

            Console.Clear();
            Console.WriteLine("Vänligen bekräfta uttag av beloppet {0} Kr med ditt lösenord: ", transferAmt);
            string tempPassword = Console.ReadLine();

            //If users input matches password field stored in their User-object, the money is deducted from the account
            if (tempPassword == loggedInUser.password)
            {
                loggedInUser.accounts[select - 1] = loggedInUser.accounts[select - 1] - transferAmt;
                CheckBalance(loggedInUser);
                WriteGreen((transferAmt + " Kr togs ut från " + loggedInUser.accountNames[select - 1]), 1);
            }
            else
            {
                WriteRed("Fel lösenord. ", 1);
            }       
            Return();
        }

        //ADDFUNDS - Like WithdrawFunds, but adds money instead.
        public static void AddFunds(User loggedInUser)
        {
            Console.Clear();
            CheckBalance(loggedInUser, "KONTON");
            Console.WriteLine("Välj konto att sätta in pengar på: ");
            int select;
            while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 1) || (select > loggedInUser.accounts.Count))
            {
                Menu.ClearLine();
            }
            ClearLine();
            Console.WriteLine("Hur mycket vill du sätta in?");
            decimal transferAmt;
            while (!Decimal.TryParse(Console.ReadLine(), out transferAmt))
            {
                Menu.ClearLine();
            }
            loggedInUser.accounts[select - 1] = loggedInUser.accounts[select - 1] + transferAmt;
            CheckBalance(loggedInUser);
            WriteGreen((transferAmt + " Kr sattes in på " + loggedInUser.accountNames[select - 1]), 1);
            Return();
        }


        //MANAGEACCOUNTS - Allows the user to create and remove accounts, the latter of which has a few safety measures
        public static void ManageAccounts (User loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("Vill du lägga till eller ta bort ett konto?");
            Console.WriteLine();
            Console.WriteLine("[1] Lägga till");
            Console.WriteLine("[2] Ta bort");
            int select;
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
                WriteGreen("Kontot " + loggedInUser.accountNames[loggedInUser.accountNames.Count - 1] + " har skapats.", 1);
            }
            else
            {
                if (loggedInUser.accounts.Count < 2)
                {
                    Console.Clear();
                    WriteRed("Ogiltigt val. Du måste ha minst ett konto.", 1);
                }
                else
                {
                    CheckBalance(loggedInUser, "KONTON");
                    Console.WriteLine("Vilket konto vill du ta bort?");
                    while ((!int.TryParse(Console.ReadLine(), out select)) || (select < 1) || (select > loggedInUser.accounts.Count))
                    {
                        Menu.ClearLine();
                    }

                    Console.Clear();

                    //This if-clause makes sure that there are no remaining funds on the account that the user wants to delete, otherwise it doesn't go through with it.
                    if (loggedInUser.accounts[select - 1] == 0)
                    {
                        //If there are no remaining funds, user still has to enter their password to through with the deletion
                        Console.WriteLine("Vänligen bekräfta borttagning av {0} med ditt lösenord: ", loggedInUser.accountNames[select - 1]);
                        string tempPassword = Console.ReadLine();

                        //If users input matches password field stored in their User-object, the account is deleted
                        if (tempPassword == loggedInUser.password)
                        {
                            Console.Clear();
                            WriteGreen(loggedInUser.accountNames[select - 1] + " har tagits bort.", 1);
                            loggedInUser.accounts.RemoveAt(select - 1);
                            loggedInUser.accountNames.RemoveAt(select - 1);

                            //This for loop starts at the index of the deleted account, and copies the funds and name of the index above, to make sure that there are no null indexes
                            for (int i = select - 1; i < loggedInUser.accounts.Count - 2; i++)
                            {
                                loggedInUser.accounts[i] = loggedInUser.accounts[i + 1];
                                loggedInUser.accountNames[i] = loggedInUser.accountNames[i + 1];
                            }
                        }
                        else
                        {
                            WriteRed("Fel lösenord. ", 1);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Vänligen flytta kontots återstående saldo innan du tar bort det.");
                    }               
                }
            }
            Return();
        }

        public static void PrintMenu(string tempUserName, string tempUserSurName)
        {
            Console.Clear();

            string[] menuChoices = { ("Välkommen, "+ tempUserName + " " + tempUserSurName), "Gör menyval genom att skriva motsvarande siffra och tryck Enter.", " ", "[1] Se dina konton och saldo", "[2] Hantera konton", "[3] Överföring mellan konton", "[4] Ta ut pengar", "[5] Sätta in pengar", "[6] Logga ut" };
            for (int i = 0; i < menuChoices.Length; i++)
            {
                Console.WriteLine(menuChoices[i]);
            }
        }

        //LOGINBEEP - Plays a nice little ascending C Major chord arpeggio
        public static void LogInBeep()
        {
            Console.Write("Loggar in ");
            Console.Beep(261, 150);
            Console.Write(". ");
            Console.Beep(329, 150);
            Console.Write(". ");
            Console.Beep(392, 150);
            Console.Write(". ");
            Console.Beep(522, 250);
        }

        //LOGOUTBEEP - The same as above, but in reverse
        public static void LogOutBeep()
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
        public static void LoginPrint()
        {
            Console.Clear();
            Console.WriteLine("Vänligen fyll i användarnamn och lösenord.");
            Console.WriteLine("Användarnamn: ");
            Console.WriteLine("Lösenord: ");
        }
        public static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.Write(new String(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop - 1);
        }

        //WriteGreen and WriteRed are just used to have a short command available for writing in those colors.
        //The parameter "select" is used to decice whether I want to use the Write or WriteLine method.
        //Since it is never used by the user, only "behind the scenes", I figured it didn´t have to be too self explanatory.
        public static void WriteGreen(string text, int select)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            if (select == 1)
            {
                Console.WriteLine(text);
            }
            else if(select == 2)
            {
                Console.Write(text);
            }           
            Console.ResetColor();
        }
        public static void WriteRed(string text, int select)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (select == 1)
            {
                Console.WriteLine(text);
            }
            else if (select == 2)
            {
                Console.Write(text);
            }
            Console.ResetColor();
        }
        public static void Return()
        {
            Console.WriteLine();
            Console.WriteLine("Tryck Enter för att återvända.");
            Console.ReadLine();
        }
    }

    //USER - a blueprint for making User-objects, which store all the useful information that the program needs. 
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
