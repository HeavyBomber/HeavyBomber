using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Graphics2d
{
    public class Camera2D : ICamera2D
    {
        private const float ZoomRate = 0.1f;
        private const float RotationRate = 0.1f;
        private const float MovementRate = 20f;
        private const float MinimalZoom = 0.1f;
        private const float MaximalZoom = 2;
  
        private  Vector2 screenCenter;
        private  Rectangle boundsRect;
        private  Rectangle avaiableRect;
        private  Vector2 positionValue;
        private  bool isMovingUsingScreenAxis;
        private  float rotationValue;
        private  float zoomValue;
        private  bool cameraChanged = false;

        private  Vector2 shakeOffset;

        public  Vector2 ScreenCenter
        {
            get { return screenCenter; }
        }

        public  Vector2 Position
        {
            get { return positionValue; }
        }

        public  float Rotation
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

        public  float Zoom
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

        public  bool IsChanged
        {
            get
            {
                return cameraChanged;
            }
        }

        public bool MoveUsingScreenAxis
        {
            set { isMovingUsingScreenAxis = value; }
            get { return isMovingUsingScreenAxis; }
        }

        public  Rectangle BoundsRect
        {
            get { return boundsRect; }
        }

        public Camera2D()
        {
            zoomValue = 1f;
            rotationValue = 0.0f;
            Center(computeProperCenterPoint(Vector2.Zero));
            shakeOffset = new Vector2();
            SwitchSize(new Rectangle(0, 0, 800, 480), new Rectangle(0, 0, 800, 480));
        }

        public void SwitchSize(Rectangle screenRect, Rectangle levelRect)
        {
            boundsRect = screenRect;
            avaiableRect = levelRect;
            screenCenter = new Vector2(screenRect.X + screenRect.Width / 2, screenRect.Y + screenRect.Height / 2);
        }

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

        public  void Shake()
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

            //if (InputManager.Instance.IsKeyPressed(downKey))
            //    value -= 1.0f;

            //if (InputManager.Instance.IsKeyPressed(upKey))
            //    value += 1.0f;

            return value;
        }

        #region Movement Methods

        /// <summary>
        /// Used to inform the camera that new values are updated by the application.
        /// </summary>
        public  void ResetChanged()
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
