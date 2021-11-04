using ImGuiNET;
using Microsoft.Xna.Framework;
using myEngine;
using System;
using System.Collections.Generic;
using System.Text;
using zzMathVisu.myProject._02_EarthMap;
using Num = System.Numerics;

namespace zzMathVisu.myProject._03_Earth2DPath
{
    public class PropertiesMenu
    {
        //PINS
        bool showPins = true;
        bool showText = true;
        bool showSprites = true;

        //PINS TEXT
        int fontSize = 32;
        Num.Vector3 textColor = new Num.Vector3(0, 0, 0);

        //PINS SPRITE
        int spriteSize = 30;
        Num.Vector3 spriteColor = new Num.Vector3(255, 0, 0);

        //GRID
        bool showGrid = false;
        //GRID COLOR
        //GRID SIZE
        //

        public PropertiesMenu()
        {

        }

        public void DrawGUI()
        {
            //ImGui.SetNextWindowSize(new Num.Vector2(300, Engine.game.Window.ClientBounds.Height));
            //ImGui.SetNextWindowPos(new Num.Vector2(Engine.game.Window.ClientBounds.Width - 300, 0));

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

            ImGui.Text("Pins' Properties");

            if (ImGui.Checkbox("Show pins", ref showPins))
            {
                SetPinsVisible(showPins);
            }
            if (ImGui.Checkbox("Show Text", ref showText))
            {
                SetTextVisible(showText);
            }
            if (ImGui.Checkbox("Show Sprites", ref showSprites))
            {
                SetSpritesVisible(showSprites);
            }

            ImGui.Separator();

            ImGui.Text("Text Properties");
            if (ImGui.InputInt("Font Size", ref fontSize))
            {
                SetFontSize(fontSize);
            }

            if (ImGui.ColorEdit3("Text Color", ref textColor))
            {
                SetTextColor(new Color(textColor.X, textColor.Y, textColor.Z));
            }

            ImGui.Separator();

            ImGui.Text("Sprites Properties");
            if (ImGui.InputInt("Sprite Size", ref spriteSize))
            {
                SetSpriteSize(spriteSize);
            }

            if (ImGui.ColorEdit3("Sprite Color", ref spriteColor))
            {
                SetSpriteColor(new Color(spriteColor.X, spriteColor.Y, spriteColor.Z));
            }

            ImGui.Separator();

            ImGui.Text("Grid Properties");

            if (ImGui.Checkbox("Show Grid", ref showGrid))
            {
                Console.WriteLine("osef");
            }

            //grid size ?
            //grid cells ?
            //grid color ?

            ImGui.Separator();

            ImGui.Text("Camera Propsertie");

            ImGui.DragFloat("Camera Horizontal", ref SceneManager.currentScene.camera.transform.position.X);

            /*
            if (ImGui.DragFloat("Camera Horizontal", ref SceneManager.currentScene.camera.transform.position.X))
            {
                SetCameraAtPos()
            }
            */

            ImGui.Separator();

            if (ImGui.Button("Re-Center Map"))
            {
                SetCameraAtPos(Vector2.Zero);
            }

            if (ImGui.Button("Delete all pins"))
            {
                //SceneManager.ReloadScene();
                //return;
                DeleteAllPins();
            }

            if (ImGui.Button("Spawn default pins"))
            {
                SpawnDefaultPins();
            }

            ImGui.End();
        }

        private void SetPinsVisible(bool value)
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.button.isVisible = value;
                p.button.text.isVisible = value;
                p.button.sprite.isVisible = value;
            }
        }

        private void SetTextVisible(bool value)
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.button.text.isVisible = value;
            }
        }

        private void SetSpritesVisible(bool value)
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.button.sprite.isVisible = value;
            }
        }

        private void SetFontSize(int value)
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.button.text.fontSize = value;
            }
        }

        private void SetTextColor(Color color)
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.button.text.color = color;
            }
        }

        private void SetSpriteSize(int value)
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.button.sprite.dimension = new Vector2(value, value);
            }
        }

        private void SetSpriteColor(Color color)
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.button.defaultColor = color;
                p.button.hoverColor = color;
            }
        }

        private void SetGridVisible(bool value)
        {
            Console.WriteLine("NOT IMPLEMETNED YET");
        }

        private void SetCameraAtPos(Vector2 pos)
        {
            SceneManager.currentScene.camera.transform.position = pos + new Vector2(0, 0);
        }

        private void DeleteAllPins()
        {
            List<Pin> pins = PinManager.Instance.GetPins();

            foreach (Pin p in pins)
            {
                p.Destroy();
            }
        }

        private void SpawnDefaultPins()
        {
            MVUtil.SpawnPinAtCoords("Paris", Coord.Paris);
            MVUtil.SpawnPinAtCoords("Tokyo", Coord.Tokyo);
            MVUtil.SpawnPinAtCoords("Le Cap", Coord.LeCap);
            MVUtil.SpawnPinAtCoords("Mexico", Coord.Mexico);
            MVUtil.SpawnPinAtCoords("Puntas Arenas", Coord.PuntasArenas);
        }
    }
}