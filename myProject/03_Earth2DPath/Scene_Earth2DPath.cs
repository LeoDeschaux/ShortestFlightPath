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

        Text coordsCenter;

        public Scene_Earth2DPath()
        {
            Settings.BACKGROUND_COLOR = Color.LightGoldenrodYellow;

            propertiesMenu = new PropertiesMenu();
            //pathMenu = new PathMenu();

            Sprite s = new Sprite();
            s.texture = Ressources.Load<Texture2D>("myContent/2D/Map");
            s.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            s.transform.position = new Vector2(0, 0);
            s.isVisible = true;
            s.drawOrder = -1000;

            camControl.isActive = true;

            camera.ZoomOut(0);

            //Spawn Cities

            Marker a = new Marker("MarkerA");
            a.latitude = -70;
            a.longitude = -30;

            Marker b = new Marker("MarkerB");
            b.latitude = 70;
            b.longitude = 30;

            t = new Trajectory(a, b);

            coordsCenter = new Text();
            coordsCenter.color = Color.Black;
            coordsCenter.transform.position = new Vector2(0, 720/2);
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            //DrawTrajectory(new Vector2(-150, 150), new Vector2(400, -200), 5, Color.Red);
            
            DrawTrajectory(MVUtil.ConvertCoordToVector(t.a.latitude, t.a.longitude), MVUtil.ConvertCoordToVector(t.b.latitude, t.b.longitude), 5, Color.Red);
        }

        private void DrawTrajectory(Vector2 start, Vector2 end, int segmentation, Color color)
        {
            /*
            for (int i = 0; i < segmentation; i++)
            {
                Vector2 point1 = start + (((end - start) / segmentation) * i);
                Vector2 point2 = start + (((end - start) / segmentation) * (i + 1));

                Color c = new Color((float)(i) / (float)segmentation, (float)(i) / (float)segmentation, (float)(i) / (float)segmentation);
                DrawSimpleShape.DrawLine(point1, point2, c, thickness: 5, matrix: SceneManager.currentScene.camera.transformMatrix);
            }
            */

            /*
            Vector2 point1 = start;
            Vector2 point2 = end;
            Vector2 center = MVUtil.ConvertVectorToCoord(point1) + (MVUtil.ConvertVectorToCoord(point2) - MVUtil.ConvertVectorToCoord(point1) /2);

            center = MVUtil.ConvertCoordToVector(center.X, center.Y);
            */

            //DrawCapConstant(MVUtil.ConvertVectorToCoord(start), MVUtil.ConvertVectorToCoord(end));

            //DrawSimpleShape.DrawLine(point1, center, Color.Black, thickness: 5, matrix: SceneManager.currentScene.camera.transformMatrix);
            //DrawSimpleShape.DrawLine(center, point2, Color.Red, thickness: 5, matrix: SceneManager.currentScene.camera.transformMatrix);
        }

        private void DrawTrajectoryOld(Vector2 start, Vector2 end, int segmentation, Color color)
        {
            for (int i = 0; i < segmentation; i++)
            {
                Vector2 point1 = start + (((end - start) / segmentation) * i);
                Vector2 point2 = start + (((end - start) / segmentation) * (i + 1));

                Color c = new Color((float)(i) / (float)segmentation, (float)(i) / (float)segmentation, (float)(i) / (float)segmentation);
                DrawSimpleShape.DrawLine(point1, point2, c, thickness: 5, matrix: SceneManager.currentScene.camera.transformMatrix);
            }
        }

        private void DrawCapConstant(Vector2 start, Vector2 end)
        {
            Vector2 point1 = new Vector2(MathHelper.ToRadians(start.X), MathHelper.ToRadians(start.Y));
            Vector2 point2 = new Vector2(MathHelper.ToRadians(end.X), MathHelper.ToRadians(end.Y));

            double distanceB = Math.Acos(Math.Sin(point1.X) * Math.Sin(point2.X) +
                Math.Cos(point1.X) * Math.Cos(point2.X) *
                Math.Cos(point1.Y - point2.Y));

            double v = ( Math.Sin(point2.X) - Math.Sin(point1.X) * Math.Cos(distanceB) ) /
                (Math.Cos(point1.X) * Math.Sin(distanceB));

            double a = Math.Acos(v);

            float q = (float)Math.PI / (float)(1.852 * 60 * 180);

            float capLength = 10;

            float newLat = point1.X + (float)Math.Cos(a) * capLength * q * (720/2);
            float newLong = point1.Y + ((float)(Math.Sin(a) / Math.Cos(point1.X)) * capLength * q * (1280/2));

            Vector2 newPos = MVUtil.ConvertCoordToVector(MathHelper.ToDegrees(newLat), MathHelper.ToDegrees(newLong));
            coordsCenter.s = newPos.ToString();

            Trajectory.DrawCrosshairAtPos(newPos, Color.Red, SceneManager.currentScene.camera.transformMatrix);
        }

        public override void DrawGUI()
        {
            base.DrawGUI();
            //propertiesMenu.DrawGUI();
            //pathMenu.DrawGUI();

            ImGui.SetNextWindowSize(new Num.Vector2(300, Settings.SCREEN_HEIGHT));
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
