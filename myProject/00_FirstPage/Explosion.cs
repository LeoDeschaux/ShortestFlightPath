using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;


namespace zzMathVisu.myProject._00_FirstPage
{
    public class Explosion : GameObject
    {
        AnimatedSprite animation;

        public Explosion(Vector2 pos)
        {
            animation = new AnimatedSprite(Ressources.Load<Texture2D>("myContent/2D/explosion"), 2, 4);
            animation.speed = 10;
            animation.destroyOnEnd = true;
            animation.dimension = new Vector2(150, 150);
            animation.transform.position = Vector2.Zero;
            animation.transform.position = new Vector2(pos.X,  720/2 - animation.dimension.Y/2);
        }
    }
}
