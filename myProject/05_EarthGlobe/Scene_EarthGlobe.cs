using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;
using zzMathVisu.myProject._04_EarthSphereCoordinates;
using zzMathVisu.myProject._05_EarthGlobe;
using Num = System.Numerics;

namespace zzMathVisu
{
    public class Scene_EarthGlobe : MathScene
    {
        //FIELDS
        Camera3D camera3D;
        Object3D globe;

        SphereTrajectory trajectory;
        CameraController controller;

        //CONSTRUCTOR
        public Scene_EarthGlobe()
        {
            Engine.game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Engine.game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Engine.game.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            Settings.BACKGROUND_COLOR = Color.AliceBlue;

            camera3D = new Camera3D(Engine.game, new Vector3(0, 0, -4), Vector3.Zero, 10);
            camera3D.isActive = false;

            controller = new CameraController(camera3D);

            Floor f = new Floor(Engine.game.GraphicsDevice, camera3D, 4, 4, 1);
            f.transform3D.position = new Vector3(0, -3, 0);

            globe = new Object3D(camera3D);

            globe.model = Ressources.Load<Model>("myContent/3D/earthV2/source/Earth");
            globe.effect.Texture = Ressources.Load<Texture2D>("myContent/3D/earthV2/textures/1_earth_8k");
            globe.effect.TextureEnabled = true;

            globe.transform3D.position = new Vector3(0, 0, 0);
            globe.transform3D.rotation.X = -90;
            globe.transform3D.scale = new Vector3(1, 1, 1);
            globe.effect.Alpha = 1f;

            globe.drawOrder = 10;

            globe.isVisible = false;

            trajectory = new SphereTrajectory
            (
                camera3D,
                new Cursor3D(camera3D, 90, 0),
                new Cursor3D(camera3D, 45, -45)
            );

            points = new List<Cursor3D>();
            CreateArray(36);

            greatCircle = new List<Cursor3D>();
            CreateGreatCircle(360);
        }

        List<Cursor3D> points;
        List<Cursor3D> greatCircle;
        Vector2 greatCircleRotation = new Vector2(0, 90);

        private void CreateGreatCircle(int amountOfPoints)
        {
            foreach (Cursor3D c in greatCircle)
                c.Destroy();

            greatCircle = new List<Cursor3D>();

            int amount = amountOfPoints;

            //VERTICAL 
            for (int i = 0; i < amount; i++)
            {
                Vector2 coords = new Vector2(greatCircleRotation.X + (360 / amount) * i, greatCircleRotation.Y);
                greatCircle.Add(new Cursor3D(camera3D, coords.X, coords.Y));
            }
        }

        private void CreateArray(int amountOfPoints)
        {
            foreach (Cursor3D c in points)
                c.Destroy();

            points = new List<Cursor3D>();

            int amount = amountOfPoints;

            //VERTICAL 
            for (int i = 0; i < amount; i++)
            {
                Vector2 coords = new Vector2((360/amount) * i, 0);
                points.Add(new Cursor3D(camera3D, coords.X, coords.Y));
            }

            //VERTICAL + 90
            for (int i = 0; i < amount; i++)
            {
                Vector2 coords = new Vector2((360 / amount) * i, 90);
                points.Add(new Cursor3D(camera3D, coords.X, coords.Y));
            }

            //HORIZONTAL
            for (int i = 0; i < amount; i++)
            {
                Vector2 coords = new Vector2(90, (360 / amount) * i);
                points.Add(new Cursor3D(camera3D, coords.X, coords.Y));
            }
        }

        public override void Update()
        {
            /*
            int amount = points.Count;

            for (int i = 0; i < amount; i++)
            {
                Vector2 coords = new Vector2((360 / amount) * i, 0);
                points[i].SetPos(coords.X, coords.Y);
            }

            for (int i = amount; i < amount + amount; i++)
            {
                Vector2 coords = new Vector2(90, (360 / amount) * i);
                points[i].SetPos(coords.X, coords.Y);
            }
            */

            for (int i = 0; i < greatCircle.Count; i++)
            {
                Vector2 coords = new Vector2(greatCircleRotation.X, (360 / greatCircle.Count) * i);
                greatCircle[i].SetPos(coords.X, coords.Y);
            }

            //greatCircleRotation += 180 * Time.deltaTime;
        }

        private float scale = 2f;
        private float radius = 1.300f;

        float sphereRotation = 83f;

        public override void DrawGUI()
        {
            base.DrawGUI();

            ImGui.SetNextWindowSize(new Num.Vector2(300, Settings.SCREEN_HEIGHT));
            ImGui.SetNextWindowPos(new Num.Vector2((Settings.SCREEN_WIDTH - 300), 0));

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
            //ImGui.Begin("NAME");

            ImGui.Text("Cursor Coordinates");

            //ImGui.InputText("Name", ref name, 100);

            ImGui.SliderFloat("Latitude", ref trajectory.b.latitude, -180, 180f);
            ImGui.SliderFloat("Longitude", ref trajectory.b.longitude, -180f, 180f);

            //
            ImGui.Text("Cursor Properties");
            ImGui.SliderFloat("Scale", ref scale, 0, 10f);
            ImGui.SliderFloat("Radius", ref radius, 0, 3);

            ImGui.Text("Earth Properties");
            ImGui.SliderFloat("Earth Rotation", ref sphereRotation, -180f, 180f);

            ImGui.Text("Camera Properties");
            ImGui.SliderFloat("Vertical Rotation", ref controller.camRotX, -80, 80);
            ImGui.SliderFloat("Horizontal Rotation", ref controller.camRotY, -360f, 360f);
            ImGui.SliderFloat("Distance", ref controller.camDist, 0.3f, 3f);

            //ImGui.PushItemWidth(250);

            ImGui.TextDisabled(points.Count.ToString());

            ImGui.SameLine();

            if (ImGui.Button("-"))
            {
                if(points.Count > 3)
                {
                    CreateArray(points.Count - 1);
                }
            }

            ImGui.SameLine();

            if (ImGui.Button("+"))
            {
                CreateArray(points.Count + 1);
            }

            ImGui.DragFloat("GreatCircle Rot X", ref greatCircleRotation.X);
            ImGui.DragFloat("GreatCircle Rot Y", ref greatCircleRotation.Y);

            ImGui.End();
        }
    }
}
