using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myEngine;

using ImGuiNET;

using Num = System.Numerics;

using zzMathVisu.myProject;

using zzMathVisu.myProject._02_EarthMap;

namespace zzMathVisu
{
    public class Scene_EarthMap : IScene
    {
        public static bool IsSceneActive = true;

        //FIELDS
        Text t;
        PopUpCoord c;

        //CONSTRUCTOR
        public Scene_EarthMap()
        {
            Settings.BACKGROUND_COLOR = Color.Pink; 

            Sprite s = new Sprite();
            //s.texture = Ressources.Load<Texture2D>("myContent/2D/Utm-zones");
            s.texture = Ressources.Load<Texture2D>("myContent/2D/Map");
            s.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            s.transform.position = new Vector2(0, 0);
            s.isVisible = true;
            s.drawOrder = -1000;

            camControl.isActive = true;

            t = new Text();
            t.color = Color.White;
            c = new PopUpCoord();

            //Spawn Cities
            MVUtil.SpawnPinAtCoords("Paris", Coord.Paris);
            MVUtil.SpawnPinAtCoords("Tokyo", Coord.Tokyo);
            MVUtil.SpawnPinAtCoords("Le Cap", Coord.LeCap);
            MVUtil.SpawnPinAtCoords("Mexico", Coord.Mexico);
            MVUtil.SpawnPinAtCoords("Puntas Arenas", Coord.PuntasArenas);
        }

        //METHODS
        public override void Update()
        {
            Vector2 pos = new Vector2(myEngine.Mouse.position.X, myEngine.Mouse.position.Y);

            pos = Util.ScreenToWorld(camera.transformMatrix, pos);
            t.s = "" + pos;
        }

        public override void DrawGUI()
        {
            c.DrawPopUp();
            DrawRightPanel();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
        }

        float osef = 0;

        public void DrawRightPanel()
        { 
            ImGui.SetNextWindowSize(new Num.Vector2(300, Engine.game.Window.ClientBounds.Height));
            ImGui.SetNextWindowPos(new Num.Vector2(Engine.game.Window.ClientBounds.Width - 300, 0));

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

            ImGui.Text("Properties");

            ImGui.SliderFloat("Slider", ref osef, 0, 100);

            if (ImGui.Button("Set Marker"))
            {
                Console.WriteLine("PRESSED");
            }

            ImGui.End();
        }
    }
}