using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

using ImGuiNET;

using Num = System.Numerics;
using Microsoft.Xna.Framework.Input;
using zzMathVisu.myProject._04_EarthSphereCoordinates;

namespace zzMathVisu
{
    public class Scene_SphereCoordinates : MathScene
    {
        //FIELDS
        Camera3D camera3D;
        Object3D globe;
        Cursor3D cursor;

        CameraController controller;

        Text t;

        //CONSTRUCTOR
        public Scene_SphereCoordinates()
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
            
            cursor = new Cursor3D(camera3D);

            Vector2 a = new Vector2(90, 0);
            Vector2 b = new Vector2(45, -45);

            SpawnCursor(a.X, a.Y, Color.Red);
            SpawnCursor(b.X, b.Y, Color.Red);

            /*
            float x = 90 - 45 / 2;
            float y = 0 - 45 / 2;
            SpawnCursor(x, y, Color.Yellow);
            */

            SpawnTrajectory(a, b, 30);

            t = new Text();
        }

        private void SpawnTrajectory(Vector2 start, Vector2 end, float amount)
        {

            for(int i = 0; i < amount; i++)
            {
                Vector2 pos = start - ((end / (amount+1)) * (1 + i));
                SpawnCursor(pos.X, -pos.Y, Color.Green);
            }
        }

        //METHODS
        public override void Update()
        {
            SetCoordPos();
        }

        private Vector3 RotateAround(float latitude, float longitude)
        {
            latitude = latitude % 360;
            longitude = longitude % 360;

            float posX = (float)Math.Sin(MathHelper.ToRadians(-latitude)) *
                         (float)Math.Sin(MathHelper.ToRadians(longitude));

            float posY = (float)Math.Cos(MathHelper.ToRadians(-latitude));

            float posZ = (float)Math.Sin(MathHelper.ToRadians(-latitude)) *
                         (float)Math.Cos(MathHelper.ToRadians(longitude));

            return new Vector3(posX, posY, posZ);
        }

        private void SetCoordPos()
        {
            t.s = "" + Math.Floor(-latitude) + ", " + Math.Floor(longitude);
            
            cursor.transform3D.position = RotateAround(latitude, longitude) * 0.8f * radius;
            cursor.transform3D.scale = Vector3.One * 0.02f * scale;
            globe.transform3D.rotation = new Vector3(globe.transform3D.rotation.X, sphereRotation, globe.transform3D.rotation.Z);
        }

        private float scale = 2f;
        private float radius = 1.300f;

        float latitude;
        float longitude;
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

            ImGui.SliderFloat("Latitude", ref latitude, -180, 180f);
            ImGui.SliderFloat("Longitude", ref longitude, -180f, 180f);

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

            //CURSOR SPAWNNER
            if (ImGui.Button("Spawn Cursor"))
            {
                SpawnCursor(latitude, longitude, Color.Red);
            }

            ImGui.End();
        }

        private void SpawnCursor(float latitude, float longitude, Color color)
        {
            Cursor3D c = new Cursor3D(camera3D);
            c.transform3D.position = RotateAround(latitude, longitude) * 0.8f * radius;
            c.transform3D.scale = Vector3.One * 0.01f * scale;
            c.effect.DiffuseColor = color.ToVector3();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
        }

        private void SetScene()
        {
            Object3D.GlobalEffect.FogEnabled = true;
            Object3D.GlobalEffect.FogColor = Color.AliceBlue.ToVector3();
            Object3D.GlobalEffect.FogStart = 0f;
            Object3D.GlobalEffect.FogEnd = 20f;

            Object3D.GlobalEffect.LightingEnabled = true;
            Object3D.GlobalEffect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
            Object3D.GlobalEffect.DirectionalLight0.Direction = new Vector3(1, 0, 0);
            Object3D.GlobalEffect.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1);

            Object3D.GlobalEffect.AmbientLightColor = Color.AliceBlue.ToVector3();
        }
    }
}