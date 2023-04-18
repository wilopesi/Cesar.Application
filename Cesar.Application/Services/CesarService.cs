using System.Text.RegularExpressions;


namespace Cesar.Application.Services
{
    public class CesarService
    {
        private static string _alphabet = "";

        private static string _deslocated = "";

        private static string _numbers = "";

        private static string _deslocatedNumbers = "";

        public CesarService()
        {
            _alphabet = GenerateAlfhabet();
            _deslocated = GenerateDeslocatedAlphabet();
            _numbers = GenerateNumbers();
            _deslocatedNumbers = GenerateDeslocatedNumbers();
        }

        public bool ValidateOption(string optionSelected)
        {
            switch (optionSelected)
            {
                case "1":
                    return true;
                case "2":
                    return true;
                default:
                    Console.WriteLine("Digite uma opção valida!");
                    return false;
            }
        }

        public string Encrypt()
        {
            bool messageValidated = false;
            var input = "";

            while (!messageValidated)
            {
                Console.WriteLine("\n #Criptografar");
                Console.WriteLine("\n Digite uma mensagem:");
                input = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"\n Message: {input}");
                messageValidated = ValidateInput(input, 1);
            }

            string[] words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = EncryptInput(words[i]);
            }

            return string.Join("#X20", words);
        }

        private string EncryptInput(string input)
        {
            var newInput = "";
            var arrayWord = input.ToCharArray();

            var deslocatedArray = _deslocated.ToCharArray();
            var alphabetArray = _alphabet.ToCharArray();

            var deslocatedNumbersArray = _deslocatedNumbers.ToCharArray();
            var numbersArray = _numbers.ToCharArray();

            foreach (var item in arrayWord)
            {
                if (Regex.IsMatch(input, @"^[0-9]+$"))
                {
                    int numberIndex = Array.FindIndex(numbersArray, number => number == Char.ToUpper(item));
                    newInput += deslocatedNumbersArray[numberIndex];
                }
                else
                {
                    int alphabetIndex = Array.FindIndex(alphabetArray, letter => letter == Char.ToUpper(item));
                    newInput += deslocatedArray[alphabetIndex];
                }
            }

            return newInput;
        }

        private string DecryptInput(string input)
        {
            var newInput = "";
            var arrayWord = input.ToCharArray();

            var deslocatedArray = _deslocated.ToCharArray();
            var alphabetArray = _alphabet.ToCharArray();


            var deslocatedNumbersArray = _deslocatedNumbers.ToCharArray();
            var numbersArray = _numbers.ToCharArray();

            foreach (var item in arrayWord)
            {
                if (Regex.IsMatch(input, @"^[0-9]+$"))
                {
                    int deslocatedNumberIndex = Array.FindIndex(deslocatedNumbersArray, number => number == Char.ToUpper(item));
                    newInput += numbersArray[deslocatedNumberIndex];
                }
                else
                {
                    int deslocatedIndex = Array.FindIndex(deslocatedArray, letter => letter == Char.ToUpper(item));
                    newInput += alphabetArray[deslocatedIndex];
                }
            }

            return newInput;
        }

        private bool ValidateInput(string input, int type)
        {

            bool justLetterAndNumbers = Regex.IsMatch(input, @"^[a-zA-Z0-9 ]+$");
            bool repeatedNumberCharacter = Regex.IsMatch(input, @"\b\d*(\d)\1+\d*\b");

            if (type == 1)
            {
                bool containExpressions = Regex.IsMatch(input, @"[^\u0000-\u007F]+");

                if (repeatedNumberCharacter)
                {
                    Console.WriteLine("A string contém repetições de dígitos.");
                    return false;
                }

                if (justLetterAndNumbers && !containExpressions)
                    return true;
                else
                {
                    Console.WriteLine("A string contém caracteres especiais ou assentos!");
                    return false;
                }
            }
            else
            {
                if (repeatedNumberCharacter)
                {
                    Console.WriteLine("A string contém repetições de dígitos ou letras seguidos.");
                    return false;
                }

                return true;
            }
        }

        public string Decrypt()
        {
            bool messageValidated = false;
            var input = "";

            while (!messageValidated)
            {
                Console.WriteLine("\n #Desincriptografar");
                Console.WriteLine("\n Digite uma mensagem:");
                input = Console.ReadLine();

                Console.Clear();
                Console.WriteLine($"\n Message: {input}");
                messageValidated = ValidateInput(input, 2);
            }

            string[] words;
            if (input.Contains("#X20"))
                words = input.Split("#X20");
            else
                words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                words[i] = DecryptInput(words[i]);
            }

            var joined = string.Join("#X20", words);
            return joined.Replace("#X20", " ");
        }

        private string GenerateAlfhabet()
        {
            string alphabet = "";
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                alphabet += letter;
            }
            return alphabet;
        }

        private string GenerateDeslocatedAlphabet()
        {
            string deslocated = "";

            var arrayAlphabet = _alphabet.ToCharArray();

            for (var i = 0; i <= arrayAlphabet.Length; i++)
            {
                i = (i == 23) ? -3 : i;
                deslocated += arrayAlphabet[i + 3];

                if (deslocated.Length == _alphabet.Length)
                    break;
            }
            return deslocated;
        }


        private string GenerateNumbers()
        {
            string numbers = "";
            for (char number = '0'; number <= '9'; number++)
            {
                numbers += number;
            }
            return numbers;
        }

        private string GenerateDeslocatedNumbers()
        {
            string deslocated = "";

            var arrayNumbers = _numbers.ToCharArray();

            for (var i = 0; i <= arrayNumbers.Length; i++)
            {
                i = (i == 7) ? -3 : i;
                deslocated += arrayNumbers[i + 3];

                if (deslocated.Length == _numbers.Length)
                    break;
            }
            return deslocated;
        }

    }
}
