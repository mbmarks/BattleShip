using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    public class Input
    {
        private readonly Output _output;

        public Input()
        {
            _output = new Output();
        }

        private int GetIntFromUser(string prompt)
        {
            bool first = true;
            int result;
            string userInput;

            do
            {
                // If here a second or subsequent time, the input was
                // not valid
                if (!first)
                {
                    // bad input
                    Console.WriteLine("That is not a valid input.");
                    _output.PressKeyToContinue();
                }

                // Set first to false to cause error message to display
                // above on the next pass through the loop.
                first = false;

                // 1 & 2: Prompt and Read
                Console.Write(prompt);
                userInput = Console.ReadLine();

                // attempt to convert - if fail, loop again
            } while (!int.TryParse(userInput, out result));

            return result;
        }

        public string GetStringFromUser(string prompt)
        {
            bool first = true;
            string userInput;

            do
            {
                // If here a second or subsequent time, the input was
                // not valid
                if (!first)
                {
                    // bad input
                    Console.WriteLine("That is not a valid input.");
                    _output.PressKeyToContinue();
                }

                // Set first to false to cause error message to display
                // above on the next pass through the loop.
                first = false;

                // 1 & 2: Prompt and Read
                Console.Write(prompt);
                userInput = Console.ReadLine();

                // attempt to convert - if fail, loop again
            } while (string.IsNullOrEmpty(userInput));

            return userInput;
        }

        public string AskForNameFromUser(int player)
        {
            return GetStringFromUser($"What is the name of player {player}? ");
        }

        public int GetIntInRange(string prompt, int low, int high)
        {
            while (true)
            {
                int input = GetIntFromUser(prompt);

                if (input <= high && input >= low)
                {
                    return input;
                }

                Console.WriteLine("Out of range, try again.");
                _output.PressKeyToContinue();
            }
        }

        public string GetCoordinateFromUser()
        {
            return GetStringFromUser("Please enter a coordinate: ");
        }

        public string GetDirectionFromUser()
        {
            return GetStringFromUser("Please enter a direction: ");
        }

        public string GetPlayAgainFromUser()
        {
            return GetStringFromUser("Do you want to play again? ");
        }
    }
}
