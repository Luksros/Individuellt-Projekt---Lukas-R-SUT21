using System;

namespace Individuellt_Projekt___Lukas_R_SUT21
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu MainMenu = new Menu();
            MainMenu.PrintMenu("Lukas");
            Console.ReadLine();
        }
    }
    class Menu
    {
        string[] menuChoices = { "1. Se dina konton och saldo", "2. Överföring mellan konton", "3. Ta ut pengar", "4. Logga ut" };

        public void PrintMenu(string tempUserName)
        {
            Console.WriteLine("Välkommen, " + tempUserName);
            Console.WriteLine();
            for (int i = 0; i < menuChoices.Length; i++)
            {
                Console.WriteLine(menuChoices[i]);
            }
        }

        public static void MenuSelection()
        {
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
                        //return to login screen
                        break;
                    }
            }             
            }
    }
    class User
    {

    }
}
