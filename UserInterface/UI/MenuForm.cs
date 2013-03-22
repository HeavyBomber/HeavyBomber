using System;
using System.Collections.Generic;
using GameEntities.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PublicIterfaces;
using PublicIterfaces.GameObjects;

namespace UserInterface.UI
{
    /// <summary>
    /// Forma, która zawiera w sobie składowe wpisy menu
    /// </summary>
    public class MenuForm : GameObjectBase, IMenuForm
    {
        protected int marginBottom;
        protected int marginLeft;
        protected int lineSpacing;
        protected int columnSpacing;
        protected const int EntryLength = 200;
        protected const int EntryHeight = 50;
        protected int selectedEntry = 0;
        protected Color selectedTextColor;
        protected Color normalTextColor;
        protected Color disabledTextColor;
        protected Texture2D buttonTexture;


        // private event EventHandler<PlayerIndexEventArgs> BackButtonReleased;

        protected virtual void OnBackButtonPressed()
        {
            //if (BackButtonReleased != null)
            //    BackButtonReleased(this, null);
        }

        public Color SelectedTextColor
        {
            get { return selectedTextColor; }
        }

        public int MarginLeft
        {
            get { return marginLeft; }
        }

        public int MarginBottom
        {
            get { return marginBottom; }
        }

        public int LineSpacing
        {
            get { return lineSpacing; }
        }

        public Color NormalTextColor
        {
            get { return normalTextColor; }
        }

        public Color DisabledTextColor
        {
            get { return disabledTextColor; }
        }

        public Texture2D ButtonTexture
        {
            get { return buttonTexture; }
            set { buttonTexture = value; }
        }

        public int ColumnSpacing
        {
            get { return columnSpacing; }
        }

        public MenuForm()
        {
            children = new List<IGameObject>();
            selectedTextColor = Color.White;
        }

        public void AddMenuEntry(string entry, Delegate callback)
        {
            throw new NotImplementedException();
        }

        //public void HandleInput()
        //{
        //    //// Move to the previous menu entry?
        //    //if (InputManager.Instance.IsKeyHit(Keys.Up))
        //    //{
        //    //    selectedEntry--;

        //    //    if (selectedEntry < 0)
        //    //        selectedEntry = menuEntries.Count - 1;
        //    //}

        //    //// Move to the next menu entry?
        //    //if (InputManager.Instance.IsKeyHit(Keys.Down))
        //    //{
        //    //    selectedEntry++;

        //    //    if (selectedEntry >= menuEntries.Count)
        //    //        selectedEntry = 0;
        //    //}

        //    //else if (InputManager.Instance.IsKeyHit(Keys.Enter))
        //    //{
        //    //    OnSelectEntry(selectedEntry);
        //    //}

        //    if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        //    {
        //        OnBackButtonPressed();
        //    }

        //    foreach (var position in InputManager.Instance.TouchPositions)
        //    {
        //        foreach (var menuEntry in menuEntries)
        //        {
        //            menuEntry.Deselect();

        //            if (position.X > menuEntry.Bounds.Left && position.X < menuEntry.Bounds.Right)
        //            {
        //                if (position.Y > menuEntry.Bounds.Top && position.Y < menuEntry.Bounds.Bottom)
        //                {
        //                    menuEntry.Select();
        //                    break;
        //                }
        //            }
        //        }
        //    }

        //    foreach (var position in InputManager.Instance.ReleasedTouchPositions)
        //    {
        //        foreach (var menuEntry in menuEntries)
        //        {
        //            if (position.X > menuEntry.Bounds.Left && position.X < menuEntry.Bounds.Right)
        //            {
        //                if (position.Y > menuEntry.Bounds.Top && position.Y < menuEntry.Bounds.Bottom)
        //                {
        //                    menuEntry.Deselect();
        //                    //SoundManager.PlaySound("metalclick");
        //                    //OnSelectEntry(menuEntry.Row);
        //                    menuEntry.OnSelectEntry();
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        //protected virtual void OnSelectEntry(int entryIndex)
        //{
        //    menuEntries[selectedEntry].OnSelectEntry();
        //}

        //public override void Update(GameTime gameTime)
        //{
        //    HandleInput();
        //    foreach (MenuButton menuEntry in menuEntries)
        //    {
        //        // Update each nested MenuEntry object.
        //        for (int i = 0; i < menuEntries.Count; i++)
        //        {
        //            bool isSelected = (i == selectedEntry);//&&isActive;

        //            menuEntries[i].Update(isSelected, gameTime);
        //        }
        //    }
        //}

        //public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        //{
        //    foreach (MenuButton menuEntry in menuEntries)
        //    {
        //        menuEntry.Draw(gameTime, spriteBatch);
        //    }
        //}
    }
}
