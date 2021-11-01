using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;
using System;
using Num = System.Numerics;

using zzMathVisu.myProject;

using zzMathVisu.myProject._02_EarthMap;

namespace zzMathVisu
{
    public class PopUpCoord : GameObject
    {
        //FIELDS
        ImGuiRenderer renderer;

        //UI VALUES
        private static float latitude;
        private static float longitude;

        private static string name;

        //CONSTRUCTOR
        public PopUpCoord()
        {
            renderer = new ImGuiRenderer(Engine.game);
            renderer.RebuildFontAtlas();

            name = "Default_Name";
        }

        //METHODS

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            //DrawPopUp();
            DrawSimpleShape.DrawRullerFree(MVUtil.ConvertCoordToVector(-latitude, longitude), matrix);
        }

        private byte[] _textBuffer = new byte[100];

        public void DrawPopUp()
        {
            /*
            ImGui.SetNextWindowSize(new Num.Vector2(300, 300));
            ImGui.SetNextWindowPos(new Num.Vector2(Settings.SCREEN_WIDTH/2 - 150, Settings.SCREEN_HEIGHT/2 - 150));

            ImGui.GetStyle().WindowRounding = 0.0f;
            ImGui.GetStyle().ChildRounding = 0.0f;
            ImGui.GetStyle().FrameRounding = 0.0f;
            ImGui.GetStyle().GrabRounding = 0.0f;
            ImGui.GetStyle().PopupRounding = 0.0f;
            ImGui.GetStyle().ScrollbarRounding = 0.0f;

            ImGuiWindowFlags window_flags = 0;

            //window_flags |= ImGuiWindowFlags.NoResize;
            //window_flags |= ImGuiWindowFlags.NoCollapse;
            //window_flags |= ImGuiWindowFlags.NoMove;
            ImGui.Begin("WINDOW", window_flags);
            */

            ImGui.Begin("NAME");

            ImGui.Text("Choose your marker position : ");

            ImGui.InputText("Name", ref name, 100);
            //ImGui.InputText("Name", _textBuffer, 100);

            ImGui.SliderFloat("Latitude", ref latitude, -90f, 90f);
            ImGui.SliderFloat("Longitude", ref longitude, -180f, 180f);

            if (ImGui.Button("Set Marker"))
            {
                Console.WriteLine("PRESSED");

                //Scene_EarthMap.SpawnPinAtCoords(System.Text.Encoding.UTF8.GetString(_textBuffer, 0, _textBuffer.Length), new Coord(latitude, longitude));
                MVUtil.SpawnPinAtCoords(name, new Coord(latitude, longitude));
            }

            ImGui.End();
        }
    }
}
