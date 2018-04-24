
using System;
using System.Collections.Generic;
using System.Text;

namespace EniroMapApi
{
    public class RoutingParameters
    {
        public Coordinate From { get; set; }
        public Coordinate To { get; set; }
        public PrefEnum Pref { get; set; }
    }

    public enum PrefEnum
    {
        Fastest,
        Shortest
    }
}
