using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

using zzMathVisu.myProject;

namespace zzMathVisu
{
    public class Scene_EarthTravel : IScene
    {
        Text t;

        public Scene_EarthTravel()
        {
            Settings.BACKGROUND_COLOR = Color.LightGoldenrodYellow;

            Sprite s = new Sprite();
            //s.texture = Ressources.Load<Texture2D>("myContent/2D/Utm-zones");
            s.texture = Ressources.Load<Texture2D>("myContent/2D/Map");
            s.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            s.transform.position = new Vector2(0, 0);
            s.isVisible = true;
            s.drawOrder = -1000;

            //camControl.isActive = true;

            t = new Text();
            t.color = Color.White;

            //Spawn Cities
            MVUtil.SpawnPinAtCoords("Paris", Coord.Paris);
            MVUtil.SpawnPinAtCoords("Tokyo", Coord.Tokyo);
            MVUtil.SpawnPinAtCoords("Le Cap", Coord.LeCap);
            MVUtil.SpawnPinAtCoords("Mexico", Coord.Mexico);
            MVUtil.SpawnPinAtCoords("Puntas Arenas", Coord.PuntasArenas);

            shapes = new Shapes(Engine.game);
        }

        Shapes shapes;

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            shapes.Begin(this.camera);

            DrawTrajectory(Coord.Paris, Coord.Mexico, Color.Red);

            DrawTrajectory(new Coord(0, 0), new Coord(45, 45), 2, Color.LightGreen);

            shapes.End();
        }

        private void DrawTrajectory(Coord a, Coord b, Color color)
        {
            Vector2 posA = MVUtil.ConvertCoordToVector(a.latitude, a.longitude);
            Vector2 posB = MVUtil.ConvertCoordToVector(b.latitude, b.longitude);

            shapes.DrawLine(posA * new Vector2(1, -1), posB * new Vector2(1, -1), 3, color);
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
    }
}
