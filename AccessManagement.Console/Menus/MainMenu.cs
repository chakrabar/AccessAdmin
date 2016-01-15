using System;

namespace AccessManagement.Console.Menus
{
    static class MainMenu
    {
        internal static void ShowStartupMessage(string menuText = "Main Menu")
        {
            System.Console.BackgroundColor = ConsoleColor.DarkBlue;
            System.Console.ForegroundColor = ConsoleColor.Gray;
            System.Console.WriteLine("##############################################################################");
            System.Console.WriteLine("                        Access Management demo CONSOLE                        ");
            System.Console.WriteLine("##############################################################################");//+ GetNewLine(2)
            
            System.Console.ForegroundColor = ConsoleColor.Blue;
            System.Console.Write(menuText);
            var leftPadding = 78 - menuText.Length;

            string msg = string.Empty;
            if (!Program.IsUserLoggedIn)
            {
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                msg = "Guest";
            }
            else
            {
                System.Console.ForegroundColor = System.ConsoleColor.Green;
                msg = "Logged in as : " + Program.UserName + (Program.IsUserManager ? " [Manager]" : "");
            }
            msg = msg.PadLeft(leftPadding) + GetNewLine(2);
            System.Console.Write(msg);
            System.Console.ResetColor();
        }

        internal static string ShowMainMenu()
        {
            ShowStartupMessage();
            System.Console.Write("You can do a [1] Initial data load [2] Daily report [3] Access violation notification [4] Login");
            System.Console.Write("Press `Enter` without input to exit." + GetNewLine());
            var options = string.Format("\tPress 'I' for initial load\n\tPress 'R' for daily report\n\tPress 'N' for access notification\n\t{0}\n\tEnter choice (I/R/N/{1}) : ", Program.IsUserLoggedIn ? "Press 'O' for logout" : "Press 'L' for login", Program.IsUserLoggedIn ? "O" : "L");
            System.Console.Write(options);
            var userInput = System.Console.ReadLine();
            System.Console.WriteLine(GetNewLine());
            return userInput.Trim().ToUpper();
        }

        internal static string GetNewLine(int count = 1)
        {
            if (count < 0)
                count = 1;
            var outputString = string.Empty;
            for (int i = 0; i < count; i++)
            {
                outputString += Environment.NewLine;
            }
            return outputString;
        }
    }
}
