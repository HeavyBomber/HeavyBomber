#region Using Statements
using System;
using System.Collections.Generic;
using Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

#endregion


namespace Renderer.Camera
{
    public class Camera2D
    {
        #region Constants
        private const float ZoomRate = 0.1f;
        private const float RotationRate = 0.1f;
        private const float MovementRate = 20f;
        private const float MinimalZoom = 0.1f;
        private const float MaximalZoom = 2;
        #endregion

        #region Fields
        private static Vector2 screenCenter;
        private static Rectangle boundsRect;
        private static Rectangle avaiableRect;
        private static Vector2 positionValue;
        private static bool isMovingUsingScreenAxis;
        private static float rotationValue;
        private static float zoomValue;
        private static bool cameraChanged = false;

        private static Vector2 shakeOffset;
        #endregion

        #region Public Properties
        /// <summary>
        /// Get the position of the screen center
        /// </summary>
        public static Vector2 ScreenCenter
        {
            get { return screenCenter; }
        }

        /// <summary>
        /// Get/Set the postion value of the camera
        /// </summary>
        public static Vector2 Position
        {
            get { return positionValue; }
        }

        /// <summary>
        /// Get/Set the rotation value of the camera
        /// </summary>
        public static float Rotation
        {
            set
            {
                if (rotationValue != value)
                {
                    cameraChanged = true;
                    rotationValue = value;
                }
            }
            get { return rotationValue; }
        }

        /// <summary>
        /// Get/Set the zoom value of the camera
        /// </summary>
        public static float Zoom
        {
            set
            {
                if (zoomValue != value)
                {
                    cameraChanged = true;
                    zoomValue = value;
                }
            }
            get { return zoomValue; }
        }

        /// <summary>
        /// Gets whether or not the camera has been changed since the last
        /// ResetChanged call
        /// </summary>
        public static bool IsChanged
        {
            get
            {
                return cameraChanged;
            }
        }

        /// <summary>
        /// Set to TRUE to pan relative to the screen axis when
        /// the camera is rotated.
        /// </summary>
        public bool MoveUsingScreenAxis
        {
            set { isMovingUsingScreenAxis = value; }
            get { return isMovingUsingScreenAxis; }
        }

        public static Rectangle BoundsRect
        {
            get { return boundsRect; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Create a new Camera2D
        /// </summary>
        public Camera2D(Rectangle screenRect, Rectangle levelRect)
        {
            avaiableRect = levelRect;
            screenCenter = new Vector2(screenRect.X + screenRect.Width / 2, screenRect.Y + screenRect.Height / 2);
            boundsRect = screenRect;
            zoomValue = 1f;
            rotationValue = 0.0f;
            Center(computeProperCenterPoint(Vector2.Zero));
            shakeOffset = new Vector2();
        }
        #endregion

        #region Update Methods
        public void Update(GameTime gameTime)
        {
            HandleInput();

            if (shakeOffset.X> 0)
            {
                cameraChanged = true;
                positionValue.X += (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds) * (float)Math.Sqrt(shakeOffset.X) * 5;
                positionValue.Y += (float)Math.Cos(gameTime.TotalGameTime.TotalMilliseconds) * (float)Math.Sqrt(shakeOffset.Y) * 5;

                rotationValue += (float)Math.Sin(gameTime.TotalGameTime.TotalMilliseconds) *
                                 (float)Math.Sqrt(shakeOffset.X) / 40;

                shakeOffset.X -= 0.1f;
                shakeOffset.Y -= 0.1f;
            }

        }

        public static void Shake()
        {
            shakeOffset += new Vector2(1f, 1f);
        }

        private void HandleInput()
        {
            //check for camera movement
            float dX = ReadKeyboardAxis(Keys.Left, Keys.Right) * MovementRate;
            float dY = ReadKeyboardAxis(Keys.Up, Keys.Down) * MovementRate;
            MoveHorizontally(dX);
            MoveVertically(dY);

            //check for camera rotation
            dX = ReadKeyboardAxis(Keys.A, Keys.D) * RotationRate;
            Rotation += dX;

            //check for camera zoom
            dX = ReadKeyboardAxis(Keys.S, Keys.W) * ZoomRate;

            //limit the zoom
            Zoom += dX;
            if (Zoom < MinimalZoom) Zoom = MinimalZoom;
            if (Zoom > MaximalZoom) Zoom = MaximalZoom;

        }

        private float ReadKeyboardAxis(Keys downKey, Keys upKey)
        {
            float value = 0;

            if (InputManager.Instance.IsKeyPressed(downKey))
                value -= 1.0f;

            if (InputManager.Instance.IsKeyPressed(upKey))
                value += 1.0f;

            return value;
        }

        #endregion
        #region Movement Methods

        /// <summary>
        /// Used to inform the camera that new values are updated by the application.
        /// </summary>
        public static void ResetChanged()
        {
            cameraChanged = false;
        }

        /// <summary>
        /// Pan horizontally.  Corrects for rotation if specified.
        /// </summary>
        private void MoveHorizontally(float dist)
        {
            if (dist != 0)
            {
                cameraChanged = true;
                if (isMovingUsingScreenAxis)
                {
                    positionValue.X += (float)Math.Cos(-rotationValue) * dist;
                    positionValue.Y += (float)Math.Sin(-rotationValue) * dist;
                }
                else
                {
                    positionValue.X += dist;
                }
            }
        }
        /// <summary>
        /// Pan vertically.  Corrects for rotation if specified.
        /// </summary>
        private void MoveVertically(float dist)
        {
            if (dist != 0)
            {
                cameraChanged = true;
                if (isMovingUsingScreenAxis)
                {
                    //using negative distance becuase 
                    //"up" actually decreases the y value
                    positionValue.X += (float)Math.Sin(rotationValue) * dist;
                    positionValue.Y += (float)Math.Cos(rotationValue) * dist;
                }
                else
                {
                    positionValue.Y += dist;
                }
            }
        }
        /// <summary>
        /// Centrowanie kamery na podanym w argumencie punkcie
        /// Przesuwa kamerę w stronę podanego punktu
        /// </summary>
        public void Center(Vector2 centerPoint)
        {
            MoveHorizontally(centerPoint.X - positionValue.X);
            MoveVertically((centerPoint.Y - positionValue.Y));

        }

        private Vector2 computeProperCenterPoint(Vector2 centerPoint)
        {
            if (centerPoint.X < -BoundsRect.Left + screenCenter.X)
            {
                centerPoint.X = -BoundsRect.Left + screenCenter.X;
            }
            else if (centerPoint.X > avaiableRect.Right - BoundsRect.Right + screenCenter.X)
            {
                centerPoint.X = avaiableRect.Right - BoundsRect.Right + screenCenter.X;
            }

            if (centerPoint.Y < -BoundsRect.Top + screenCenter.Y)
            {
                centerPoint.Y = -BoundsRect.Top + screenCenter.Y;
            }
            else if (centerPoint.Y > avaiableRect.Bottom - BoundsRect.Height + screenCenter.Y)
            {
                centerPoint.Y = avaiableRect.Bottom - BoundsRect.Height + screenCenter.Y;
            }
            return centerPoint;

            return Vector2.Zero;
        }

        public void Align(Vector2 centerPoint)
        {
            centerPoint = computeProperCenterPoint(centerPoint);

            float distanceX = centerPoint.X - positionValue.X;
            float distanceY = centerPoint.Y - positionValue.Y;

            if (distanceX > 1)
            {
                distanceX = (float) Math.Sqrt(distanceX);
            }
            else if(distanceX < -1)
            {
                distanceX = -(float)Math.Sqrt(-distanceX);

            }
            if (distanceY > 1)
            {
                distanceY = (float) Math.Sqrt(distanceY);
            }
            else if(distanceY < -1)
            {
                distanceY = -(float)Math.Sqrt(-distanceY);
            }

            const float ROTATION_ALIGN = 0.01f;
            if (rotationValue < -ROTATION_ALIGN)
                rotationValue += ROTATION_ALIGN;
            else if (rotationValue > ROTATION_ALIGN)
                rotationValue -= ROTATION_ALIGN;
            else
                rotationValue = 0;

            MoveHorizontally(distanceX);
            MoveVertically(distanceY);
        }

        #endregion
    }
}
