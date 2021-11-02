using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using myEngine;

namespace zzMathVisu.myProject._02_EarthMap
{
    public class PinManager
    {
        private static PinManager instance;

        public static PinManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new PinManager();

                return instance;
            }
        }

        private static List<Pin> pins;

        private PinManager()
        {
            pins = new List<Pin>();
        }

        public void AddPin(Pin pin)
        {
            pins.Add(pin);
        }

        public List<Pin> GetPins()
        {
            return pins;
        }
    }

    public class Pin
    {
        public Button button;
        private Coord c;

        public Pin(String name, Coord c)
        {
            PinManager.Instance.AddPin(this);

            this.c = c;

            button = new Button();
            button.transform.position = MVUtil.ConvertCoordToVector(c.latitude, c.longitude);
            button.text.transform.position = button.transform.position; //* new Vector2(1f, -1f);
            button.sprite.transform.position = button.transform.position;

            button.sprite.dimension = new Vector2(10, 10);
            button.sprite.color = Color.White;

            button.defaultColor = Color.Red;
            button.hoverColor = Color.Red;

            button.sprite.drawOrder = 100;

            button.text.useScreenCoord = false;
            button.text.color = Color.White;

            float osefX = button.text.transform.position.X;
            float osefY = button.text.transform.position.Y;

            button.text.transform.position = new Vector2(osefX, osefY);
            button.sprite.transform.position = new Vector2(osefX, osefY);

            button.text.s = name;

            button.text.drawOrder = button.sprite.drawOrder + 1;

            /*
            Sprite s = new Sprite();
            s.transform.position = Scene_EarthMap.ConvertCoordToVector(coord.latitude, coord.longitude);
            //s.transform.position = new Vector2(positionUI.X, positionUI.Y);
            s.dimension = new Vector2(20, 20);
            s.color = Color.Red;
            s.drawOrder = 100;
            */

            defaultSpriteSize = button.sprite.dimension;

            button.onHoverEnter = new Event(ScaleUp);
            button.onHoverExit = new Event(ScaleDown);

            button.onButtonPressed = new Event(OnPinPressed);
        }

        private Vector2 defaultSpriteSize; 

        private void ScaleUp()
        {
            defaultSpriteSize = button.sprite.dimension;
            button.sprite.dimension *= 2;
        }

        private void ScaleDown()
        {
            button.sprite.dimension = defaultSpriteSize;
        }

        private void OnPinPressed()
        {
            Console.WriteLine(button.text.s + ": " + this.c.ToString());
        }
    }
}
