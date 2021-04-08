using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pokemon
{
    class Program
    {

        static Pokemon[] team = new Pokemon[3];
        static Pokemon[] enemyTeam = new Pokemon[3];

        static Pokemon[] fighters = new Pokemon[2];

        static Random enemySel = new Random();
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "Pokemon.csv");
            var fileContents = ReadPokemon(fileName);



            Console.WriteLine("Please type in three pokemon.");

            string[] sel = new string[3];
            sel[0] = Console.ReadLine();
            sel[1] = Console.ReadLine();
            sel[2] = Console.ReadLine();



            for (int i = 0; i <= team.Length-1; i++)
            {

                foreach (Pokemon pokemon in fileContents)
                {

                    if (pokemon.Name == sel[i])
                    {
                        team[i] = pokemon;
                        Console.WriteLine(pokemon.Name + " added!");
                        
                    }
                }
            }

            for (int i = 0; i <= enemyTeam.Length-1; i++)
                foreach (Pokemon pokemon in fileContents)
                {
                    int foe = enemySel.Next(1, 150);
                    if (pokemon.Number == foe)
                    {
                        enemyTeam[i] = pokemon;
                        Console.WriteLine(enemyTeam[i].Name + " added by opponent.");
                    }
                }

            Console.WriteLine("Your team is: " + team[0].Name + ", " + team[1].Name + ", " + team[2].Name);
            Console.WriteLine("The opponent's team is: " + enemyTeam[0].Name + ", " + enemyTeam[1].Name + ", " + enemyTeam[2].Name);

            chooseFighters();
            Battle();
        }

        public static void teamSelection()
        {

        }

        public static void chooseFighters()
        {

            Console.WriteLine("Which pokemon will you send out?");

            var choice = Console.ReadLine();
            for (int i = 0; i <= team.Length-1; i++)
            {
                if (choice == team[i].Name)
                {
                    fighters[0] = team[i];
                    Console.WriteLine("Sent out " + fighters[0].Name);
                }
            }

            int foeChoice = enemySel.Next(0, enemyTeam.Length);
            {
                fighters[1] = enemyTeam[foeChoice];
                Console.WriteLine("Opponent sent out " + fighters[1].Name);
            }


        }

        public static void Battle()
        {
            int score = 0;
            int[] stats = { fighters[0].Speed,
                            fighters[0].Attack,
                            fighters[0].Defense,
                            fighters[0].SpAttack,
                            fighters[0].SpDefense
                           };
            int foeScore = 0;
            int[] foeStats = { fighters[1].Speed,
                            fighters[1].Attack,
                            fighters[1].Defense,
                            fighters[1].SpAttack,
                            fighters[1].SpDefense
                           };
            string[] statNames = { "Speed",
                                   "Attack",
                                   "Defense",
                                   "Special Attack",
                                   "Special Defense"
                                  };



            Console.WriteLine("The two pokemon battle!");

            for (int i = 0; i <= stats.Length-1; i++)
            {
                if (stats[i] > foeStats[i])
                {
                    score++;
                    Console.WriteLine("Your " + fighters[0].Name + " scores a hit with its superior " + statNames[i] + " stat!");
                }
                else if (foeStats[i] > stats[i])
                {
                    foeScore++;
                    Console.WriteLine("The opponent's " + fighters[1].Name + " scores a hit with its superior " + statNames[i] + " stat!");
                }
                else
                {
                    Console.WriteLine("The Pokemon are equally matched in " + statNames[i] + "!");
                }
            }

            if (score > foeScore)
            {
                Console.WriteLine("Your " + fighters[0].Name + " defeated " + fighters[1].Name +"!");
            }
            else if (score < foeScore)
            {
                Console.WriteLine("The opponent's " + fighters[1].Name + " defeated " + fighters[0].Name + "!");
            }
            else
            {
                Console.WriteLine("After an epic clash both pokemon faint!");
            }
            

        }

            public static string ReadFile(string fileName)
            {
                using (var reader = new StreamReader(fileName))
                {
                    return reader.ReadToEnd();
                }
            }

            public static List<Pokemon> ReadPokemon(string fileName)
            {
                var pokedex = new List<Pokemon>();
                using (var reader = new StreamReader(fileName))
                {
                    string line = "";

                    while ((line = reader.ReadLine()) != null)
                    {
                        var pokemon = new Pokemon();

                        string[] values = line.Split(',');

                        pokemon.Name = values[1];
                        pokemon.Type1 = values[2];
                        pokemon.Type2 = values[3];
                        pokemon.Legendary = values[12];
                        pokemon.Name = values[1];
                        pokemon.Type1 = values[2];
                        pokemon.Type2 = values[3];
                        pokemon.Legendary = values[12];

                        int parseInt;

                        if (int.TryParse(values[0], out parseInt))
                        {
                            pokemon.Number = parseInt;
                        }
                        if (int.TryParse(values[5], out parseInt))
                        {
                            pokemon.Hp = parseInt;
                        }
                        if (int.TryParse(values[6], out parseInt))
                        {
                            pokemon.Attack = parseInt;
                        }
                        if (int.TryParse(values[7], out parseInt))
                        {
                            pokemon.Defense = parseInt;
                        }
                        if (int.TryParse(values[8], out parseInt))
                        {
                            pokemon.SpAttack = parseInt;
                        }
                        if (int.TryParse(values[9], out parseInt))
                        {
                            pokemon.SpDefense = parseInt;
                        }
                        if (int.TryParse(values[10], out parseInt))
                        {
                            pokemon.Speed = parseInt;
                        }
                        if (int.TryParse(values[11], out parseInt))
                        {
                            pokemon.Generation = parseInt;
                        }

                        pokedex.Add(pokemon);
                    }
                }
                return pokedex;
            }
        }
    }

