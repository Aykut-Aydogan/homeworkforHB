using System;
using System.Collections.Generic;
using System.Linq;
using Mars_Rover.Models;

namespace Mars_Rover
{
    public class Program
    {
        private static string[] DirectionArray = new string[] { "N", "E", "S", "W" };
        private static string[] ExploreDirectionArray = new string[] { "L", "R", "M" };
        public static void Main(string[] args)
        {
            Console.WriteLine("Mars Rover");
            Console.WriteLine();

            var area = GetAreaInformation();
            var rover = GetRoverInformation();
            ExplorePlateau(rover, area);

            while (true)
            {
                Console.WriteLine();
                Console.Write("Do you want to next rover set to position (y / n) : ");
                var keyInfo = Console.ReadKey();
                Console.WriteLine();

                if (keyInfo.Key == ConsoleKey.Y)
                {
                    var nextRover = GetRoverInformation();
                    ExplorePlateau(nextRover, area);
                }
                else if (keyInfo.Key == ConsoleKey.N)
                {
                    break;
                }
            }

            Console.WriteLine("Finish");
            Console.ReadLine();
        }

        static Area GetAreaInformation()
        {
            Console.Write("Upper-right coordinates of the plateau : ");
            string[] plateauArea = Console.ReadLine().Split(' ');

            try
            {
                var area = GetArea(plateauArea[0], plateauArea[1]);
                if (area.maxX == 0 && area.maxY == 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the different values. (0,0) value is not valid.");
                    throw new Exception();
                }
                return area;
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error !. Please enter the correct values.");
                Console.WriteLine();
                return GetAreaInformation();
            }
        }

        public static Area GetArea(string x, string y)
        {
            return new Area
            {
                maxX = int.Parse(x),
                maxY = int.Parse(y)
            };
        }

        static Rover GetRoverInformation()
        {
            Console.WriteLine();

            Console.Write("Rover's position : ");
            string[] roverPosition = Console.ReadLine().Split(' ');

            Console.WriteLine();

            string[] exploreValues = GetExploreData();

            try
            {
                //validation
                if (!exploreValues.Any(x => ExploreDirectionArray.Contains(x)) ||
                    roverPosition.Length < 3 ||
                    !DirectionArray.Contains(roverPosition[2].ToUpper()))
                {
                    throw new Exception();
                }

                return GetRover(roverPosition[0], roverPosition[1], roverPosition[2].ToUpper(), exploreValues);
            }
            catch (Exception)
            {
                Console.WriteLine();
                Console.WriteLine("Error !. Please enter the correct values.");
                Console.WriteLine();
                return GetRoverInformation();
            }
        }

        public static Rover GetRover(string x, string y, string direction, string[] exploreDirection)
        {
            return new Rover
            {
                X = int.Parse(x),
                Y = int.Parse(y),
                Direction = direction,
                exploreDirection = exploreDirection
            };
        }

        static string[] GetExploreData()
        {
            Console.Write("Explore the plateau : ");
            var roverExplore = Console.ReadLine().ToCharArray();

            return GetExplore(roverExplore);            
        }

        public static string[] GetExplore(char[] roverExplore)
        {
            string[] exploreValues = new string[roverExplore.Length];
            for (int i = 0; i < roverExplore.Length; i++)
            {
                exploreValues[i] = roverExplore[i].ToString().ToUpper();
            }

            return exploreValues;
        }

        static void ExplorePlateau(Rover rover, Area area)
        {
            var roverDirection = rover.Direction;
            int moveX = rover.X;
            int moveY = rover.Y;
            bool flag = true;

            foreach (var explorer in rover.exploreDirection)
            {
                //get new direction
                roverDirection = GetDirectionFromExploreDirection(roverDirection, explorer);

                if (explorer == ExploreDirectionArray.Last())
                {
                    var newPoint = MoveRover(moveX, moveY, roverDirection);
                    if (newPoint.X > area.maxX || newPoint.Y > area.maxY)
                    {
                        Console.WriteLine("Error !. The rover went out of the plateau.");
                        Console.WriteLine("Please enter the correct explore values.");

                        //get new explore direction
                        var newExploreData = GetExploreData();
                        rover.exploreDirection = newExploreData;
                        ExplorePlateau(rover, area);
                        flag = false;
                        break;
                    }
                    else
                    {
                        moveX = newPoint.X;
                        moveY = newPoint.Y;
                    }
                }
            }
            if (flag)
                Console.WriteLine($"Rover position : {moveX} {moveY} {roverDirection}");
        }

        public static string GetDirectionFromExploreDirection(string direction, string exploreDirection)
        {
            int dirPosition = DirectionArray.ToList().IndexOf(direction) + 1;
            switch (exploreDirection)
            {
                case "L":
                    if (dirPosition == 1)
                    {
                        return DirectionArray.Last();
                    }
                    else
                    {
                        return DirectionArray[dirPosition - 2];
                    }
                case "R":
                    if (dirPosition == DirectionArray.Length)
                    {
                        return DirectionArray.First();
                    }
                    else
                    {
                        return DirectionArray[dirPosition];
                    }
                default:
                    return direction;
            }
        }

        public static Point MoveRover(int x, int y, string direction)
        {
            switch (direction)
            {
                case "N":
                    return new Point { X = x, Y = y + 1 };
                case "E":
                    return new Point { X = x + 1, Y = y };
                case "S":
                    return new Point { X = x, Y = y - 1 };
                case "W":
                    return new Point { X = x - 1, Y = y };
                default:
                    return new Point { X = x, Y = y };
            }
        }
    }
}
