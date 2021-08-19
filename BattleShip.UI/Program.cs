using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Ships;
using BattleShip.BLL.Requests;

namespace BattleShip.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            //Board board = new Board();

            //PlaceShipRequest request = new PlaceShipRequest
            //{
            //    Coordinate = new Coordinate(1, 1),
            //    Direction = ShipDirection.Down,
            //    ShipType = ShipType.Cruiser
            //};
            //PlaceShipRequest request2 = new PlaceShipRequest
            //{
            //    Coordinate = new Coordinate(2, 1),
            //    Direction = ShipDirection.Down,
            //    ShipType = ShipType.Battleship
            //};
            //PlaceShipRequest request3 = new PlaceShipRequest
            //{
            //    Coordinate = new Coordinate(3, 1),
            //    Direction = ShipDirection.Down,
            //    ShipType = ShipType.Submarine
            //};
            //PlaceShipRequest request4 = new PlaceShipRequest
            //{
            //    Coordinate = new Coordinate(4, 1),
            //    Direction = ShipDirection.Down,
            //    ShipType = ShipType.Carrier
            //};
            //PlaceShipRequest request5 = new PlaceShipRequest
            //{
            //    Coordinate = new Coordinate(5, 1),
            //    Direction = ShipDirection.Down,
            //    ShipType = ShipType.Destroyer
            //};

            //board.PlaceShip(request);
            //board.PlaceShip(request2);
            //board.PlaceShip(request3);
            //board.PlaceShip(request4);
            //board.PlaceShip(request5);

            //board.FireShot(new Coordinate(1, 1));
            //board.FireShot(new Coordinate(7, 5));

            //Output output = new Output();
            //output.PrintBoard(board);

            Workflow wf = new Workflow();
            wf.Start();

            Console.Read();
        }
    }
}
