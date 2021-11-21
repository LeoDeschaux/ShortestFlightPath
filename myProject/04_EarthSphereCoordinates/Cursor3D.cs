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
        public float latitude;
        public float longitude;

        public Cursor3D(Camera3D camera) : base(camera)
        {
            this.model = Ressources.Load<Model>("myContent/3D/sphere");

            this.transform3D.position = new Vector3(0, 0, 0);
            this.transform3D.scale = new Vector3(0.02f, 0.02f, 0.02f);

            this.effect.DiffuseColor = Color.Red.ToVector3();
            this.effect.Alpha = 1f;
            this.drawOrder = 0;
        }

        public Cursor3D(Camera3D camera, float latitude, float longitude) : base(camera)
        {
            this.model = Ressources.Load<Model>("myContent/3D/sphere");

            this.transform3D.position = new Vector3(0, 0, 0);
            this.transform3D.scale = new Vector3(0.02f, 0.02f, 0.02f);

            this.effect.DiffuseColor = Color.Red.ToVector3();
            this.effect.Alpha = 1f;
            this.drawOrder = 0;

            transform3D.position = RotateAround(latitude, longitude) * 0.8f * 1.3f;
            transform3D.scale = Vector3.One * 0.01f * 2f;
            effect.DiffuseColor = Color.Red.ToVector3();
        }

        public void SetPos(float latitude, float longitude)
        {
            transform3D.position = RotateAround(latitude, longitude) * 0.8f * 1.3f;
        }

        private static Vector3 RotateAround(float latitude, float longitude)
        {
            latitude = latitude % 360;
            longitude = longitude % 360;

            float posX = (float)Math.Sin(MathHelper.ToRadians(-latitude)) *
                         (float)Math.Sin(MathHelper.ToRadians(longitude));

            float posY = (float)Math.Cos(MathHelper.ToRadians(-latitude));

            float posZ = (float)Math.Sin(MathHelper.ToRadians(-latitude)) *
                         (float)Math.Cos(MathHelper.ToRadians(longitude));

            return new Vector3(posX, posY, posZ);
        }
    }
}
