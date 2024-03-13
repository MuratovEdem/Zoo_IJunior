using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.Work();
        }
    }

    class Menu
    {
        private Zoo _zoo = new Zoo();

        public void Work()
        {
            const string CommandExit = "exit";
            List<Aviary> aviaries = _zoo.GetAviaries();

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine($"В зоопарке {aviaries.Count} вольера" +
                    "\nВведите номер вольера для просмотра" +
                    $"\nВведите {CommandExit} для выхода");

                int userValue;

                string userInput = Console.ReadLine();

                if (userInput == CommandExit)
                {
                    isRunning = false;
                }
                else
                {
                    if (int.TryParse(userInput, out userValue))
                    {
                        if (userValue > 0 && userValue <= _zoo.GetAviaries().Count)
                            Console.WriteLine(aviaries[userValue - 1]);
                        else
                            Console.WriteLine("Вольера с таким номером нет. Попробуйте снова");
                    }
                    else
                    {
                        Console.WriteLine("Введено не число. Попробуйте снова");
                    }
                }
            }
        }
    }

    class Zoo
    {
        private List<Aviary> _aviaries;

        public Zoo()
        {
            int minNumberAnimalsInAviary = 1;
            int maxNumberAnimalsInAviary = 10;

            _aviaries = new List<Aviary>()
            {
                new Aviary("Тигр", UserUtils.GenerateRandomNumber(minNumberAnimalsInAviary, maxNumberAnimalsInAviary)),
                new Aviary("Лев", UserUtils.GenerateRandomNumber(minNumberAnimalsInAviary, maxNumberAnimalsInAviary)),
                new Aviary("Кот", UserUtils.GenerateRandomNumber(minNumberAnimalsInAviary, maxNumberAnimalsInAviary)),
                new Aviary("Собака", UserUtils.GenerateRandomNumber(minNumberAnimalsInAviary, maxNumberAnimalsInAviary))
            };
        }

        public List<Aviary> GetAviaries()
        {
            return new List<Aviary>(_aviaries);
        }
    }

    class Aviary
    {
        private List<Animal> _animals = new List<Animal>();

        public Aviary(string nameAnimal, int numberAnimals)
        {
            FillListAmimals(nameAnimal, numberAnimals);
        }

        private void FillListAmimals(string nameAnimal, int numberAnimals)
        {
            AnimalCreator animalCreator = new AnimalCreator();

            for (int i = 0; i < numberAnimals; i++)
                _animals.Add(animalCreator.CreateAnimal(nameAnimal));
        }

        public override string ToString()
        {
            string result = "";

            foreach (Animal animal in _animals)
            {
                result += animal + "\n";
            }

            return $"В вольере сидят : {result}";
        }
    }

    class Animal
    {
        private string _name;
        private string _whatSay;
        private char _gender;

        public Animal(string name, string whatSay, char gender)
        {
            _name = name;
            _whatSay = whatSay;
            _gender = gender;
        }

        public override string ToString()
        {
            return $"{_name} - {_whatSay} - {_gender}";
        }
    }

    class AnimalCreator
    {
        private Dictionary<string, string> _animals;
        private List<char> _genders;

        public AnimalCreator()
        {
            _animals = new Dictionary<string, string>()
            {
                { "Тигр", "ррр" },
                { "Лев", "роар" },
                { "Кот", "мяу" },
                { "Собака", "гав" }
            };

            _genders = new List<char>() { 'm', 'f' };
        }

        public Animal CreateAnimal(string name)
        {
            return new Animal(name, _animals[name], GetRandomGender());
        }

        private char GetRandomGender()
        {
            return _genders[UserUtils.GenerateRandomNumber(_genders.Count)];
        }
    }

    class UserUtils
    {
        private static Random s_random = new Random();

        public static int GenerateRandomNumber(int maxValue)
        {
            return s_random.Next(maxValue);
        }

        public static int GenerateRandomNumber(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }
}