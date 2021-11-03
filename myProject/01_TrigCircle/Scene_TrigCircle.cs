using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

namespace zzMathVisu
{
    public class Scene_TrigCircle : MathScene
    {
        //FIELDS
        Text t;

        //CONSTRUCTOR
        public Scene_TrigCircle()
        {
            Settings.BACKGROUND_COLOR = Color.Black;

            this.camControl.isActive = true;

            t = new Text();
            t.color = Color.White;
        }

        float angle = 45f;
        float radius = 300f;

        Vector2 pos;

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            Shapes s = new Shapes(Engine.game);

            s.Begin(this.camera);

            s.DrawCircle(Vector2.Zero, radius, 64, 2, Color.White);


            /* CACULATE POS */
            pos = new Vector2((float)Math.Cos(-angle),(float)Math.Sin(-angle));
            pos *= radius;
            
            //pos.Normalize();
            //pos *= radius;

            /* ------------------------------ */

            s.DrawLine(Vector2.Zero, pos, 3, Color.Red);

            Vector2 startPos = new Vector2(0, pos.Y);
            Vector2 endPos = new Vector2(pos.X, pos.Y);
            s.DrawLine(startPos, endPos, 3, Color.LightGreen);

            startPos = new Vector2(pos.X, 0);
            endPos = new Vector2(pos.X, pos.Y);
            s.DrawLine(startPos, endPos, 3, Color.LightGreen);

            s.End();
        }

        public override void Update()
        {
            //t.s = "" + Util.ScreenToWorld(camera, Mouse.position.ToVector2());

            string s1 = string.Format("{0:0.##}", (float)Math.Cos(-angle));
            string s2 = string.Format("{0:0.##}", (float)Math.Sin(angle));

            string s3 = string.Format("{0:0.##}", pos.X);
            string s4 = string.Format("{0:0.##}", pos.Y);

            t.s = 
                "Cos: " + s1 +
                "\nSin: " + s2 +
                "\npos: " + pos.X + ", " + pos.Y;

            angle += 1f * Time.deltaTime;
        }
    }
}
