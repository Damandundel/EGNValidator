using System;


namespace EGNValidatorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter EGN: ");
            string egn = Console.ReadLine();

            EGNValidator validator = new EGNValidator();

            if (validator.Validate(egn))
            {
                Person person = validator.ExtractPersonInfo(egn, name);
                Console.WriteLine("\nEGN is valid. Extracted info:");
                Console.WriteLine(person);
            }
            else
            {
                Console.WriteLine("Invalid EGN.");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}