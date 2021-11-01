using System;
using System.Collections.Generic;
using System.Text;

namespace zzMathVisu.myProject
{
    public struct Coord
    {
        public float latitude;
        public float longitude;

        public Coord(float latitude, float longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public override string ToString()
        {
            return "" + latitude + ", " + longitude;
        }

        public static readonly Coord Paris = new Coord(48.86f, 2.34f);
        public static readonly Coord Tokyo = new Coord(35.65f, 139.83f);
        public static readonly Coord LeCap = new Coord(-33f, 18f);
        public static readonly Coord Mexico = new Coord(19.43f, -99.13f);
        public static readonly Coord PuntasArenas = new Coord(-53f, -70f);
    }
}