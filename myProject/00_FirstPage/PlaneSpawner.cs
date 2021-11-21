using System;
using System.Collections.Generic;
using System.Text;
using myEngine;

namespace zzMathVisu
{
    public class PlaneSpawner : EmptyObject
    {
        Random random;
        public PlaneSpawner()
        {
            random = new Random();
            Delay d = new Delay(1000, () => SpawnPlane());
        }

        public override void Update()
        {
        }

        private void SpawnPlane()
        {
            float posX = random.Next(-1280 / 2, 1280 / 2);
            float speed = random.Next(60, 240);

            Plane p = new Plane(posX, speed);
        }
    }
}
