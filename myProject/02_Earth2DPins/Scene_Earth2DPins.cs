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
    public class Scene_Earth2DPins : MathScene
    {
        public static bool IsSceneActive = true;

        //FIELDS
        Text t;

        TopMenu topMenu;
        PopUpCoordMenu c;
        PropertiesMenu propertiesMenu;

        //CONSTRUCTOR
        public Scene_Earth2DPins()
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

            c = new PopUpCoordMenu();
            c.drawOrder = 1000;

            //Spawn Cities
            MVUtil.SpawnPinAtCoords("Paris", Coord.Paris);
            MVUtil.SpawnPinAtCoords("Tokyo", Coord.Tokyo);
            MVUtil.SpawnPinAtCoords("Le Cap", Coord.LeCap);
            MVUtil.SpawnPinAtCoords("Mexico", Coord.Mexico);
            MVUtil.SpawnPinAtCoords("Puntas Arenas", Coord.PuntasArenas);
        }

        public override void Update()
        {
            Vector2 pos = new Vector2(myEngine.Mouse.position.X, myEngine.Mouse.position.Y);

            pos = Util.ScreenToWorld(camera.transformMatrix, pos);
            t.s = "" + pos;
        }

        public override void DrawGUI()
        {
            base.DrawGUI();
            propertiesMenu.DrawRightPanel();
            c.DrawGUI();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            //DrawSimpleShape.DrawRuller(myEngine.Mouse.position.ToVector2());
        }
    }
}