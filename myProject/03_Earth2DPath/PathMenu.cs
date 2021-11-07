using System;
using System.Collections.Generic;
using System.Text;

using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;
using zzMathVisu.myProject._02_EarthMap;
using Num = System.Numerics;

namespace zzMathVisu.myProject._03_Earth2DPath
{
    public struct Marker
    {
        public string name;
        public float latitude;
        public float longitude;

        public Marker(string defaultName)
        {
            this.name = defaultName;
            latitude = 0;
            longitude = 0;
        }
    }

    public class PathMenu : GameObject
    {
        //UI VALUES
        Marker marker1;
        Marker marker2;

        Trajectory trajectory;

        bool isPathActive = false;

        //CONSTRUCTOR
        public PathMenu()
        {
            marker1 = new Marker("Marker1");
            marker2 = new Marker("Marker2");
        }

        //METHODS

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            //DRAW RULLER
            /*
            DrawSimpleShape.DrawRullerFree(MVUtil.ConvertCoordToVector(-marker1.latitude, marker1.longitude), matrix);
            DrawSimpleShape.DrawRullerFree(MVUtil.ConvertCoordToVector(-marker2.latitude, marker2.longitude), matrix);
            */

            Vector2 marker1Pos = MVUtil.ConvertCoordToVector(-marker1.latitude, marker1.longitude);
            Vector2 marker2Pos = MVUtil.ConvertCoordToVector(-marker2.latitude, marker2.longitude);

            Trajectory.DrawCrossHaitAtPos(marker1Pos, Color.Red, matrix);
            Trajectory.DrawCrossHaitAtPos(marker2Pos, Color.Red, matrix);
        }

        private byte[] _textBuffer = new byte[100];

        public void DrawGUI()
        {
            /*
            ImGui.SetNextWindowSize(new Num.Vector2(300, 300));
            ImGui.SetNextWindowPos(new Num.Vector2(Settings.SCREEN_WIDTH/2 - 150, Settings.SCREEN_HEIGHT/2 - 150));

            ImGuiWindowFlags window_flags = 0;

            //window_flags |= ImGuiWindowFlags.NoResize;
            //window_flags |= ImGuiWindowFlags.NoCollapse;
            //window_flags |= ImGuiWindowFlags.NoMove;
            ImGui.Begin("WINDOW", window_flags);
            */

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

            ImGui.SetNextWindowSize(new Num.Vector2(300, 300));
            ImGui.SetNextWindowPos(new Num.Vector2(Settings.SCREEN_WIDTH - 300, 0));

            ImGui.Begin("NAME", window_flags);

            ImGui.Text("Choose markers' position : ");

            ImGui.PushID("Marker1");
            ImGui.Text("Marker n°1");
            ImGui.InputText("Name", ref marker1.name, 100);

            ImGui.SliderFloat("Latitude", ref marker1.latitude, -90f, 90f);
            ImGui.SliderFloat("Longitude", ref marker1.longitude, -180f, 180f);

            ImGui.Separator();

            ImGui.PushID("Marker2");
            ImGui.Text("Marker n°2");
            ImGui.InputText("Name", ref marker2.name, 100);

            ImGui.SliderFloat("Latitude", ref marker2.latitude, -90f, 90f);
            ImGui.SliderFloat("Longitude", ref marker2.longitude, -180f, 180f);


            uint test = 1;

            if(isPathActive)
            {
                SetStyleButtonDisabled(true);

                if (ImGui.Button("Set Markers"))
                {
                }

                SetStyleButtonDisabled(false);

                if (ImGui.Button("Delete Markers"))
                {
                    DeleteMarkers();
                    trajectory.Destroy();
                    isPathActive = false;
                }
            }
            else
            {
                if (ImGui.Button("Set Markers"))
                {
                    if(Scene_Earth2DPath.numberOfPin != 0)
                    {
                        Console.WriteLine("NUMBER OF PINS > 0");
                        return;
                    }

                    MVUtil.SpawnPinAtCoords(marker1.name, new Coord(marker1.latitude, marker1.longitude));
                    MVUtil.SpawnPinAtCoords(marker2.name, new Coord(marker2.latitude, marker2.longitude));

                    trajectory = new Trajectory(marker1, marker2);

                    isPathActive = true;
                }


                SetStyleButtonDisabled(true);

                if (ImGui.Button("Delete Markers"))
                {
                }

                SetStyleButtonDisabled(false);

            }

            ImGui.End();
        }

        private void SetStyleButtonDisabled(bool value)
        {
            if(value)
            {
                ImGui.PushStyleColor(ImGuiCol.Button, new Num.Vector4(0.2f, 0.2f, 0.2f, 255));
                ImGui.PushStyleColor(ImGuiCol.ButtonActive, new Num.Vector4(0.2f, 0.2f, 0.2f, 255));
                ImGui.PushStyleColor(ImGuiCol.ButtonHovered, new Num.Vector4(0.2f, 0.2f, 0.2f, 255));
                ImGui.PushStyleColor(ImGuiCol.Text, new Num.Vector4(0.8f, 0.8f, 0.8f, 255));
            }
            else
            {
                ImGui.PopStyleColor();
                ImGui.PopStyleColor();
                ImGui.PopStyleColor();
                ImGui.PopStyleColor();
            }
        }

        private void DeleteMarkers()
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.Destroy();
            }
        }
    }
}
