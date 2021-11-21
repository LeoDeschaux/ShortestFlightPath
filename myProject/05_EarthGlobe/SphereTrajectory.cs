using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using myEngine;
using zzMathVisu.myProject._04_EarthSphereCoordinates;

namespace zzMathVisu.myProject._05_EarthGlobe
{
    public class SphereTrajectory : EmptyObject
    {
        List<Cursor3D> points;
        public Cursor3D a;
        public Cursor3D b;
        public int amount = 5;

        Camera3D camera;

        public SphereTrajectory(Camera3D camera, Cursor3D a, Cursor3D b)
        {
            this.a = a;
            this.b = b;

            this.camera = camera;

            this.a.isVisible = false;
            this.b.isVisible = false;

            //CreateTrajectory();
        }

        private void CreateTrajectory()
        {
            return;

            Vector2 startPoint = new Vector2(a.latitude, a.longitude);
            Vector2 endPoint = new Vector2(b.latitude, b.longitude);

            points = new List<Cursor3D>();

            for (int i = 0; i < amount; i++)
            {
                Vector2 pos = startPoint - ((endPoint / (amount + 1)) * (1 + i));
                points.Add(new Cursor3D(camera, pos.X, pos.Y));
            }
        }

        public override void Update()
        {
            return;

            Vector2 startPoint = new Vector2(a.latitude, a.longitude);
            Vector2 endPoint = new Vector2(b.latitude, b.longitude);

            for (int i = 0; i < amount; i++)
            {
                Vector2 pos = startPoint - ((endPoint / (amount + 1)) * (1 + i));
                points[i].SetPos(pos.X, pos.Y);
            }
        }
    }
}
