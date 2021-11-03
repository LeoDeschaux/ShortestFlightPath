using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using myEngine;


using Num = System.Numerics;

using zzMathVisu.myProject;

using zzMathVisu.myProject._02_EarthMap;

namespace zzMathVisu
{
    public class Scene_EarthMap : MathScene
    {
        public static bool IsSceneActive = true;

        //FIELDS
        Text t;

        TopMenu topMenu;
        PopUpCoord c;
        PropertiesMenu propertiesMenu;

        //CONSTRUCTOR
        public Scene_EarthMap()
        {
            Settings.BACKGROUND_COLOR = Color.Pink;

            propertiesMenu = new PropertiesMenu();

            camControl.isActive = true;

            //camControl.boundingBox = new Rectangle(0, 0, 300, 0);

            Sprite s = new Sprite();
            //s.texture = Ressources.Load<Texture2D>("myContent/2D/Utm-zones");
            s.texture = Ressources.Load<Texture2D>("myContent/2D/Map");
            s.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            s.transform.position = new Vector2(0, 0);
            s.isVisible = true;
            s.drawOrder = -1000;

            t = new Text();
            t.color = Color.White;
            t.transform.position = new Vector2(0, Settings.VIEWPORT_HEIGHT - t.GetRectangle().Height);

            c = new PopUpCoord();
            c.drawOrder = 1000;

            //Spawn Cities
            MVUtil.SpawnPinAtCoords("Paris", Coord.Paris);
            MVUtil.SpawnPinAtCoords("Tokyo", Coord.Tokyo);
            MVUtil.SpawnPinAtCoords("Le Cap", Coord.LeCap);
            MVUtil.SpawnPinAtCoords("Mexico", Coord.Mexico);
            MVUtil.SpawnPinAtCoords("Puntas Arenas", Coord.PuntasArenas);
        }

        //METHODS
        float previousScrollValue;
        MouseState currentMouseState;

        public override void Update()
        {
            Vector2 pos = new Vector2(myEngine.Mouse.position.X, myEngine.Mouse.position.Y);

            pos = Util.ScreenToWorld(camera.transformMatrix, pos);
            t.s = "" + pos;

            currentMouseState = Microsoft.Xna.Framework.Input.Mouse.GetState();

            if (currentMouseState.ScrollWheelValue < previousScrollValue)
            {
                camera.ZoomAt(Util.ScreenToWorld(camera.transformMatrix, new Vector2((1280-300) / 2, 720 / 2)), -0.5f);
            }
            else if (currentMouseState.ScrollWheelValue > previousScrollValue)
            {
                camera.ZoomAt(Util.ScreenToWorld(camera.transformMatrix, new Vector2((1280 - 300) / 2, 720 / 2)), 0.5f);
            }
            previousScrollValue = currentMouseState.ScrollWheelValue;
        }

        public override void DrawGUI()
        {
            base.DrawGUI();
            propertiesMenu.DrawRightPanel();
            c.DrawPopUp();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {

        }
    }
}