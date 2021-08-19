using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Ships;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;

namespace BattleShip.UI
{
    public class Workflow
    {
        private Player _player1, _player2;
        private readonly Output _output = new Output();
        private readonly Input _input = new Input();

        public void Start()
        {
            _output.SplashScreen();
            
            bool keepPlaying = true;
            while (keepPlaying)
            {
                GetPlayers();

                // Skip randomly choosing the first player
                Player currentPlayer = _player1, otherPlayer = _player2;

                SetUp(_player1, _player2);

                bool gameOn = true;
                while (gameOn)
                {
                    ShotStatus nextTurn = Turn(currentPlayer, otherPlayer);

                    switch (nextTurn)
                    {
                        case ShotStatus.Duplicate:
                        case ShotStatus.Invalid:
                            continue;
                        case ShotStatus.Hit:
                        case ShotStatus.Miss:
                        case ShotStatus.HitAndSunk:
                            Player temp = currentPlayer;
                            currentPlayer = otherPlayer;
                            otherPlayer = temp;
                            continue;
                        case ShotStatus.Victory:
                            gameOn = false;
                            break;
                    }
                }

                string playAgain = null;
                while (string.IsNullOrEmpty(playAgain))
                {
                    playAgain = _input.GetPlayAgainFromUser();
                    switch (playAgain.ToLower())
                    {
                        case "y":
                        case "yes":
                            break;
                        case "n":
                        case "no":
                            _output.OutputLine("Thanks for playing!!");
                            Console.ReadKey();
                            keepPlaying = false;
                            break;
                        default:
                            playAgain = null;
                            break;
                    }
                }
            }
        }

        private ShotStatus Turn(Player currentPlayer, Player otherPlayer)
        {
            // This will be the loop for the turns
            Console.Clear();

            _output.BeginTurn(currentPlayer, otherPlayer);

            Coordinate coordinate = null;
            while (coordinate == null)
            {
                string stringCoordinate = _input.GetCoordinateFromUser();

                coordinate = ParseCoordinate(stringCoordinate);
            }
            FireShotResponse fireShotResponse = otherPlayer.Board.FireShot(coordinate);

            ShotStatus shotStatus = InterpretFireShotResponse(fireShotResponse);

            return shotStatus;
        }

        private ShotStatus InterpretFireShotResponse(FireShotResponse fireShotResponse)
        {
            switch (fireShotResponse.ShotStatus)
            {
                case ShotStatus.Duplicate:
                    _output.OutputLine("You already went there.");
                    _output.PressKeyToContinue();
                    return ShotStatus.Duplicate;
                case ShotStatus.Hit:
                    _output.OutputLine("You got a hit!");
                    _output.PressKeyToContinue();
                    return ShotStatus.Hit;
                case ShotStatus.HitAndSunk:
                    _output.OutputLine($"You hit and sunk the {fireShotResponse.ShipImpacted}");
                    _output.PressKeyToContinue();
                    return ShotStatus.HitAndSunk;
                case ShotStatus.Invalid:
                    _output.OutputLine("Invalid, repeat your turn.");
                    _output.PressKeyToContinue();
                    return ShotStatus.Invalid;
                case ShotStatus.Miss:
                    _output.OutputLine("oooo.. Miss.");
                    _output.PressKeyToContinue();
                    return ShotStatus.Miss;
                case ShotStatus.Victory:
                    _output.OutputLine("Hit and sunk!\nYou won the game!!");
                    _output.PressKeyToContinue();
                    return ShotStatus.Victory;
                default:
                    return ShotStatus.Invalid;
            }
        }

        private void GetPlayers()
        {
            _player1 = new Player(1);
            _player2 = new Player(2);
        }

        // To ask each player to fill the boards and place their ships
        private void SetUp(Player player1, Player player2)
        {
            Console.Clear();

            Player[] players = { player1, player2 };
            ShipType[] shipTypes = { ShipType.Battleship, ShipType.Carrier, 
                ShipType.Cruiser, ShipType.Destroyer, ShipType.Submarine };
            foreach (Player player in players)
            {
                _output.OutputLine($"{player.Name}, please place your ships.");
                foreach (ShipType shipType in shipTypes)
                {
                    ShipPlacement shipPlacement = ShipPlacement.Overlap;
                    while (shipPlacement == ShipPlacement.Overlap || shipPlacement == ShipPlacement.NotEnoughSpace)
                    {

                        _output.OutputLine($"\nWhere would you like to place your {shipType}?");

                        Coordinate coordinate = null;
                        while (coordinate == null)
                        {
                            coordinate = ParseCoordinate(_input.GetCoordinateFromUser());
                        }

                        ShipDirection? shipDirectionNullable = null;
                        while (shipDirectionNullable == null)
                        {
                            shipDirectionNullable = ParseShipDirection(_input.GetDirectionFromUser());
                        }

                        ShipDirection shipDirection = (ShipDirection)shipDirectionNullable;

                        PlaceShipRequest placeShipRequest = new PlaceShipRequest
                                                                    {
                                                                        Coordinate = coordinate,
                                                                        Direction = shipDirection,
                                                                        ShipType = shipType
                                                                    };

                        shipPlacement = player.Board.PlaceShip(placeShipRequest);
                        switch (shipPlacement)
                        {
                            case ShipPlacement.NotEnoughSpace:
                            case ShipPlacement.Overlap:
                                _output.OutputLine($"{shipPlacement}. Try something else.");
                                _output.PressKeyToContinue();
                                break;
                            case ShipPlacement.Ok:
                                _output.OutputLine($"{placeShipRequest.ShipType} was placed at " +
                                    $"{(char)('A' - 1 +placeShipRequest.Coordinate.YCoordinate)}{placeShipRequest.Coordinate.XCoordinate} " +
                                    $"facing {placeShipRequest.Direction}.");
                                _output.PressKeyToContinue();
                                break;
                        }
                    }
                }
                Console.Clear();
            }
        }

        public ShipDirection? ParseShipDirection(string stringShipDirection)
        {
            switch (stringShipDirection)
            {
                case "up":
                    return ShipDirection.Up;
                case "down":
                    return ShipDirection.Down;
                case "left":
                    return ShipDirection.Left;
                case "right":
                    return ShipDirection.Right;
                default:
                    _output.OutputLine("Not a vaild direction.");
                    break;
            }

            return null;
        }

        public Coordinate ParseCoordinate(string stringCoordinate)
        {
            int x, y;

            if (stringCoordinate.Length < 2 || stringCoordinate.Length > 3)
            {
                _output.OutputLine("Coordinate not valid. Too long or too short.");
                return null;
            }

            if (stringCoordinate.Substring(0, 1).ToLower()[0] >= 'a' && stringCoordinate.Substring(0, 1).ToLower()[0] <= 'j')
            {
                y = stringCoordinate.Substring(0, 1).ToLower()[0] - 'a' + 1;
            }
            else
            {
                _output.OutputLine("Coordinate not valid. Valid character not in first spot.");
                return null;
            }

            if (stringCoordinate.Length == 2)
            {
                if (int.TryParse(stringCoordinate.Substring(1, 1), out x))
                {
                    return new Coordinate(x, y);
                }
                else
                {
                    _output.OutputLine("Coordinate not valid. Not a valid numberical second coordinate (single digit).");
                    return null;
                }
            }
            else
            {
                if (int.TryParse(stringCoordinate.Substring(1, 2), out x) && x == 10)
                {
                    return new Coordinate(x, y);
                }
                else
                {
                    _output.OutputLine("Coordinate not valid. Not a valid numberical second coordinate (double digit).");
                    return null;
                }
            }
        }

        public class Player
        {
            public string Name { get; }
            public Board Board { get; }

            public Player(int number)
            {
                Name = new Input().AskForNameFromUser(number);
                Board = new Board();

                Console.WriteLine($"Player One's name is {this.Name}" +
                    $"\nPress Any Key too Continue . . .");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
