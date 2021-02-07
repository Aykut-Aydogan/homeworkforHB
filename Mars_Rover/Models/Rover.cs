using System;
using System.Collections.Generic;
using System.Text;

namespace Mars_Rover.Models
{
    public class Rover
    {
        public int X { get; set; }
        public int Y { get; set; }
        public string Direction { get; set; }
        public string[] exploreDirection { get; set; }
    }
}
