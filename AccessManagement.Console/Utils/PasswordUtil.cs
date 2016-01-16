using System;
using System.Linq;
using System.Text;

namespace AccessManagement.Console.Utils
{
    class PasswordUtil
    {
        internal static string ProcessPassword(string passwordPrompt)
        {
            var password = new StringBuilder();
            var passwordLength = 0;

            System.Console.Write(passwordPrompt);

            var next = System.Console.ReadKey();
            while (next.Key != ConsoleKey.Enter)
            {
                passwordLength++;
                password.Append(next.KeyChar);
                System.Console.Write("\r" + passwordPrompt + new string(Enumerable.Repeat('*', passwordLength).ToArray()));
                next = System.Console.ReadKey();
            }

            return password.ToString();
        }
    }
}
