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
        static Pokemon[] team;
        static Pokemon[] enemyTeam;

        static Pokemon[] fighters = new Pokemon[2];

        static Random randomSel = new Random();


        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "Pokemon.csv");
            var fileContents = ReadPokemon(fileName); // ReadPokemon definition at line 497.

            bool gameState = true;

            // Intro
            Console.WriteLine("Welcome to POKEMON DOJO");

            LineBreak();// Definition at line 231.
            Console.WriteLine("in which you will be given three pokemon(from Generations 1 - 6)");
            Console.WriteLine("with which to battle a randomly generated opposing team.");

            // Master loop
            while (gameState == true)
            {
                team = new Pokemon[3];
                enemyTeam = new Pokemon[3];
                
                LineBreak(); 
                Console.WriteLine("If you are unfamiliar with pokemon, and would like");
                Console.WriteLine("your own team randomly generated, press the R key.");

                LineBreak();
                Console.Write("Otherwise, press any other key: ");
                var choiceOrRandom = Console.ReadKey();

                if (choiceOrRandom.Key == ConsoleKey.R) // User opts to have pokemon team randomly generated.
                {
                    LineBreak();
                    LineBreak();
                    Console.WriteLine("Your team: ");
                    Delay();// Definition at line 236.
                    for (int i = 0; i <= team.Length - 1; i++)
                    {
                        int teamMate = randomSel.Next(1, 721);
                        foreach (Pokemon pokemon in fileContents)
                        {
                            // If random result has an alternate 'Mega' form, the Mega result(s) will be removed,
                            // and the standard version of the result pokemon will be assigned to the team.
                            if (pokemon.Name.Contains("Mega"))
                            {
                                pokemon.Number = 0;
                            }

                            // If random result is a Legendary pokemon, or a pokemon with multiple form options,
                            // the result is overridden and reset to find a standard pokemon.
                            if (pokemon.Number == teamMate && pokemon.Name.Contains("Rotom") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Forme") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Size") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Cloak") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Mode") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Male") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Male") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("♂") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Female") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("♀") ||
                                pokemon.Number == teamMate && pokemon.Legendary.Contains("True"))
                            {
                                teamMate = 1;
                                pokemon.Number = 0;
                                i--;

                            }
                            // Standard pokemon is assigned to team.
                            else if (pokemon.Number == teamMate && pokemon.Legendary.Contains("False"))
                            {
                                team[i] = pokemon;
                                Console.WriteLine(team[i].Name + ". ");
                                Delay();
                            }
                        }
                    }
                }

                else // If user elects to choose their own pokemon
                {
                    LineBreak();
                    LineBreak();
                    Console.WriteLine("Enter a desired pokemon's name in each of the fields below.");

                    string[] sel = new string[3]; 

                    for (int i = 0; i < sel.Length; i++) // Generated field in which user enters each respective pokemon's name.
                    {
                        Console.Write(i + 1 + ": ");
                        sel[i] = Console.ReadLine(); 
                    }

                    for (int i = 0; i <= team.Length - 1; i++) // Checks for matching result in list, filters typos.
                    {
                        bool isPokemon = false; 
                        foreach (Pokemon pokemon in fileContents)
                        {

                            if (pokemon.Name.ToUpper() == sel[i].ToUpper())
                            {
                                isPokemon = true;
                                team[i] = pokemon;
                            }
                        }

                        if (isPokemon == false) // Resets current selection in absence of matching result - or typo.
                        {
                            LineBreak();
                            LineBreak();

                            Console.WriteLine("There is no pokemon with the name '" + sel[i] + "',");
                            Console.Write("please check for typing errors and try again: ");

                            LineBreak();
                            sel[i] = Console.ReadLine();
                            LineBreak();

                            i--;
                        }
                    }
                    LineBreak();
                    
                    Console.WriteLine("Your team: "); 
                    Delay();
                    
                    for (int i = 0; i <= team.Length - 1; i++) // Displays verified team results.
                    {
                        Console.WriteLine(team[i].Name + ". ");
                        Delay();
                    }
                }

                LineBreak();

                // Enemy team construction.
                Console.Write(String.Format("{0," + Console.WindowWidth / 1 + "}", "Opponent's team: "));
                Delay();
                for (int i = 0; i <= enemyTeam.Length - 1; i++)
                {
                    int foe = randomSel.Next(1, 721);
                    foreach (Pokemon pokemon in fileContents)
                    {
                        // If random result has an alternate 'Mega' form, the Mega result(s) will be removed,
                        // and the standard version of the result pokemon will be assigned to the team.
                        if (pokemon.Name.Contains("Mega"))
                        {
                            pokemon.Number = 0;
                        }

                        // If random result is a Legendary pokemon, or a pokemon with multiple form options,
                        // the result is overridden and reset to find a standard pokemon.
                        if (pokemon.Number == foe && pokemon.Name.Contains("Rotom") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Forme") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Size") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Cloak") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Mode") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Male") ||
                                pokemon.Number == foe && pokemon.Name.Contains("♂") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Female") ||
                                pokemon.Number == foe && pokemon.Name.Contains("♀") ||
                                pokemon.Number == foe && pokemon.Legendary.Contains("True"))
                        {
                            foe = 1;
                            pokemon.Number = 0;
                            i--;
                        }
                        // Standard pokemon is assigned to team.
                        else if (pokemon.Number == foe && pokemon.Legendary.Contains("False"))
                        {
                            enemyTeam[i] = pokemon;
                            Console.Write(String.Format("{0," + Console.WindowWidth / 1 + "}", enemyTeam[i].Name + ". "));
                            Delay();
                        }
                    }
                }

                Next(); // Definition at line 248.
                NextEnemy(); //Definition at line 332.
                Battle(); // Definition at line 344.

                // Prompts user to quit or continue play.
                Console.Write("Play again? y/n: ");

                var answer = Console.ReadKey();
                if (answer.Key == ConsoleKey.Y) // Resets Master Loop.
                {
                    LineBreak();
                    LineBreak();
                    Delay();
                    continue;
                }
                else if (answer.Key == ConsoleKey.N)// Ends game, closes program.
                {
                    LineBreak();
                    gameState = false;

                }
                else
                {
                    while (answer.Key != ConsoleKey.Y && answer.Key != ConsoleKey.N) // Reprompts user for valid input.
                    {
                        LineBreak();
                        Console.WriteLine("Press Y to continue, or N to exit the program");
                        answer = Console.ReadKey();
                    }
                }
            }
        }



        public static void LineBreak() // Shorthand version of enclosed function, used throughout program for increased readability.
        {
            Console.WriteLine(); // Simple line breaks.
        }

        public static void Delay() // Shorthand version of enclosed function, used throughout program for UX purposes.

        {
            System.Threading.Thread.Sleep(800); // Beatlong delays between instances of output.
        }

        public static void Exit() // Shorthand - exits program.
        {
            System.Environment.Exit(0);
        }


        public static void Next() // Prompts user to select the next fighter from their team to battle with.
        {
            LineBreak();

            if (team.Length > 1)
            {
                LineBreak();
                Console.WriteLine("Which pokemon will you send out?");

                var choice = Console.ReadLine();

                for (int i = 0; i <= team.Length - 1; i++) // Verifies entry, filters typos.
                {
                    bool isPokemon = false;
                    foreach (Pokemon pokemon in team)
                    {
                        if (pokemon.Name.ToUpper() == choice.ToUpper())
                        {
                            isPokemon = true;
                            fighters[0] = pokemon;
                        }
                    }

                    if (isPokemon == false) // User entered a typo.
                    {
                        LineBreak();
                        LineBreak();

                        Console.WriteLine("There is no pokemon named '" + choice + "' in your team,");
                        Console.Write("please check for typing errors and try again: ");

                        choice = Console.ReadLine();

                        LineBreak();
                        LineBreak();

                        i--;
                    }
                    else // Entry is verified, pokemon is sent out.
                    {
                        Console.WriteLine("Sent out " + fighters[0].Name);
                        Delay();
                        break; // Prevents multiple printed lines.
                    }
                }
            }
            else if (team.Length < 1) // User's team is empty, current function terminates.
            {
                return;
            }
            else // User has a single remaining pokemon, selection is expedited.
            {
                Console.Write("Send out your final pokemon, " + team[0].Name + "? (y/n) "); // Prompts user to exit or continue.
                var answer = Console.ReadKey();
                LineBreak();

                while (answer.Key != ConsoleKey.Y && answer.Key != ConsoleKey.N) // Reprompts for valid input
                {
                    LineBreak();
                    Console.WriteLine("Press Y to continue, or N to exit the program");
                    answer = Console.ReadKey();
                }

                if (answer.Key == ConsoleKey.N) // User gives up, exits program.
                {
                    LineBreak();
                    Console.WriteLine("Thanks for playing!");
                    Delay();
                    Exit();
                }

                else // Neither previous condition is met, final pokemon is sent out.
                {
                    fighters[0] = team[0];
                    LineBreak();
                    LineBreak();
                    Console.WriteLine("Sent out " + fighters[0].Name);
                    Delay();
                }
            }  
        }


        
        public static void NextEnemy() // Enemy chooses random pokemon in their team to send out next.
        {
            int foeChoice = randomSel.Next(0, enemyTeam.Length);
            {
                fighters[1] = enemyTeam[foeChoice];
                LineBreak();
                Delay();
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 1 + "}", "Opponent sent out " + fighters[1].Name));
                Delay();
            }
        }

        public static void Battle() // The two current fighting pokemon are compared.
        {
            int score = 0;
            int[] stats = { fighters[0].Speed, // Stats of user's battling pokemon.
                            fighters[0].Attack,
                            fighters[0].Defense,
                            fighters[0].SpAttack,
                            fighters[0].SpDefense
                           };

            int foeScore = 0;
            int[] foeStats = { fighters[1].Speed, // Stats of opposing pokemon.
                            fighters[1].Attack,
                            fighters[1].Defense,
                            fighters[1].SpAttack,
                            fighters[1].SpDefense
                           };

            string[] statNames = { "Speed", // Coresponding strings displayed with each stat advantage.
                                   "Attack",
                                   "Defense",
                                   "Special Attack",
                                   "Special Defense"
                                  };

            LineBreak();
            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", fighters[0].Name + " battles " + fighters[1].Name + "!"));

            for (int i = 0; i <= stats.Length - 1; i++) // For each stat, compares both pokemons' value in respective stat.
            {
                LineBreak();

                if (stats[i] > foeStats[i]) // User has superior stat, awards point, prints result.
                {
                    score++;
                    Delay();
                    Console.WriteLine("Your " + fighters[0].Name + " scores a hit with its superior " + statNames[i] + "( " + stats[i] + " )!");
                }

                else if (foeStats[i] > stats[i]) // Opponent has superior stat, awards point, prints result.
                {
                    foeScore++;
                    Delay();
                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 1 + "}", "The opponent's " + fighters[1].Name + " scores a hit with its superior " + statNames[i] + "( " + foeStats[i] + " )!"));
                }

                else // The two pokemons' current stat is equal.
                {
                    Delay();
                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "The Pokemon are equally matched in " + statNames[i] + "!"));

                }
            }

            if (score > foeScore) // User has majority of points.
            {
                Delay();
                LineBreak();
                Console.WriteLine("Your " + fighters[0].Name + " defeated " + fighters[1].Name + "!");

                for (int i = 0; i <= enemyTeam.Length - 1; i++) 
                {
                    if (enemyTeam[i].Name == fighters[1].Name) // Removes fainted pokemon from enemyTeam array.
                    {
                        int pos = i;

                        for (int j = pos; j < enemyTeam.Length - 1; j++)
                        {
                            enemyTeam[j] = enemyTeam[j + 1];
                        }

                        Array.Resize(ref enemyTeam, enemyTeam.Length - 1);

                        if (enemyTeam.Length > 0) // Opponent continues play.
                        {
                            NextEnemy();
                            Battle();
                        }
                        else // You win the battle.
                        {
                            LineBreak();
                            Console.WriteLine("You defeated the opponent!");
                        }
                    }
                }
            }
            else if (score <= foeScore) // Opponent's pokemon has equal or majority points.
            {
                Delay();
                LineBreak();
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 1 + "}", "The opponent's " + fighters[1].Name + " defeated " + fighters[0].Name + "!"));
                for (int i = 0; i <= team.Length - 1; i++)
                {
                    if (team[i].Name == fighters[0].Name)
                    {
                        int pos = i;

                        for (int j = pos; j < team.Length - 1; j++) // Removes fainted pokemon from team array.
                        {
                            team[j] = team[j + 1];
                        }

                        Array.Resize(ref team, team.Length - 1);
                        Delay();

                        if (team.Length > 0)
                        {
                            if (team.Length > 1)
                            {
                                Console.Write("Use next pokemon? (y/n): ");

                                var answer = Console.ReadKey();
                                LineBreak();
                                while (answer.Key != ConsoleKey.Y && answer.Key != ConsoleKey.N)
                                {
                                    LineBreak();
                                    Console.Write("Press Y to continue, or N to exit the program");
                                    answer = Console.ReadKey();
                                    LineBreak();
                                }

                                if (answer.Key == ConsoleKey.N)
                                {
                                    Console.WriteLine("Thanks for playing!");
                                    Delay();
                                    Exit();
                                }

                                LineBreak();
                                LineBreak();
                                Console.Write("Your remaining pokemon: "); 

                                Delay();
                                foreach (Pokemon pokemon in team)
                                {
                                    Console.Write(pokemon.Name + ". ");
                                    Delay();
                                }
                            }
                            Next(); // The battle continues.
                            Battle();
                        }
                        else
                        {
                            LineBreak();
                            Delay();
                            Console.WriteLine("Your team has been wiped out!"); // You lose the battle.
                        }
                    }
                }
            }
        }

        public static List<Pokemon> ReadPokemon(string fileName)  
        {
            var pokedex = new List<Pokemon>();
            using (var reader = new StreamReader(fileName))
            {
                string line = "";

                while ((line = reader.ReadLine()) != null) // Assigns all properties to individual Pokemon objects.
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

