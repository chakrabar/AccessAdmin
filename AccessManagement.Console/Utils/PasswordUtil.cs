using System;
using System.Linq;
using System.Text;

namespace AccessManagement.Console.Utils
{
    class PasswordUtil
    {
        internal static string ProcessPassword(string passwordPrompt = "Enter password : ", char hideChar = '*')
        {
            var password = new StringBuilder();
            var passwordLength = 0;

            System.Console.Write(passwordPrompt);

            var next = System.Console.ReadKey();
            while (next.Key != ConsoleKey.Enter)
            {
                if (next.Key == ConsoleKey.LeftArrow || next.Key == ConsoleKey.RightArrow)
                    continue;
                if (next.Key == ConsoleKey.Backspace)
                {
                    if (passwordLength == 0)
                        continue;
                    passwordLength--;
                    password.Remove(password.Length - 1, 1);
                }
                else
                {
                    passwordLength++;
                    password.Append(next.KeyChar);
                }
                System.Console.Write("\r" + passwordPrompt + new string(hideChar, passwordLength));
                next = System.Console.ReadKey();
            }

            return password.ToString();
        }
    }
}
