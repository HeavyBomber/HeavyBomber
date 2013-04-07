﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using PublicIterfaces.Sound;

namespace Input
{
    class InputManager : IInputManager
    {
        private static InputManager instance = new InputManager();
        private List<Vector2> touchPositions = new List<Vector2>();
        private List<Vector2> releasedTouchPositions = new List<Vector2>();
        private GestureSample gesteture;
        private bool isGestureAvaiable;
        private KeyboardState currentKeyboardState;
        private KeyboardState lastKeyboardState;
        private GamePadState currentButtonState;
        private GamePadState lastButtonState;
        private MouseState currentMouseState;

        private List<ITapListener> clickListeners; 
        private List<ISwipeListener> swipeListeners; 

        public float MouseX
        {
            get { return currentMouseState.X * 3; }
        }

        public float MouseY
        {
            get { return currentMouseState.Y * 3; }
        }

        public List<Vector2> TouchPositions
        {
            get
            {
                return touchPositions;
            }
        }

        public List<Vector2> ReleasedTouchPositions
        {
            get
            {
                return releasedTouchPositions;
            }
        }

        public InputManager()
        {
            TouchPanel.EnabledGestures = GestureType.HorizontalDrag
                                        | GestureType.VerticalDrag
                                        | GestureType.Tap;
            
            clickListeners = new List<ITapListener>();
            swipeListeners = new List<ISwipeListener>();
        }

        public void RegisterClickListener(ITapListener clickListener)
        {
           if(!clickListeners.Contains(clickListener))
           {
               clickListeners.Add(clickListener);
           }
        }

        public void RegisterMovementListener(ISwipeListener swipeListener)
        {
            if (!swipeListeners.Contains(swipeListener))
            {
                swipeListeners.Add(swipeListener);
            }
        }

        public void Update()
        {
            lastButtonState = currentButtonState;
            currentButtonState = new GamePadState();
            lastKeyboardState = currentKeyboardState;
            currentKeyboardState = new KeyboardState();

            checkForGestures();

            //if (TouchPanel.IsGestureAvailable)
            //{
            //    gesteture = TouchPanel.ReadGesture();
            //    isGestureAvaiable = true;
            //}
            //else
            //{
            //    isGestureAvaiable = false;
            //}

            //touchPositions.Clear();
            //releasedTouchPositions.Clear();

            //TouchCollection touchCollection = TouchPanel.GetState();
            //foreach (TouchLocation touchLoc in touchCollection)
            //{
            //    if ((touchLoc.State == TouchLocationState.Pressed) ||
            //        (touchLoc.State == TouchLocationState.Moved))
            //    {
            //        touchPositions.Add(new Vector2((int)touchLoc.Position.X, (int)touchLoc.Position.Y));
            //    }
            //    else if (touchLoc.State == TouchLocationState.Released)
            //    {
            //        releasedTouchPositions.Add(new Vector2((int)touchLoc.Position.X, (int)touchLoc.Position.Y));
            //    }
            //}
        }

        private void checkForGestures()
        {
            if (TouchPanel.IsGestureAvailable)
            {
                gesteture = TouchPanel.ReadGesture();
                if(gesteture.GestureType == GestureType.Tap)
                {
                    notifyListenersAboutTap(gesteture.Position);
                }
                else if(gesteture.GestureType == GestureType.HorizontalDrag ||
                        gesteture.GestureType == GestureType.VerticalDrag)
                {
                    notifyListenersAboutSwipe(gesteture.Delta);
                }
            }
        }

        private void notifyListenersAboutTap(Vector2 position)
        {
            foreach (var clickListener in clickListeners)
            {
                if (clickListener.ScreenClicked(position))
                {
                    break;
                }
            }
        }

        private void notifyListenersAboutSwipe(Vector2 delta)
        {
            foreach (var swipeListener in swipeListeners)
            {
                swipeListener.SwipeExecuted(delta);
            }
        }

        public bool IsButtonPressed(Buttons b)
        {
            //return (GamePad.GetState(PlayerIndex.One).IsButtonDown(k));
            return currentButtonState.IsButtonDown(b);
        }
        public bool WasButtonPressed(Buttons b)
        {
            var a = currentButtonState.IsButtonUp(b);
            var c = lastButtonState.IsButtonDown(b);
            return (currentButtonState.IsButtonUp(b)
                && lastButtonState.IsButtonDown(b));

        }
        public bool IsKeyPressed(Keys k)
        {
            return currentKeyboardState.IsKeyDown(k);
        }

        public Vector2 GetGesture()
        {
            if (isGestureAvaiable && gesteture.GestureType != GestureType.Tap)
            {
                return gesteture.Delta;
            }

            return Vector2.Zero;
        }

        public Vector2 TapExecuted()
        {

            if (isGestureAvaiable && gesteture.GestureType == GestureType.Tap)
            {
                return gesteture.Position;
            }

            return Vector2.Zero;
        }

        private bool WasKeyPressed(Keys k)
        {
            return lastKeyboardState.IsKeyDown(k);
        }

        public bool IsKeyHit(Keys k)
        {
            if (IsKeyPressed(k) && !WasKeyPressed(k))
                return true;
            return false;
        }

    }
}
