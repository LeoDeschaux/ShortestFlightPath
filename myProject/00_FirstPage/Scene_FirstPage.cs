using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

namespace zzMathVisu
{
    public class Scene_FirstPage : MathScene
    {
        Sprite plane;

        public Scene_FirstPage()
        {
            Settings.BACKGROUND_COLOR = Color.GhostWhite;

            this.camControl.isActive = false;

            Viewport viewPort = new Viewport();
            viewPort.X = 0;
            viewPort.Y = 19;
            viewPort.Width = Settings.SCREEN_WIDTH;
            viewPort.Height = Settings.SCREEN_HEIGHT - 19;
            viewPort.MinDepth = 0;
            viewPort.MaxDepth = 1;
            Engine.renderingEngine.viewPort = viewPort;

            Text title = new Text();
            title.useScreenCoord = false;
            title.alignment = Alignment.Center;
            title.transform.position = new Vector2(0, 200);
            title.fontSize = 80;
            title.s = "Navigation";

            Text subtitle = new Text();
            subtitle.useScreenCoord = false;
            subtitle.alignment = Alignment.Center;
            subtitle.transform.position = new Vector2(0, 100);
            subtitle.fontSize = 40;
            subtitle.s = "Détermination d’une route aérienne";

            Text module = new Text();
            module.useScreenCoord = false;
            module.alignment = Alignment.Center;
            module.transform.position = new Vector2(0, 0);
            module.fontSize = 30;
            module.s = "Modélisation mathématique - M3202";

            Text name = new Text();
            name.useScreenCoord = false;
            name.alignment = Alignment.Center;
            name.transform.position = new Vector2(0, -100);
            name.s = "Léo Deschaux-Beaume";

            PlaneSpawner spawner = new PlaneSpawner();

            /*
            Particle particle = new Particle(Ressources.Load<Texture2D>("myContent/2D/plane"));
            //particle.TTL = 2;
            particle.AngularVelocity = 0.1f;
            particle.Size = 0.1f;
            particle.Speed = 0;

            ParticleProfile particleProfile = new ParticleProfile(particle);
            particleProfile.burstMode = false;
            particleProfile.loopMode = true;

            particleProfile.emissionRate = 1;
            particleProfile.maxParticles = 2;

            ParticleEngine particleEngine = new ParticleEngine(particleProfile, Vector2.Zero);
            particleEngine.drawOrder = 1000000;
            */
        }

        public override void Update()
        {

        }
    }
}
