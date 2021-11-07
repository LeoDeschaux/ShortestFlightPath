using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

using zzMathVisu.myProject;
using zzMathVisu.myProject._03_Earth2DPath;

using Num = System.Numerics;

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

            camera.ZoomOut(0);

            //Spawn Cities
            //MVUtil.SpawnPinAtCoords("Paris", Coord.Paris);
            //MVUtil.SpawnPinAtCoords("Mexico", Coord.Mexico);

            Marker a = new Marker("MarkerA");
            a.latitude = -70;
            a.longitude = -30;

            Marker b = new Marker("MarkerB");
            b.latitude = 70;
            b.longitude = 30;

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

            ImGui.SetNextWindowSize(new Num.Vector2(300, Settings.SCREEN_HEIGHT - 300));
            ImGui.SetNextWindowPos(new Num.Vector2(Settings.SCREEN_WIDTH - 300, 0));

            ImGui.GetStyle().WindowRounding = 0.0f;
            ImGui.GetStyle().ChildRounding = 0.0f;
            ImGui.GetStyle().FrameRounding = 0.0f;
            ImGui.GetStyle().GrabRounding = 0.0f;
            ImGui.GetStyle().PopupRounding = 0.0f;
            ImGui.GetStyle().ScrollbarRounding = 0.0f;

            ImGuiWindowFlags window_flags = 0;

            window_flags |= ImGuiWindowFlags.NoResize;
            window_flags |= ImGuiWindowFlags.NoCollapse;
            window_flags |= ImGuiWindowFlags.NoMove;

            ImGui.SetNextWindowBgAlpha(1);

            ImGui.Begin("WINDOW", window_flags);

            ImGui.PushID("MarkerA");
            ImGui.Text("Marker A");
            ImGui.DragFloat("Latitude", ref t.a.latitude);
            ImGui.DragFloat("Longitude", ref t.a.longitude);
            ImGui.PopID();

            ImGui.Text("Marker B");
            ImGui.DragFloat("Latitude", ref t.b.latitude);
            ImGui.DragFloat("Longitude", ref t.b.longitude);

            ImGui.End();
        }

        
    }
}
