using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System.Windows;
using myEngine;

namespace zzMathVisu
{
    public class OrbitCamera
    {
        public Matrix World = Matrix.Identity;
        public Matrix View { get { return Matrix.Invert(World); } }
        public Matrix Projection = Matrix.Identity;
        private float distanceToTarget = 10.0f;
        public float AddDistanceToTarget { get { return distanceToTarget; } set { if (distanceToTarget + value > 1.0f) distanceToTarget += value; } }

        public OrbitCamera()
        {
            World = Matrix.CreateWorld(new Vector3(0, 2, 10), new Vector3(0, 0, .1f) - new Vector3(0, 2, 10), Vector3.Up);
            Projection = Matrix.CreatePerspectiveFieldOfView(1.0f, 4f / 3f, 1f, 100000f);
        }
        public Vector3 Position
        {
            set { World.Translation = value; }
            get { return World.Translation; }
        }
        public Vector3 Forward
        {
            set { World = Matrix.CreateWorld(World.Translation, Vector3.Normalize(value), Vector3.Up); }
            get { return World.Forward; }
        }
        public void SetTargetAsPosition(Vector3 targetPosition)
        {
            World = Matrix.CreateWorld(World.Translation, targetPosition - World.Translation, Vector3.Up);
        }

        public void OrbitTargetLeftRight(Vector3 targetPosition, float distance, float angle)
        {
            var rotAmnt = Matrix.CreateRotationY(angle);
            World *= rotAmnt;
            World.Translation = Vector3.Normalize(World.Translation - targetPosition) * distance;
            World = Matrix.CreateWorld(World.Translation, targetPosition - World.Translation, World.Up);
        }
        public void OrbitTargetUpDown(Vector3 targetPosition, float distance, float angle)
        {
            var rotAmnt = Matrix.CreateRotationX(angle);
            World *= rotAmnt;
            World.Translation = Vector3.Normalize(World.Translation - targetPosition) * distance;
            World = Matrix.CreateWorld(World.Translation, targetPosition - World.Translation, World.Up);
        }
        public void MoveTowardsAway(Vector3 targetPosition, float distance)
        {
            World.Translation = Vector3.Normalize(World.Translation - targetPosition) * distance;
            World = Matrix.CreateWorld(World.Translation, targetPosition - World.Translation, World.Up);
        }

        //METHODS
        public void DrawModel(Object3D object3D)
        {
            Matrix x = Matrix.CreateScale(object3D.transform3D.scale) *
                            Matrix.CreateRotationX(MathHelper.ToRadians(object3D.transform3D.rotation.X)) *
                            Matrix.CreateRotationY(MathHelper.ToRadians(object3D.transform3D.rotation.Y)) *
                            Matrix.CreateRotationZ(MathHelper.ToRadians(object3D.transform3D.rotation.Z)) *
                            Matrix.CreateTranslation(new Vector3(-object3D.transform3D.position.X,
                                                                 object3D.transform3D.position.Y,
                                                                 object3D.transform3D.position.Z));

            Engine.game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            Engine.game.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Engine.game.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            foreach (ModelMesh mesh in object3D.model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    //mesh.Effects = object3D.effect;

                    effect.World = x;
                    effect.View = View;
                    effect.Projection = Projection;

                    effect.DiffuseColor = object3D.effect.DiffuseColor;
                    effect.Alpha = object3D.effect.Alpha;

                    //TEXTURE
                    effect.TextureEnabled = object3D.effect.TextureEnabled;

                    effect.Texture = object3D.effect.Texture;

                    //GLOBAL
                    effect.FogEnabled = Object3D.GlobalEffect.FogEnabled;
                    effect.FogColor = Object3D.GlobalEffect.FogColor;
                    effect.FogStart = Object3D.GlobalEffect.FogStart;
                    effect.FogEnd = Object3D.GlobalEffect.FogEnd;

                    //LIGHTING
                    effect.LightingEnabled = Object3D.GlobalEffect.LightingEnabled;
                    effect.DirectionalLight0.DiffuseColor = Object3D.GlobalEffect.DirectionalLight0.DiffuseColor;
                    effect.DirectionalLight0.Direction = Object3D.GlobalEffect.DirectionalLight0.Direction;
                    effect.DirectionalLight0.SpecularColor = Object3D.GlobalEffect.DirectionalLight0.SpecularColor;

                    effect.AmbientLightColor = Object3D.GlobalEffect.AmbientLightColor;
                }

                mesh.Draw();
            }

        }
    }
}
