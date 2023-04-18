using Cesar.Application.Services;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var serviceCesar = new CesarService();
            string option = "1";

            while(option == "1")
            {
                bool validateOption = false;

                while (!validateOption)
                {
                    Console.WriteLine("\n Escolha uma opção: \n 1- Criptografar\n 2- Decriptografar");
                    option = Console.ReadLine();
                    Console.Clear();
                    validateOption = serviceCesar.ValidateOption(option);
                }

                switch (option)
                {
                    case "1":
                        Console.WriteLine("\n Encrypt: " + serviceCesar.Encrypt());
                        break;
                    case "2":
                        Console.WriteLine("\n Decrypt: " + serviceCesar.Decrypt());
                        break;
                }

                validateOption = false;
                while (!validateOption)
                {
                    Console.WriteLine("\n Deseja continuar? \n 1- Sim \n 2- Não");
                    option = Console.ReadLine();
                    Console.Clear();
                    validateOption = serviceCesar.ValidateOption(option);
                }

            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.ReadKey();
        }
    }
}