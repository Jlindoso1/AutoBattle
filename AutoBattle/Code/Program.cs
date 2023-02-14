using System;
using static AutoBattle.Character;
using static AutoBattle.Grid;
using System.Collections.Generic;
using System.Linq;
using static AutoBattle.Types;

namespace AutoBattle
{
    class Program
    {
        static Grid grid;
        static CharacterClass playerCharacterClass;
        static Character playerCharacter;
        static Character enemyCharacter;
        static List<Character> allPlayers;
        static int currentTurn;
        static int numberOfPossibleTiles;

        static void Setup()
        {
            GetPlayerChoice();
        }

        static void GetPlayerChoice()
        {
            //asks for the player to choose between for possible classes via console.
            Console.WriteLine("Choose Between One of this Classes:\n");
            Console.WriteLine("[1] Paladin, [2] Warrior, [3] Cleric, [4] Archer");
            //store the player choice in a variable
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreatePlayerCharacter(Int32.Parse(choice));
                    break;
                case "2":
                    CreatePlayerCharacter(Int32.Parse(choice));
                    break;
                case "3":
                    CreatePlayerCharacter(Int32.Parse(choice));
                    break;
                case "4":
                    CreatePlayerCharacter(Int32.Parse(choice));
                    break;
                default:
                    GetPlayerChoice();
                    break;
            }
        }

        static void CreatePlayerCharacter(int classIndex)
        {

            CharacterClass characterClass = (CharacterClass)classIndex;
            Console.WriteLine($"Player Class Choice: {characterClass}");
            playerCharacter = new Character(characterClass);
            playerCharacter.Health = 100;
            playerCharacter.BaseDamage = 20;
            playerCharacter.PlayerIndex = 0;

            CreateEnemyCharacter();
        }

        static void CreateEnemyCharacter()
        {
            //randomly choose the enemy class and set up vital variables
            var rand = new Random();
            int randomInteger = rand.Next(1, 4);
            CharacterClass enemyClass = (CharacterClass)randomInteger;
            Console.WriteLine($"Enemy Class Choice: {enemyClass}");
            enemyCharacter = new Character(enemyClass);
            enemyCharacter.Health = 100;
            enemyCharacter.BaseDamage = 20;
            enemyCharacter.PlayerIndex = 1;
            StartGame();
        }

        static void StartGame()
        {
            //populates the character variables and targets
            enemyCharacter.Target = playerCharacter;
            playerCharacter.Target = enemyCharacter;
            allPlayers.Add(playerCharacter);
            allPlayers.Add(enemyCharacter);
            AlocatePlayers();
            StartTurn();

        }

        static void AlocatePlayers()
        {
            AlocatePlayerCharacter();
        }

        static int RandomInt(int min, int maxExclusive, int seed)
        {
            Random rand = new Random(seed);
            int result = rand.Next(min, maxExclusive);
            return result;
        }

        static void AlocatePlayerCharacter()
        {
            int min = 0;
            int max = grid.xLength;
            int random = RandomInt(min, max, System.DateTime.Now.Millisecond);
            GridBox RandomLocation = (grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!RandomLocation.ocupied)
            {
                RandomLocation.ocupied = true;
                grid.grids[random] = RandomLocation;
                playerCharacter.currentBox = grid.grids[random];
                AlocateEnemyCharacter();
            }
            else
            {
                AlocatePlayerCharacter();
            }
        }

        static void AlocateEnemyCharacter()
        {
            int min = (grid.yLength - 1) * grid.xLength;
            int max = grid.yLength * grid.xLength;
            int random = RandomInt(min, max, System.DateTime.Now.Millisecond);
            GridBox RandomLocation = (grid.grids.ElementAt(random));
            Console.Write($"{random}\n");
            if (!RandomLocation.ocupied)
            {
                RandomLocation.ocupied = true;
                grid.grids[random] = RandomLocation;
                enemyCharacter.currentBox = grid.grids[random];
                grid.drawBattlefield();
            }
            else
            {
                AlocateEnemyCharacter();
            }


        }

        static void StartTurn()
        {

            if (currentTurn == 0)
            {
                int rand = RandomInt(0, 2, DateTime.Now.Millisecond);
                if(rand > 0)
                {
                    Character temp = allPlayers[0];
                    allPlayers[0] = allPlayers[1];
                    allPlayers[1] = temp;
                }
            }

            foreach (Character character in allPlayers)
            {
                character.StartTurn(grid);
            }

            currentTurn++;
            HandleTurn();
        }

        static void HandleTurn()
        {
            if (playerCharacter.Health <= 0 || enemyCharacter.Health <= 0)
            {
                if(playerCharacter.Health <= 0)
                {
                    Console.WriteLine($"Player {playerCharacter.PlayerIndex} is dead\n");
                }else
                {
                    Console.WriteLine($"Player {enemyCharacter.PlayerIndex} is dead\n");
                }

                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.Write(Environment.NewLine + Environment.NewLine);

                return;
            }
            else
            {
                Console.Write(Environment.NewLine + Environment.NewLine);
                Console.WriteLine("Click on any key to start the next turn...\n");
                Console.Write(Environment.NewLine + Environment.NewLine);

                ConsoleKeyInfo key = Console.ReadKey();
                StartTurn();
            }
        }

        static void Main(string[] args)
        {
            grid = new Grid(6, 10);
            allPlayers = new List<Character>();
            currentTurn = 0;
            numberOfPossibleTiles = grid.grids.Count;
            Setup();
        }
    }
}
