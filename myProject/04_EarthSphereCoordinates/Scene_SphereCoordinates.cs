using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

using ImGuiNET;

using Num = System.Numerics;
using Microsoft.Xna.Framework.Input;

namespace zzMathVisu
{
    public class Scene_SphereCoordinates : IScene
    {
        //FIELDS
        Camera3D camera3D;

        Object3D globe;
        Object3D cursor;

        Text t;

        //CONSTRUCTOR
        public Scene_SphereCoordinates()
        {
            Engine.game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Engine.game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Engine.game.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

            Settings.BACKGROUND_COLOR = Color.AliceBlue;

            Viewport viewPort = new Viewport();
            viewPort.X = 0;
            viewPort.Y = 0;
            viewPort.Width = Settings.SCREEN_WIDTH - 300;
            viewPort.Height = Settings.SCREEN_HEIGHT;
            viewPort.MinDepth = 0;
            viewPort.MaxDepth = 1;

            Engine.renderingEngine.viewPort = viewPort;

            camera3D = new Camera3D(Engine.game, new Vector3(0, 0, -4), Vector3.Zero, 10);
            camera3D.isActive = false;

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
            
            cursor = new Object3D(camera3D);
            cursor.model = Ressources.Load<Model>("myContent/3D/sphere");

            cursor.transform3D.position = new Vector3(0, 0, 0);
            cursor.transform3D.scale = new Vector3(0.02f, 0.02f, 0.02f);

            cursor.effect.DiffuseColor = Color.Red.ToVector3();
            cursor.effect.Alpha = 1f;
            cursor.drawOrder = 0;

            Object3D.GlobalEffect.FogEnabled = true;
            Object3D.GlobalEffect.FogColor = Color.AliceBlue.ToVector3();
            Object3D.GlobalEffect.FogStart = 0f;
            Object3D.GlobalEffect.FogEnd = 20f;

            Object3D.GlobalEffect.LightingEnabled = true;
            Object3D.GlobalEffect.DirectionalLight0.DiffuseColor = new Vector3(1, 1, 1);
            Object3D.GlobalEffect.DirectionalLight0.Direction = new Vector3(1, 0, 0);  
            Object3D.GlobalEffect.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1);

            Object3D.GlobalEffect.AmbientLightColor = Color.AliceBlue.ToVector3();

            t = new Text();

            initPos = camera3D.Position;
        }

        Vector3 initPos;

        Vector2 deltaMouse;

        float speedMouse = 0.5f;

        //METHODS
        public override void Update()
        {
            SetCoordPos();

            if (Input.GetKeyDown(Microsoft.Xna.Framework.Input.Keys.Tab))
            {
                //camera.isActive = camera.isActive == true ? false : true;
            }

            //MOUSE CAM CONTROLE
            if(myEngine.Mouse.position.X <= 980)
            {
                if (Input.GetMouseDown(MouseButtons.Left))
                {
                    deltaMouse = myEngine.Mouse.position.ToVector2();
                }

                if (Input.GetMouse(MouseButtons.Left))
                {
                    //camera.transform.position += ((deltaMouse - myEngine.Mouse.position.ToVector2()) * speedMouse);
                    Vector2 newRot = ((deltaMouse - myEngine.Mouse.position.ToVector2()) * speedMouse);

                    if(camRotX - newRot.Y < 80 && camRotX - newRot.Y > -80)
                        camRotX -= newRot.Y;

                    camRotY += newRot.X;
                    

                    deltaMouse = myEngine.Mouse.position.ToVector2();
                }
            }
            

            //CAM
            Vector3 targetPos = Vector3.Zero;

            camera3D.cameraLookAt = targetPos;

            camera3D.Position = targetPos + Vector3.Transform(initPos - targetPos,
                Matrix.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.ToRadians(camRotX)) *
                Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.ToRadians(camRotY))) * (camDist);
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

            cursor.transform3D.scale = Vector3.One * 0.01f * scale;

            globe.transform3D.rotation = new Vector3(globe.transform3D.rotation.X, sphereRotation, globe.transform3D.rotation.Z);
        }

        private string name = "osef";

        private float scale = 2f;
        private float radius = 1.300f;

        float latitude;
        float longitude;
        float sphereRotation = 83f;

        float camRotX = 0;
        float camRotY = 0;
        float camDist = 1;

        public override void DrawGUI()
        {
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
            ImGui.SliderFloat("Vertical Rotation", ref camRotX, -360f, 360f);
            ImGui.SliderFloat("Horizontal Rotation", ref camRotY, -360f, 360f);
            ImGui.SliderFloat("Distance", ref camDist, 0.3f, 5f);

            if (ImGui.Button("Set Marker"))
            {
                Console.WriteLine("PRESSED");
                SetCoordPos();
            }

            ImGui.End();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
        }
    }
}