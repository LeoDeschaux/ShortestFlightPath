using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using myEngine;

namespace zzMathVisu.myProject._04_EarthSphereCoordinates
{
    public class Cursor3D : Object3D
    {
        public Cursor3D(Camera3D camera) : base(camera)
        {
            this.model = Ressources.Load<Model>("myContent/3D/sphere");

            this.transform3D.position = new Vector3(0, 0, 0);
            this.transform3D.scale = new Vector3(0.02f, 0.02f, 0.02f);

            this.effect.DiffuseColor = Color.Red.ToVector3();
            this.effect.Alpha = 1f;
            this.drawOrder = 0;
        }
    }
}
