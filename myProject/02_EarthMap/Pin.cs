using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using myEngine;

namespace zzMathVisu.myProject._02_EarthMap
{
    public class Pin
    {
        Button b;

        private Coord c;

        public Pin(String name, Coord c)
        {
            this.c = c;

            b = new Button();
            b.transform.position = MVUtil.ConvertCoordToVector(c.latitude, c.longitude);
            b.text.transform.position = b.transform.position; //* new Vector2(1f, -1f);
            b.sprite.transform.position = b.transform.position;

            b.sprite.dimension = new Vector2(10, 10);
            b.sprite.color = Color.White;

            b.defaultColor = Color.Red;
            b.hoverColor = Color.Red;

            b.sprite.drawOrder = 100;

            b.text.useScreenCoord = false;
            b.text.color = Color.White;

            float osefX = b.text.transform.position.X;
            float osefY = b.text.transform.position.Y;

            b.text.transform.position = new Vector2(osefX, osefY);
            b.sprite.transform.position = new Vector2(osefX, osefY);

            b.text.s = name;

            b.text.drawOrder = b.sprite.drawOrder + 1;

            /*
            Sprite s = new Sprite();
            s.transform.position = Scene_EarthMap.ConvertCoordToVector(coord.latitude, coord.longitude);
            //s.transform.position = new Vector2(positionUI.X, positionUI.Y);
            s.dimension = new Vector2(20, 20);
            s.color = Color.Red;
            s.drawOrder = 100;
            */

            defaultSpriteSize = b.sprite.dimension;

            b.onHoverEnter = new Event(ScaleUp);
            b.onHoverExit = new Event(ScaleDown);

            b.onButtonPressed = new Event(OnPinPressed);
        }

        private Vector2 defaultSpriteSize; 

        private void ScaleUp()
        {
            b.sprite.dimension = defaultSpriteSize * 2;
        }

        private void ScaleDown()
        {
            b.sprite.dimension = defaultSpriteSize;
        }

        private void OnPinPressed()
        {
            Console.WriteLine(b.text.s + ": " + this.c.ToString());
        }
    }
}
