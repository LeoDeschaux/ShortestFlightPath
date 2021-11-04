using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

using zzMathVisu.myProject;
using zzMathVisu.myProject._03_Earth2DPath;

namespace zzMathVisu
{
    public class Scene_Earth2DPath : MathScene
    {
        public static int numberOfPin = 0;

        PropertiesMenu propertiesMenu;
        PathMenu pathMenu;

        Trajectory t;

        public Scene_Earth2DPath()
        {
            Settings.BACKGROUND_COLOR = Color.LightGoldenrodYellow;

            propertiesMenu = new PropertiesMenu();
            //pathMenu = new PathMenu();

            Sprite s = new Sprite();
            //s.texture = Ressources.Load<Texture2D>("myContent/2D/Utm-zones");
            s.texture = Ressources.Load<Texture2D>("myContent/2D/Map");
            s.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            s.transform.position = new Vector2(0, 0);
            s.isVisible = true;
            s.drawOrder = -1000;

            camControl.isActive = true;

            //Spawn Cities
            //MVUtil.SpawnPinAtCoords("Paris", Coord.Paris);
            //MVUtil.SpawnPinAtCoords("Mexico", Coord.Mexico);

            Marker a = new Marker("MarkerA");
            a.latitude = 70;
            a.longitude = 20;

            Marker b = new Marker("MarkerB");
            b.latitude = -70;
            b.longitude = -30;

            t = new Trajectory(a, b);
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            //DrawTrajectory(Coord.Paris, Coord.Mexico, Color.Red);
            //DrawTrajectory(new Coord(0, 0), new Coord(45, 45), 2, Color.LightGreen);
        }

        public override void DrawGUI()
        {
            base.DrawGUI();
            //propertiesMenu.DrawGUI();
            //pathMenu.DrawGUI();

            ImGui.Begin("DEBUG");

            ImGui.DragFloat("MarkerA.Latitude", ref t.a.latitude);
        }

        
    }
}
