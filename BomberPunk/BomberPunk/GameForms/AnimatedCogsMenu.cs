using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.Effects;
using BomberPunk.GameScreens;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using Core.Resources;
using GameEntities.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.GameObjects;
using PhantomEngine.GameScreens;
using PhantomEngine.MathFunctions;
using PhantomEngine.Sound;
using PhantomEngine.UI;

namespace BomberPunk.GameForms
{
    internal class AnimatedCogsMenu : MenuForm
    {
        private readonly Vector2 largeCogPosition = new Vector2(602, 602);
        private readonly Vector2 largeCogOrigin = new Vector2(0, 480);

        private readonly Vector2 smallCogPosition = new Vector2(296, 296);
        private readonly Vector2 smallCogOrigin = new Vector2(720, -2);

        private readonly Vector2 backgroundCogPosition = new Vector2(296, 296);
        private readonly Vector2 backgroundCogOrigin = new Vector2(400, 50);

        private const float ALIGN_ROTATION = 0.050f;
        private float initialRotation = 0;
        private float smallCogRotation = ALIGN_ROTATION;
        private float largeCogRotation = 0;
        private Texture2D largeCogTexture;
        private Texture2D smallCogTexture;
        private bool rotatesForward = false;
        private bool entriesChanged = false;
        private Texture2D longButtonTexture;       
        private GaussianFunction easingFunction;
        private float easingFunctionArg;
        private bool isInMenuRoot;
        private bool initialized;

        private delegate void EntrySelector();
        private EntrySelector SelectNewEntries;

        public AnimatedCogsMenu()
        {
            const string BUTTON_BACKGROUND_PATH = "Sprites/UI/Menu/buttonBackground";
            buttonTexture = GameResources.Content.Load<Texture2D>(BUTTON_BACKGROUND_PATH);

            isInMenuRoot = true;
            easingFunction = new GaussianFunction(6f, 3f);
            const string LARGE_COG_PATH = "Sprites/UI/Menu/largeCog";
            const string SMALL_COG_PATH = "Sprites/UI/Menu/smallCog";

            const string FONT_PATH = "Fonts/forque";
            normalTextColor = Color.Gold;
            selectedTextColor = Color.White;
            this.origin = largeCogOrigin;

            lineSpacing = 100;
            marginLeft = 200;
            marginBottom = 300;
            initialized = true;
            Font = GameResources.Content.Load<SpriteFont>(FONT_PATH);
            largeCogTexture = GameResources.Content.Load<Texture2D>(LARGE_COG_PATH);
            smallCogTexture = GameResources.Content.Load<Texture2D>(SMALL_COG_PATH);

            SelectNewEntries = SelectMainMenu;
            easingFunctionArg = 0f;
            rotatesForward = true;
        }

        protected override void OnBackButtonPressed()
        {
            SoundManager.PlaySound("menuclick");
            OnCancel(this, null);
        }

        private void SelectMainMenu()
        {
            const string BUTTON_BACKGROUND_PATH = "Sprites/UI/Menu/buttonBackground";
            buttonTexture = GameResources.Content.Load<Texture2D>(BUTTON_BACKGROUND_PATH);

            isInMenuRoot = true;

            MenuButton startMenuEntry = new MenuButton("play", new Vector2(55, -2), 0, 0, this);
            MenuButton optionsMenuEntry = new MenuButton("options", new Vector2(30, -2), 1, 0, this);
            //GraphicMenuEntry creditsMenuEntry = new GraphicMenuEntry("credits", new Vector2(30, -2), 2, this);
            MenuButton exitMenuEntry = new MenuButton("exit", new Vector2(55, -2), 2, 0, this);

            startMenuEntry.Selected += PlayMenuEntrySelected;
            optionsMenuEntry.Selected += OptionsMenuEntrySelected;
            //creditsMenuEntry.Selected += OnCancel;
            exitMenuEntry.Selected += OnCancel;

            // Add entries to the menu.
            menuEntries.Clear();
            menuEntries.Add(startMenuEntry);
            menuEntries.Add(optionsMenuEntry);
            menuEntries.Add(exitMenuEntry);
        }

        private void SelectOptionsMenu()
        {

            const string LONG_BUTTON_BACKGROUND_PATH = "Sprites/UI/Menu/longButtonBackground";
            buttonTexture = GameResources.Content.Load<Texture2D>(LONG_BUTTON_BACKGROUND_PATH);

            isInMenuRoot = false;

            MenuButton soundMenuEntry = new MenuButton("sound", new Vector2(55, -2), 0, 0, this);
            soundMenuEntry.AttachProperty((int)SystemSettings.Sound, "on", "off");

            MenuButton vibrationMenuEntry = new MenuButton("vibration", new Vector2(5, -2), 1, 0, this);
            vibrationMenuEntry.AttachProperty((int)SystemSettings.Vibration, "on", "off");

            MenuButton backMenuEntry = new MenuButton("back", new Vector2(90, -2), 2, 0, this);

            backMenuEntry.Selected += BackMenuEntrySelected;

            // Add entries to the menu.
            menuEntries.Clear();
            menuEntries.Add(soundMenuEntry);
            menuEntries.Add(vibrationMenuEntry);
            menuEntries.Add(backMenuEntry);
        }

        /// <summary>
        /// Event handler for when the Play Game menu entry is selected.
        /// </summary>
        void PlayMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            BackgroundTransition.Instance.GoToState("levelselection");
        }


        /// <summary>
        /// Event handler for when the Options menu entry is selected.
        /// </summary>
        void OptionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            SoundManager.PlaySound("cogs");
            SelectNewEntries = SelectOptionsMenu;
            easingFunctionArg = 0f;
            rotatesForward = true;
        }

        void BackMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            OnCancel(sender, e);
        }

        #region Update and Draw

        public override void Update(GameTime gameTime)
        {
            HandleInput();

            if(initialized == true)
            {
                initialized = false;
                SoundManager.PlaySound("cogs");                
            }
            if(rotatesForward)
            {
                //float ROTATION_INCREMENT = (1.01f - (float)Math.Pow(((MathHelper.ToRadians(90) - (largeCogRotation - initialRotation)) / MathHelper.ToRadians(90)), 2)) / 5;

                float rotationIncrement = easingFunction.GetFunctionValue(easingFunctionArg);
                easingFunctionArg += 0.3f;

                rotation += rotationIncrement;
                largeCogRotation += rotationIncrement;
                smallCogRotation -= rotationIncrement * 2.000f;

                if (largeCogRotation >= initialRotation + MathHelper.ToRadians(90) && entriesChanged == false)
                {
                    rotation = rotation - MathHelper.ToRadians(-180);
                    SelectNewEntries();
                    entriesChanged = true;
                }

                if (largeCogRotation >= initialRotation + MathHelper.ToRadians(180))
                {
                    if (initialRotation == 0)
                    {
                        initialRotation = MathHelper.ToRadians(180);
                    }
                    else
                    {
                        initialRotation = 0;
                        largeCogRotation -= MathHelper.ToRadians(360);
                    }

                    rotation = 0;
                    this.rotatesForward = false;
                    entriesChanged = false;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(smallCogTexture, backgroundCogOrigin, null, Color.DarkGray, -smallCogRotation, backgroundCogPosition, 1, SpriteEffects.None, LayerIdentifiers.MENU_COGS - 0.01f);

            spriteBatch.Draw(largeCogTexture, largeCogOrigin, null, Color.White, largeCogRotation, largeCogPosition, 1, SpriteEffects.None, LayerIdentifiers.MENU_COGS);

            spriteBatch.Draw(smallCogTexture, smallCogOrigin, null, Color.White, smallCogRotation, smallCogPosition, 1, SpriteEffects.None, LayerIdentifiers.MENU_COGS);
            
            foreach (MenuButton menuEntry in menuEntries)
            {
                menuEntry.Draw(gameTime, spriteBatch);
            }
        }

        #endregion

        /// <summary>
        /// When the user cancels the main menu, ask if they want to exit the sample.
        /// </summary>
        void OnCancel(object sender, PlayerIndexEventArgs e)
        {
            if (!isInMenuRoot)
            {
                SoundManager.PlaySound("cogs");
                SelectNewEntries = SelectMainMenu;
                easingFunctionArg = 0f;
                rotatesForward = true;
                SelectOptionsMenu();
            }
            else
            {
                BomberGame.ExitRequested = true;
            }
        }
    }
}
