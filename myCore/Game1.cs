using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using myEngine;

namespace zzMathVisu
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
        }

        protected override void Initialize()
        {
            Engine.Initialize(this, graphics);
            SceneManager.ChangeScene(typeof(Scene_SphereCoordinates));

            base.Initialize();
        }

        protected override void Update(GameTime gameTime)
        {
            Engine.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Engine.Draw();
            base.Draw(gameTime);
        }
    }
}
