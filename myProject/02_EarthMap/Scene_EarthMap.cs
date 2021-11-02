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
    public class Scene_EarthMap : IScene
    {
        public static bool IsSceneActive = true;

        //FIELDS
        Text t;
        PopUpCoord c;
        PropertiesMenu propertiesMenu;

        //CONSTRUCTOR
        public Scene_EarthMap()
        {
            Settings.BACKGROUND_COLOR = Color.Pink;

            Viewport viewPort = new Viewport();
            viewPort.X = 0;
            viewPort.Y = 0;
            viewPort.Width = Settings.SCREEN_WIDTH - 300;
            viewPort.Height = Settings.SCREEN_HEIGHT;
            viewPort.MinDepth = 0;
            viewPort.MaxDepth = 1;

            Engine.renderingEngine.viewPort = viewPort;

            propertiesMenu = new PropertiesMenu();

            camera.transform.position.X += 150;
            Console.WriteLine(camera.transform.position);

            Sprite s = new Sprite();
            //s.texture = Ressources.Load<Texture2D>("myContent/2D/Utm-zones");
            s.texture = Ressources.Load<Texture2D>("myContent/2D/Map");
            s.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            s.transform.position = new Vector2(0, 0);
            s.isVisible = true;
            s.drawOrder = -1000;

            Sprite mapClone = new Sprite();
            //s.texture = Ressources.Load<Texture2D>("myContent/2D/Utm-zones");
            mapClone.texture = Ressources.Load<Texture2D>("myContent/2D/Map");
            mapClone.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            mapClone.transform.position = new Vector2(mapClone.dimension.X, 0);
            mapClone.isVisible = true;
            mapClone.drawOrder = -1000;

            Sprite mapClone2 = new Sprite();
            //s.texture = Ressources.Load<Texture2D>("myContent/2D/Utm-zones");
            mapClone2.texture = Ressources.Load<Texture2D>("myContent/2D/Map");
            mapClone2.dimension = new Vector2(Settings.SCREEN_WIDTH, Settings.SCREEN_HEIGHT);
            mapClone2.transform.position = new Vector2(0, mapClone.dimension.Y);
            mapClone2.isVisible = true;
            mapClone2.drawOrder = -1000;
            mapClone2.transform.rotation = -180;

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
            propertiesMenu.DrawRightPanel();
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {

        }
    }
}