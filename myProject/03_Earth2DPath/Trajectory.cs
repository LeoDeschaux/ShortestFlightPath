using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace zzMathVisu.myProject._03_Earth2DPath
{
    public class Trajectory : GameObject
    {
        public Marker a;
        public Marker b;

        Shapes shapes;

        Text t;

        public Trajectory(Marker a, Marker b)
        {
            this.a = a;
            this.b = b;

            shapes = new Shapes(Engine.game);

            t = new Text();
            t.transform.position = new Vector2(0, 0);
            t.useScreenCoord = false;
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            shapes.Begin(SceneManager.currentScene.camera);

            DrawTrajectory(new Coord(a.latitude, a.longitude), new Coord(b.latitude, b.longitude), Color.Red);

            Vector2 marker1Pos = MVUtil.ConvertCoordToVector(-a.latitude, a.longitude);
            Vector2 marker2Pos = MVUtil.ConvertCoordToVector(-b.latitude, b.longitude);

            Trajectory.DrawCrossHaitAtPoint(marker1Pos, matrix);
            Trajectory.DrawCrossHaitAtPoint(marker2Pos, matrix);

            shapes.End();
        }

        private void DrawTrajectory(Coord a, Coord b, Color color)
        {

            Vector2 posA = MVUtil.ConvertCoordToVector(a.latitude, a.longitude);
            Vector2 posB = MVUtil.ConvertCoordToVector(b.latitude, b.longitude);

            float distanceY = Math.Abs(posB.Y - posA.Y);
            float distanceX = Math.Abs(posB.X - posA.X);

            t.s = "" + distanceY + ", posA: " + posA.Y + ", posB: " + posB.Y;

            if(distanceY < 720/2 && distanceX < 1280/2)
            {
                shapes.DrawLine(posA * new Vector2(1, -1), posB * new Vector2(1, -1), 3, color);
            }
            else
            {
                if(posA.Y > posB.Y)
                {
                    shapes.DrawLine(posA * new Vector2(1, -1), new Vector2(0, -720/2), 3, color);
                    shapes.DrawLine(posB * new Vector2(1, -1), new Vector2(0, 720 / 2), 3, color);
                }
                else
                {
                    shapes.DrawLine(posA * new Vector2(1, -1), new Vector2(0, 720/2), 3, color);
                    shapes.DrawLine(posB * new Vector2(1, -1), new Vector2(0, -720/2), 3, color);
                }
                //shapes.DrawLine(posA * new Vector2(1, -1), posB * new Vector2(1, -1), 3, color);
            }

            
        }

        private void DrawTrajectory(Coord a, Coord b, int segmentation, Color color)
        {
            Vector2 posA = MVUtil.ConvertCoordToVector(a.latitude, a.longitude);
            Vector2 posB = MVUtil.ConvertCoordToVector(a.latitude, b.longitude);

            Vector2 posC = MVUtil.ConvertCoordToVector(a.latitude, b.longitude);
            Vector2 posD = MVUtil.ConvertCoordToVector(b.latitude, b.longitude);

            shapes.DrawLine(posA * new Vector2(1, -1), posB * new Vector2(1, -1), 3, color);
            shapes.DrawLine(posC * new Vector2(1, -1), posD * new Vector2(1, -1), 3, color);
        }

        public static void DrawCrossHaitAtPoint(Vector2 pos, Matrix matrix)
        {
            Vector2 dimX = new Vector2(20, 4);
            Vector2 dimY = new Vector2(4, 20);

            DrawSimpleShape.DrawRectangleFull(pos - dimX / 2, dimX, Color.Red, matrix);
            DrawSimpleShape.DrawRectangleFull(pos - dimY / 2, dimY, Color.Red, matrix);
        }
    }
}
