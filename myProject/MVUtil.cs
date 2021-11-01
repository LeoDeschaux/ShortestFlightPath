using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

using zzMathVisu.myProject._02_EarthMap;

namespace zzMathVisu.myProject
{
    public static class MVUtil
    {
        public static void SpawnPinAtCoords(String name, Coord coord)
        {
            Pin p = new Pin(name, coord);
        }

        public static Vector2 ConvertCoordToVector(float latitude, float longitude)
        {
            //lambda: longitude
            //phi: latitude
            //x = cos(phi0)*(lambda - lambda0)
            //y = (phi - phi0)

            latitude = MathHelper.ToRadians(latitude * 2);
            longitude = MathHelper.ToRadians(longitude);

            float longCenter = 0; //Settings.SCREEN_WIDTH/2;
            float latCenter = 0; //Settings.SCREEN_HEIGHT/2;
            float R = 0; //6.371km;

            Vector2 result = Vector2.Zero;

            result.X = (longitude - longCenter) * (float)Math.Cos(latCenter);
            result.Y = (latitude - latCenter);

            result.X = (result.X / MathHelper.Pi) * myEngine.Settings.SCREEN_WIDTH / 2;
            result.Y = (result.Y / MathHelper.Pi) * myEngine.Settings.SCREEN_HEIGHT / 2;

            return result;
        }
    }
}
