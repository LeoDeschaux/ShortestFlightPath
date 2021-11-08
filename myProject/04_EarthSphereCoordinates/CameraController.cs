using Microsoft.Xna.Framework;
using myEngine;
using System;
using System.Collections.Generic;
using System.Text;

namespace zzMathVisu.myProject._04_EarthSphereCoordinates
{
    public class CameraController : EmptyObject
    {
        Camera3D camera;
        Vector3 initPos;

        public float camRotX = 0;
        public float camRotY = 0;
        public float camDist = 1;

        //METHODS
        Vector2 deltaMouse;
        float speedMouse = 0.5f;

        public CameraController(Camera3D camera)
        {
            this.camera = camera;
            initPos = camera.Position;
        }

        public override void Update()
        {
            UpdateInput();

            camDist = MathHelper.Clamp(camDist, 0.3f, 3);

            //CAM
            Vector3 targetPos = Vector3.Zero;
            camera.cameraLookAt = targetPos;
            camera.Position = targetPos + Vector3.Transform(initPos - targetPos,
                Matrix.CreateFromAxisAngle(new Vector3(1, 0, 0), MathHelper.ToRadians(camRotX)) *
                Matrix.CreateFromAxisAngle(new Vector3(0, 1, 0), MathHelper.ToRadians(camRotY))) * (camDist);
        }

        private void UpdateInput()
        {
            //MOUSE CAM CONTROLE
            if (myEngine.Mouse.position.X <= 980)
            {
                if (Input.GetMouseDown(MouseButtons.Left))
                {
                    deltaMouse = myEngine.Mouse.position.ToVector2();
                }

                if (Input.GetMouse(MouseButtons.Left))
                {
                    //camera.transform.position += ((deltaMouse - myEngine.Mouse.position.ToVector2()) * speedMouse);
                    Vector2 newRot = ((deltaMouse - myEngine.Mouse.position.ToVector2()) * speedMouse);

                    if (camRotX - newRot.Y < 80 && camRotX - newRot.Y > -80)
                        camRotX -= newRot.Y;

                    camRotY += newRot.X;

                    deltaMouse = myEngine.Mouse.position.ToVector2();
                }
            }

            camRotY = camRotY % 360;

            //WHEEL ZOOM
            //WHEEL ZOOM
            if (Engine.renderingEngine.viewPort.Bounds.Contains(myEngine.Mouse.position))
            {
                float scrollSpeed = 50;

                if (Input.GetScrollWheelZoomIn())
                {
                    camDist += -scrollSpeed * Time.deltaTime;
                }
                if (Input.GetScrollWheelZoomOut())
                {
                    camDist += scrollSpeed * Time.deltaTime;
                }
            }
        }
    }
}
