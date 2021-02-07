using Mars_Rover.Models;
using Mars_Rover;
using Xunit;

namespace XUnitTestMars_Rover
{
    public class UnitTestRover
    {
        [Fact]
        public void TestArea()
        {
            var area = Program.GetArea("5", "6");
            Assert.NotNull(area);

            var actual = new Area { maxX = 5, maxY = 6 };
            Assert.Equal(area.maxX, actual.maxX);
            Assert.Equal(area.maxY, actual.maxY);
        }

        [Fact]
        public void TestExplore()
        {
            var chrArray = new char[] { 'l', 'm', 'r', 'm' };
            var strArray = new string[] { "L", "M", "R", "M" };
            var explore = Program.GetExplore(chrArray);

            Assert.NotNull(explore);
            Assert.Equal(explore, strArray);
        }

        [Fact]
        public void TestRover()
        {
            var strArray = new string[] { "L", "M", "R", "M" };
            var rover = Program.GetRover("1", "2", "N", strArray);
            Assert.NotNull(rover);

            var actual = new Rover { X = 1, Y = 2, Direction = "N", exploreDirection = strArray };
            Assert.Equal(rover.X, actual.X);
            Assert.Equal(rover.Y, actual.Y);
            Assert.Equal(rover.Direction, actual.Direction);
            Assert.Equal(rover.exploreDirection, actual.exploreDirection);
        }

        [Fact]
        public void TestGetDirection()
        {
            var newDirection = Program.GetDirectionFromExploreDirection("N", "L");
            Assert.NotNull(newDirection);

            string actual = "W";
            Assert.Equal(newDirection, actual);
        }

        [Fact]
        public void TestMove()
        {
            var newPoint = Program.MoveRover(1, 2, "E");
            Assert.NotNull(newPoint);

            var actual = new Point { X = 2, Y = 2 };
            Assert.Equal(newPoint.X, actual.X);
            Assert.Equal(newPoint.Y, actual.Y);
        }
    }
}
