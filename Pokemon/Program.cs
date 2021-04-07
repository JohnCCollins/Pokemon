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
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "Pokemon.csv");
            var fileContents = ReadPokemon(fileName);

            Pokemon[] team = new Pokemon[3];
            Pokemon[] enemyTeam = new Pokemon[3];

            Console.WriteLine("Please type in three pokemon.");

            string[] sel = new string[3];
            sel[0] = Console.ReadLine();
            sel[1] = Console.ReadLine();
            sel[2] = Console.ReadLine();

            Random enemySel = new Random();

            for (int i = 0; i < sel.Length; i++)
            {
                int foe = enemySel.Next(1, 150);
                foreach (Pokemon pokemon in fileContents)
                {
                    if (pokemon.Number == foe)
                    {
                        enemyTeam[i] = pokemon;
                        Console.WriteLine(pokemon.Name + " added by opponent.");
                    }
                    if (pokemon.Name == sel[i])
                    {
                        Console.WriteLine(pokemon.Name + " added!");
                        team[i] = pokemon;
                    }
                }
            }

            Console.WriteLine("Your team is: " + team[0].Name + ", " + team[1].Name + ", " + team[2].Name);
            Console.WriteLine("The opponent's team is: " + enemyTeam[0].Name + ", " + enemyTeam[1].Name + ", " + enemyTeam[2].Name);

            Console.WriteLine("Choose which pokemon you will use first.");
            Console.WriteLine("Press 1 to send out " + team[0].Name);
            Console.WriteLine("Press 2 to send out " + team[1].Name);
            Console.WriteLine("Press 3 to send out " + team[2].Name);
            var choice = Console.ReadKey();
            Pokemon fighter = new Pokemon();

            while (choice.Key != ConsoleKey.D1 && choice.Key != ConsoleKey.D2 && choice.Key != ConsoleKey.D3)
            {
                if (choice.Key == ConsoleKey.D1)
                {
                    fighter = team[0];
                    Console.WriteLine();
                    Console.WriteLine("Sent out " + team[0].Name);
                }
                else if (choice.Key == ConsoleKey.D2)
                {
                    fighter = team[1];
                    Console.WriteLine();
                    Console.WriteLine("Sent out " + team[1].Name);
                }
                else if (choice.Key == ConsoleKey.D3)
                {
                    fighter = team[2];
                    Console.WriteLine();
                    Console.WriteLine("Sent out " + team[2].Name);
                }
            }

            int foeChoice = enemySel.Next(1, enemyTeam.Length);
            Pokemon foeFighter= new Pokemon();
            if (foeChoice == 1)
            {
                foeFighter = enemyTeam[0];
                Console.WriteLine("The opponent sends out " + enemyTeam[0].Name);
            }
            else if (foeChoice == 2)
            {
                foeFighter = enemyTeam[1];
                Console.WriteLine("The opponent sends out " + enemyTeam[1].Name);
            }
            else if (foeChoice == 3)
            {
                foeFighter = enemyTeam[1];
                Console.WriteLine("The opponent sends out " + enemyTeam[1].Name);
            }

            Console.WriteLine("The two pokemon battle!");

            int fighterScore = 0;
            int foeScore = 0;

            //speed
            if (fighter.Speed >= foeFighter.Speed)
            {
                fighterScore++;
                Console.WriteLine(fighter.Name + " strikes first!");
            }
            else
            {
                foeScore++;
                Console.WriteLine(foeFighter.Name + " strikes first!");
            }

            //attack
            if (fighter.Attack >= foeFighter.Attack)
            {
                fighterScore++;
                Console.WriteLine(fighter.Name + " deals a heavy blow!");
            }
            else
            {
                foeScore++;
                Console.WriteLine(foeFighter.Name + " deals a heavy blow!");
            }

            //defense
            if (fighter.Defense >= foeFighter.Defense)
            {
                fighterScore++;
                Console.WriteLine(fighter.Name + " blocks an attack and hits back even harder!");
            }
            else
            {
                foeScore++;
                Console.WriteLine(foeFighter.Name + " blocks an attack and hits back even harder!");
            }

            //spAttack
            if (fighter.SpAttack >= foeFighter.SpAttack)
            {
                fighterScore++;
                Console.WriteLine(fighter.Name + " blasts it's opponent!");
            }
            else
            {
                foeScore++;
                Console.WriteLine(foeFighter.Name + " blasts it's opponent!");
            }

            //spDefense
            if (fighter.SpDefense >= foeFighter.SpDefense)
            {
                fighterScore++;
                Console.WriteLine(fighter.Name + " unleashes its opponent's energy back at it!");
            }
            else
            {
                foeScore++;
                Console.WriteLine(foeFighter.Name + " unleashes its opponent's energy back at it!");
            }

            if(fighterScore > foeScore)
            {
                Console.WriteLine(fighter.Name + " defeated " + foeFighter.Name + "!");
            }
            else
            {
                Console.WriteLine(foeFighter.Name + " defeated " + fighter.Name + "!");
            }
            Console.ReadKey();

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
