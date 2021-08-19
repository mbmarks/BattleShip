using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using BattleShip.UI;
using BattleShip.BLL.Requests;


namespace Battleship.Tests
{
    [TestFixture]
    public class CoordinateParseTests
    {

        [Test]
        public void CoordinateParseTest1()
        {
            Coordinate expected = new Coordinate(1, 1);
            Coordinate actual = new Workflow().ParseCoordinate("a1");

            Assert.AreEqual(expected.XCoordinate, actual.XCoordinate);
            Assert.AreEqual(expected.YCoordinate, actual.YCoordinate);
        }

        [Test]
        public void CoordinateParseTest2()
        {
            Coordinate expected = new Coordinate(10, 2);
            Coordinate actual = new Workflow().ParseCoordinate("b10");

            Assert.AreEqual(expected.XCoordinate, actual.XCoordinate);
            Assert.AreEqual(expected.YCoordinate, actual.YCoordinate);
        }

        [Test]
        public void CoordinateParseTest3()
        {
            Coordinate expected = new Coordinate(10, 10);
            Coordinate actual = new Workflow().ParseCoordinate("j10");

            Assert.AreEqual(expected.XCoordinate, actual.XCoordinate);
            Assert.AreEqual(expected.YCoordinate, actual.YCoordinate);
        }

        [Test]
        public void CoordinateParseTest4()
        {
            Coordinate expected = new Coordinate(4, 3);
            Coordinate actual = new Workflow().ParseCoordinate("c4");

            Assert.AreEqual(expected.XCoordinate, actual.XCoordinate);
            Assert.AreEqual(expected.YCoordinate, actual.YCoordinate);
        }

        [Test]
        public void CoordinateParseTest5()
        {
            Coordinate actual = new Workflow().ParseCoordinate("k4");

            Assert.AreEqual(null, actual);
        }

        [Test]
        public void CoordinateParseTest6()
        {
            Coordinate actual = new Workflow().ParseCoordinate("-123");

            Assert.AreEqual(null, actual);
        }

        [Test]
        public void CoordinateParseTest7()
        {
            Coordinate actual = new Workflow().ParseCoordinate("a90");

            Assert.AreEqual(null, actual);
        }
    }
}
