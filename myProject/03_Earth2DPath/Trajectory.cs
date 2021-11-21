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

            shapes.End();
        }

        private void DrawTrajectory(Coord a, Coord b, Color color)
        {
            Vector2 posA = MVUtil.ConvertCoordToVector(a.latitude, a.longitude);
            Vector2 posB = MVUtil.ConvertCoordToVector(b.latitude, b.longitude);

            Trajectory.DrawCrosshairAtPos(posA, Color.Red, SceneManager.currentScene.camera.transformMatrix);
            Trajectory.DrawCrosshairAtPos(posB, Color.LightGreen, SceneManager.currentScene.camera.transformMatrix);

            float distance = (posB - posA).Length();
            t.s = "" + "distance : " + distance;

            DrawCursorEveryPos(posB);
            //DrawNet(posA, posB);

            //DRAW REVERSE NEAREST PATH
            shapes.DrawLine(posB * new Vector2(1, -1),
                (posB * new Vector2(1, -1)) - ((GetNearestPos(posA, posB) * new Vector2(1, -1)) - (posA * new Vector2(1, -1))) * new Vector2(1, 1),
                3, color);
        }

        private void DrawNet(Vector2 from, Vector2 to)
        {
            foreach (Vector2 outsidePos in GetAllOutsidePos(to))
            {
                shapes.DrawLine(from * new Vector2(1, -1), outsidePos * new Vector2(1, -1), 3, Color.White);
            }

            shapes.DrawLine(from * new Vector2(1, -1), to * new Vector2(1, -1), 3, Color.White);

            shapes.DrawLine(from * new Vector2(1, -1), GetNearestPos(from, to) * new Vector2(1, -1), 3, Color.Red);
        }

        private Vector2 GetNearestPos(Vector2 from, Vector2 to)
        {
            Vector2 result = to;

            float distance = Vector2.Distance(from, to);

            foreach(Vector2 outsidePos in GetAllOutsidePos(to))
            {
                float newDistance = Vector2.Distance(from, outsidePos);

                if (newDistance < distance)
                    result = outsidePos;
            }

            return result;
        }

        private List<Vector2> GetAllOutsidePos(Vector2 pos)
        {
            float width = 1280;
            float height = 720;

            Vector2 topLeft = new Vector2(pos.X - width, pos.Y + height);
            Vector2 topMiddle = new Vector2(pos.X - 0, pos.Y + height);
            Vector2 topRight = new Vector2(pos.X + width, pos.Y + height);

            Vector2 middleLeft = new Vector2(pos.X - width, pos.Y + 0);
            Vector2 middleRight = new Vector2(pos.X + width, pos.Y + 0);

            Vector2 bottomLeft = new Vector2(pos.X - width, pos.Y - height);
            Vector2 bottomMiddle = new Vector2(pos.X - 0, pos.Y - height);
            Vector2 bottomRight = new Vector2(pos.X + width, pos.Y - height);

            List<Vector2> result = new List<Vector2>();

            result.Add(topLeft);
            result.Add(topMiddle);
            result.Add(topRight);

            result.Add(middleLeft);
            result.Add(middleRight);

            result.Add(bottomLeft);
            result.Add(bottomMiddle);
            result.Add(bottomRight);

            return result;
        }

        private void DrawCursorEveryPos(Vector2 pos)
        {
            foreach(Vector2 outsidePos in GetAllOutsidePos(pos))
            {
                Trajectory.DrawCrosshairAtPos(outsidePos, Color.Black, SceneManager.currentScene.camera.transformMatrix);
            }
        }

        public static void DrawCrosshairAtPos(Vector2 pos, Color color, Matrix matrix)
        {
            Vector2 dimX = new Vector2(20, 4);
            Vector2 dimY = new Vector2(4, 20);

            DrawSimpleShape.DrawRectangleFull((pos * new Vector2(1, -1)) - dimX / 2, dimX, color, matrix);
            DrawSimpleShape.DrawRectangleFull((pos * new Vector2(1, -1)) - dimY / 2, dimY, color, matrix);
        }
    }
}
