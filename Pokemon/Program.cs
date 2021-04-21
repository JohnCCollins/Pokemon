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

        static Random enemySel = new Random();

        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(currentDirectory);
            var fileName = Path.Combine(directory.FullName, "Pokemon.csv");
            var fileContents = ReadPokemon(fileName);

            bool gameState = true;

            while (gameState == true)
            {
                
                team = new Pokemon[3];
                enemyTeam = new Pokemon[3];

                Console.WriteLine("Welcome to Pokemon Dojo, in which you will be given three pokemon (from Generations 1-6) with which to battle a randomly generated opposing team.");
                LineBreak();
                Console.WriteLine("If you are generally unfamiliar with pokemon and would like your team randomly generated, press the R key.");
                
                Console.Write("Otherwise, press any other key. ");

                var choiceOrRandom = Console.ReadKey();

                if (choiceOrRandom.Key == ConsoleKey.R)
                {
                    LineBreak();
                    Console.WriteLine("Your team: ");
                    Delay();
                    for (int i = 0; i <= team.Length - 1; i++)
                    {

                        int teamMate = enemySel.Next(1, 721);
                        foreach (Pokemon pokemon in fileContents)
                        {
                            if (pokemon.Name.Contains("Mega"))
                            {
                                pokemon.Number = 0;
                            }

                            // if random result is a Legendary pokemon, or a pokemon with multiple form options,
                            // the result is overridden and reset to find a standard pokemon.
                            if (pokemon.Number == teamMate && pokemon.Name.Contains("Rotom") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Forme") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Size") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Cloak") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Mode") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Male") ||
                                pokemon.Number == teamMate && pokemon.Name.Contains("Female") ||
                                pokemon.Number == teamMate && pokemon.Legendary.Contains("True"))
                            {
                                teamMate = 1;
                                pokemon.Number = 0;
                                i--;
        
                            }
                            else if (pokemon.Number == teamMate && pokemon.Legendary.Contains("False"))
                            {
                                team[i] = pokemon;
                                Console.WriteLine(team[i].Name + ". ");
                                Delay();
                            }
                        }
                    }
                }
                else
                {
                    LineBreak();

                    Console.WriteLine("Choose any three pokemon for battle, and enter their name in one of the fields below.");

                    string[] sel = new string[3];

                    for (int i = 0; i < sel.Length; i++)
                    {
                        Console.Write(i + 1 + ": ");
                        sel[i] = Console.ReadLine();
                    }

                    for (int i = 0; i <= team.Length - 1; i++)
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

                        if (isPokemon == false)
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

                    Console.WriteLine("Your team: ");
                    Delay();

                    for (int i = 0; i <= team.Length - 1; i++)
                    {
                        Console.WriteLine(team[i].Name + ". ");
                        Delay();
                    }
                }

                LineBreak();
                Console.Write(String.Format("{0," + Console.WindowWidth / 1 + "}", "Opponent's team: "));

                
                Delay();
                for (int i = 0; i <= enemyTeam.Length - 1; i++)
                {
                    
                    int foe = enemySel.Next(1, 721);
                    foreach (Pokemon pokemon in fileContents)
                    {
                        // if random result has an alternate 'Mega' form, the Mega result(s) will be removed,
                        // and the standard version of the result pokemon will be assigned to the team.
                        if (pokemon.Name.Contains("Mega"))
                        {
                            pokemon.Number = 0;
                        }

                        // if random result is a Legendary pokemon, or a pokemon with multiple form options,
                        // the result is overridden and reset to find a standard pokemon.
                        if (pokemon.Number == foe && pokemon.Name.Contains("Rotom") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Forme") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Size") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Cloak") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Mode") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Male") ||
                                pokemon.Number == foe && pokemon.Name.Contains("Female") ||
                                pokemon.Number == foe && pokemon.Legendary.Contains("True"))
                        {
                            foe = 1;
                            pokemon.Number = 0;
                            i--;
                            
                            
                            
                        }       // Standard pokemon is assigned to team.
                        else if (pokemon.Number == foe && pokemon.Legendary.Contains("False"))
                        {
                            enemyTeam[i] = pokemon;
                            Console.Write(String.Format("{0," + Console.WindowWidth / 1 + "}", enemyTeam[i].Name + ". "));

                            
                            Delay();
                        }
                    }
                }

                Next();
                NextEnemy();
                Battle();


                Console.Write("Play again? y/n: ");

                var yOrN = Console.ReadKey();
                if (yOrN.Key == ConsoleKey.Y)
                {
                    LineBreak();
                    continue;
                }
                else
                {
                    gameState = false;

                }
            }
        }

    

        public static void LineBreak() // shorthand version of enclosed function, used throughout program for increased readability.
        {
            Console.WriteLine(); // Simple line breaks.
        }

        public static void Delay() // shorthand version of enclosed function, used throughout program for UX purposes.
            
        {
            System.Threading.Thread.Sleep(800); // beatlong delays between instances of output.
        }

        public static void Exit()
        {
            System.Environment.Exit(0);
        }


        public static void Next() // prompts user to select the next fighter from their team to battle with.
        {
            LineBreak();

            if (team.Length == 1)
            {
                Console.Write("Send out your final pokemon, " + team[0].Name + "? (y/n) ");
                var answer = Console.ReadKey();
                while (answer.Key != ConsoleKey.Y && answer.Key != ConsoleKey.N)
                {
                    LineBreak();
                    Console.WriteLine("Press Y to continue, or N to exit the program");
                    answer = Console.ReadKey();
                }
                if (answer.Key == ConsoleKey.N)
                {
                    LineBreak();
                    Console.WriteLine("Thanks for playing!");
                    Delay();
                    Exit();
                }
                fighters[0] = team[0];
                LineBreak();
                Console.WriteLine("Sent out " + fighters[0].Name);
                Delay();

            }
            else if (team.Length < 1)
            {
                return;
            }
            else
            {
                LineBreak();
                Console.WriteLine("Which pokemon will you send out?");

                var choice = Console.ReadLine();

                for (int i = 0; i <= team.Length - 1; i++)
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

                    if (isPokemon == false)
                    {
                        LineBreak();
                        LineBreak();

                        Console.WriteLine("There is no pokemon with the name '" + choice + "',");

                        Console.Write("please check for typing errors and try again: ");

                        LineBreak();


                        choice = Console.ReadLine();

                        LineBreak();

                        i--;

                    }
                    else
                    {
                        Console.WriteLine("Sent out " + fighters[0].Name);
                        break;
                    }
                }
            }
        }



        public static void NextEnemy()
        {
            int foeChoice = enemySel.Next(0, enemyTeam.Length);
            {
                
                fighters[1] = enemyTeam[foeChoice];

                LineBreak();
               
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 1 + "}", "Opponent sent out " + fighters[1].Name));
                Delay();
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

            Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", fighters[0].Name + " battles " + fighters[1].Name + "!"));

            for (int i = 0; i <= stats.Length-1; i++)
            {
                LineBreak();
                if (stats[i] > foeStats[i])
                {
                    score++;
                    Delay();
                    Console.WriteLine("Your " + fighters[0].Name + " scores a hit with its superior " + statNames[i] + "( " + stats[i] + " )!");
                    
                }
                else if (foeStats[i] > stats[i])
                {
                    foeScore++;
                    Delay();

                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 1 + "}", "The opponent's " + fighters[1].Name + " scores a hit with its superior " + statNames[i] + "( " + foeStats[i] + " )!"));
                }
                else
                {
                    Delay();
                    Console.WriteLine(String.Format("{0," + Console.WindowWidth / 2 + "}", "The Pokemon are equally matched in " + statNames[i] + "!"));
                   
                }
            }

            if (score > foeScore)
            {
                Delay();
                LineBreak();
                Console.WriteLine("Your " + fighters[0].Name + " defeated " + fighters[1].Name +"!");
                for (int i = 0; i <= enemyTeam.Length-1; i++)
                {

                    if (enemyTeam[i].Name == fighters[1].Name)
                    {
                        int pos = i;

                        for (int j = pos; j < enemyTeam.Length - 1; j++)
                        {
                            enemyTeam[j] = enemyTeam[j + 1];
                        }

                        Array.Resize(ref enemyTeam, enemyTeam.Length - 1);

                        

                        if (enemyTeam.Length > 0)
                        {
                            NextEnemy();
                            Battle();
                            
                        }
                        else
                        {
                            LineBreak();
                            Console.WriteLine("You defeated the opponent!");
                            
                        }
                    }


                }

                
            }
            else if (score <= foeScore)
            {
                Delay();
                LineBreak();
                Console.WriteLine(String.Format("{0," + Console.WindowWidth / 1 + "}", "The opponent's " + fighters[1].Name + " defeated " + fighters[0].Name + "!"));
                for (int i = 0; i <= team.Length - 1; i++)
                {
                    if (team[i].Name == fighters[0].Name)
                    {
                        // removal of current 'fainted' pokemon from team array.
                        int pos = i;
                            
                            for (int j = pos; j < team.Length-1; j++) // re
                            {
                                team[j] = team[j+1];  
                            }
                        Array.Resize(ref team, team.Length - 1);
                        Delay();

                        if (team.Length > 0)
                        {
                            if (team.Length > 1)
                            {
                                Delay();
                                Console.Write("Use next pokemon? (y/n): ");

                                var answer = Console.ReadKey();
                                while (answer.Key != ConsoleKey.Y && answer.Key != ConsoleKey.N)
                                {
                                    Console.Write("Press Y to continue, or N to exit the program");
                                    answer = Console.ReadKey();
                                }
                                if (answer.Key == ConsoleKey.N)
                                {
                                    Console.WriteLine("Thanks for playing!");
                                    Delay();
                                    Exit();
                                }

                                LineBreak();
                                Console.Write("Your remaining pokemon: ");

                                Delay();
                                foreach (Pokemon pokemon in team)
                                {
                                    Console.Write(pokemon.Name + ". ");
                                    Delay();
                                }
                            }
             
                            Next();
                            Battle();

                        }
                        else
                        {
                            LineBreak();
                            Delay();
                            Console.WriteLine("Your team has been wiped out!");
                        }
                    }
                    
                }

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

