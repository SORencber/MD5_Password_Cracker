using System.Security.Cryptography;
using System.Text;

namespace PasswordCracker
{
    class Program
    {
        static void Main(string[] args)
        {

        Here:

            Console.Write("Enter MD5 string  : ");

            string HashedWord = "";

            HashedWord = Console.ReadLine().ToUpper();

            if (!string.IsNullOrEmpty(HashedWord))
            {
                Console.WriteLine(" MD5 string is valid");
            }
            else
            {
                Console.WriteLine("MD5 string is not valid");
                goto Here;
            }

            Console.WriteLine("Enter the File Name with extension : (passwordlist.txt)");

            string fileName = "";

            fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                Console.WriteLine("File is found ");
            }
            else
            {
                Console.WriteLine("File could not find");
                goto Here;
            }

            string line = "";
            int counter = 0;
            bool closeLoop = true;

            using (StreamReader file = new StreamReader(fileName))
            {

                while (closeLoop == true && (line = file.ReadLine()) != null)
                {
                    if (Md5HashedString(line) == HashedWord)
                    {

                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(line);
                        Console.ForegroundColor = ConsoleColor.Green;

                        Console.WriteLine("Cracked Password = " + line + "\n\r" + Md5HashedString(line));

                        Console.ResetColor();
                        Console.ReadKey();
                        file.Close();
                        closeLoop = false;
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                    counter++;
                    Console.Title = "Counter: " + counter.ToString();
                    Thread.Sleep(10);
                }
                file.Close();
                Console.ReadKey();

            }
        }
        public static string Md5HashedString(string line)
        {
            StringBuilder sb = new StringBuilder();
            MD5CryptoServiceProvider MD5Provider = new MD5CryptoServiceProvider();

            byte[] bytes = MD5Provider.ComputeHash(new UTF8Encoding().GetBytes(line));

            for (int x = 0; x < bytes.Length; x++)
            {
                sb.Append(bytes[x].ToString("X2"));

            }
            return sb.ToString();
        }
    }
}
