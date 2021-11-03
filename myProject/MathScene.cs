using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace zzMathVisu
{
    public class MathScene : IScene
    {
        TopMenu topMenu;
        Text sceneName;

        public MathScene()
        {
            topMenu = new TopMenu();

            Viewport viewPort = new Viewport();
            viewPort.X = 0;
            viewPort.Y = 19;
            viewPort.Width = Settings.SCREEN_WIDTH - 300;
            viewPort.Height = Settings.SCREEN_HEIGHT - 19;
            viewPort.MinDepth = 0;
            viewPort.MaxDepth = 1;

            Engine.renderingEngine.viewPort = viewPort;

            sceneName = new Text();
            sceneName.s = this.GetType().ToString().Substring(11);
            sceneName.drawOrder = 1000;
            sceneName.color = Color.White;

            Vector2 dim = new Vector2(sceneName.GetRectangle().Width, sceneName.GetRectangle().Height);
            Sprite background = new Sprite();
            background.color = Color.Black;
            background.dimension = dim;
            background.drawOrder = 999;
            background.useScreenCoord = true;
            background.transform.position += new Vector2(dim.X, -dim.Y)/2;
            Console.WriteLine(dim);
        }
        
        public override void DrawGUI()
        {
            topMenu.DrawGUI();
        }
    }
}
