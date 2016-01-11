
namespace AccessManagement.Console.Menus
{
    static class ManagerMenu
    {
        internal static string ShowManagerMenu(string username, string department)
        {
            System.Console.Clear();
            MainMenu.ShowStartupMessage("Main Menu > Manager");
            System.Console.WriteLine("[Manager] Name : [{0}] Department [{1}]", username, department);
            System.Console.WriteLine("");
            System.Console.WriteLine("Enter choice [1] to see logs [2] to see access points [3] to change permissions");
            System.Console.Write("Press `Enter` without input to go back to Main Menu." + MainMenu.GetNewLine());
            System.Console.Write("\n\tEnter choice (1/2/3) : ");
            var userInput = System.Console.ReadLine();
            System.Console.WriteLine(MainMenu.GetNewLine());
            return userInput.Trim().ToUpper();
        }
    }
}
