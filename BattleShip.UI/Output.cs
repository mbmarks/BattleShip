using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Responses;
using static BattleShip.UI.Workflow;

namespace BattleShip.UI
{
    public class Output
    {

        public void PressKeyToContinue(string prompt = "Press any key to continue...")
        {
            Console.WriteLine(prompt);
            Console.ReadKey();
        }

        public void SplashScreen()
        {
            Console.WriteLine("Welcome to the game of Battleship!");
            PressKeyToContinue();
            Console.Clear();
        }

        public void OutputLine(string line)
        {
            Console.WriteLine(line);
        }

        public void BeginTurn(Player currentPlayer, Player otherPlayer) 
        {
            Console.WriteLine($"It's {currentPlayer.Name}'s turn...");
            PrintBoard(otherPlayer.Board);
        }

        public void PrintBoard(Board board)
        {
            for (int letter = 0; letter < 11; letter++)
            {
                for (int number = 0; number < 11; number++)
                {
                    if (letter == 0 || number == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        if (letter == 0 && number == 0)
                        {
                            Console.Write(" * ");
                            continue;
                        }
                        if (letter == 0)
                        {
                            Console.Write(string.Format(" {0,-2}",number));
                            continue;
                        }
                        char character = (char)('A' - 1 + letter);
                        Console.Write($" {character} ");
                        continue;
                    }
                    // This is where the logic for the board will go
                    // the values from i and j should be the correct coordinates
                    // in the Dictionary for shot history
                    ShotHistory shotHistory = board.CheckCoordinate(new BLL.Requests.Coordinate(number, letter));
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" ");
                    switch (shotHistory)
                    {
                        case ShotHistory.Hit:
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("H");
                            break;
                        case ShotHistory.Miss:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.Write("M");
                            break;
                        case ShotHistory.Unknown:
                            Console.BackgroundColor = ConsoleColor.Cyan;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("-");
                            break;
                    }
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(" ");
                }
                Console.WriteLine();
            }

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
