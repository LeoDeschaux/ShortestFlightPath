using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using myEngine;

namespace zzMathVisu
{
    public class Scene_Test : IScene
    {
        public Scene_Test()
        {
            Button b = new Button();

            b.transform.position = new Vector2(0, 0);
            b.text.transform.position = new Vector2(0, 0);
            b.sprite.transform.position = new Vector2(0, 0);

            b.text.color = Color.Red;
            b.text.useScreenCoord = false;

            b.onButtonPressed = new Event(Osef);

            b.text.transform.position = new Vector2(0, 200);

            Console.WriteLine("Sprite pos: " + b.sprite.transform.position);
            Console.WriteLine("Text pos: " + b.text.transform.position);
        }

        public void Osef()
        {
            Console.WriteLine("TEST");
        }
    }
}
