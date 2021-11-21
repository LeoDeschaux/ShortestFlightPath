using System;
using System.Collections.Generic;
using System.Text;
using myEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using zzMathVisu.myProject._00_FirstPage;

namespace zzMathVisu
{
    public class Plane : GameObject
    {
        Sprite sprite;
        float speed;

        public Plane(float startPosX, float speed)
        {
            sprite = new Sprite(Ressources.Load<Texture2D>("myContent/2D/plane"));
            sprite.transform.scale = new Vector2(1, 1);
            sprite.dimension = new Vector2(100, 100);
            sprite.transform.rotation = 180;
            sprite.drawOrder = -50000;

            sprite.transform.position = new Vector2(startPosX, 720 / 2);
            this.speed = speed;
        }

        public override void Update()
        {
            sprite.transform.position.Y -= speed * Time.deltaTime;

            if (sprite.transform.position.Y - sprite.dimension.Y/2 <= -720 / 2)
            {
                Explosion e = new Explosion(sprite.transform.position);
                Destroy();
            }
        }

        public override void OnDestroy()
        {
            sprite.Destroy();
        }
    }
}
