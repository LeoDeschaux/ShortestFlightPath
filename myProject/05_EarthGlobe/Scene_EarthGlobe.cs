using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

namespace zzMathVisu
{
    public class Scene_EarthGlobe : MathScene
    {
        //FIELDS
        Camera3D camera;
        private Model model;

        Object3D globe;
        Shapes3D s;

        //CONSTRUCTOR
        public Scene_EarthGlobe()
        {
            Settings.BACKGROUND_COLOR = Color.Gray;
            camera = new Camera3D(Engine.game, new Vector3(0, 1.5f,0), Vector3.Zero, 15);

            Floor f = new Floor(Engine.game.GraphicsDevice, camera, 4, 4, 1);

            globe = new Object3D(camera);
            globe.model = Ressources.Load<Model>("myContent/3D/earth");

            globe.transform3D.position = new Vector3(0,2,5);
            globe.transform3D.rotation.X = -90;
            globe.transform3D.scale = new Vector3(5, 5, 5);

            s = new Shapes3D(Engine.game.GraphicsDevice, camera);
        }

        //METHODS
        public override void Update()
        {
            globe.transform3D.rotation.Y += 45 * Time.deltaTime;
        }

        public override void Draw(SpriteBatch sprite, Matrix matrix)
        {
            //s.DrawTriangle(Vector3.Zero, 5, Color.Red);
            s.DrawLine(Vector3.Zero, new Vector3(0, 2, 0), 100, Color.Red);
        }
    }
}
