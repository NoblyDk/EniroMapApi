
using System;
using System.Collections.Generic;
using System.Text;

namespace EniroMapApi
{
    public class RoutingParameters
    {
        public double[] From { get; set; }
        public double[] To { get; set; }
        public PrefEnum Pref { get; set; }
    }

    public enum PrefEnum
    {
        Fastest,
        Shortest
    }
}
