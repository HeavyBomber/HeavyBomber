using GameObjects.Factories;
using MathFunctions;
using Microsoft.Xna.Framework;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.GameObjectsFactories;
using UserInterface.Buttons;
using System;

namespace HeavyBomberPrefabricates.MainMenu
{
    public class LargeCog : Drawable2DContainer
    {
        private const int BUTTON_SPACING = 100;

        private IGameObjectsFactory gameObjectsFactory;
        private IUserInterfaceFactory userInterfaceFactory;

        private Drawable2DComposite cog;
        private Drawable2DContainer mainMenu;
        private Drawable2DContainer settingsMenu;
        private Drawable2DContainer exitConfirmationMenu;
        private Drawable2DContainer currentMenu;

        public event EventHandler MenuChanged;
        public event EventHandler StartClicked;


        public LargeCog(IGameObjectsFactory gameObjectsFactory, IUserInterfaceFactory userInterfaceFactory)
        {
            this.gameObjectsFactory = gameObjectsFactory;
            this.userInterfaceFactory = userInterfaceFactory;
        }

        public override void Init()
        {
            base.Init();
            const string LARGE_COG_PATH = "Sprites/UI/Menu/largeCog";
            this.cog = gameObjectsFactory.CreateSprite(LARGE_COG_PATH);            
            cog.SetRelativePosition(new Vector2(-602, -122));
            this.AddChild(cog);
            
            initMenus();
        }

        private void initMenus()
        {
            float halfRotation = MathHelper.ToRadians(180);
            //main menu
            Drawable2DComposite button = createButton("settings", onSettingsMenuSelected);
            button.Init();            
            button.SetRelativePosition(new Vector2(20, 100));
            mainMenu = new Drawable2DContainer();
            mainMenu.Init();            
            mainMenu.AddChild(button);
            mainMenu.SetRotation(halfRotation);
            this.AddChild(mainMenu);

            //settings menu
            button = createButton("back", onBackButtonPressed);
            button.Init();            
            button.SetRelativePosition(new Vector2(0, 0));
            settingsMenu = new Drawable2DContainer();
            settingsMenu.Init();            
            settingsMenu.AddChild(button);
            this.AddChild(settingsMenu);
            settingsMenu.Hide();

            //exit confirmation menu
            button = createButton("yes", onBackButtonPressed);
            button.Init();            
            button.SetRelativePosition(new Vector2(0, 0));
            exitConfirmationMenu = new Drawable2DContainer();
            exitConfirmationMenu.Init();                        
            exitConfirmationMenu.AddChild(button);
            this.AddChild(exitConfirmationMenu);
            exitConfirmationMenu.Hide();

            //this.SetRotation(halfRotation);
            base.Init();
            onMainMenuSelected(this, EventArgs.Empty);
        }

        private Drawable2DComposite createButton(string caption, EventHandler handler)
        {
            const string FONT_PATH = "Fonts/forque";
            Drawable2DComposite font = gameObjectsFactory.CreateFont(FONT_PATH, caption);
            var buttonTexture = gameObjectsFactory.CreateSprite("Sprites/UI/Menu/buttonBackground");
            Drawable2DComposite button = userInterfaceFactory.CreateButton(buttonTexture, font, 20, handler);
            return button;
        }

        private void onBackButtonPressed(object sender, EventArgs e)
        {
            if(currentMenu == mainMenu)
            {
                exitConfirmationMenu.Show();
                settingsMenu.Hide();
            }

            if (MenuChanged != null)
            {
                MenuChanged(this, EventArgs.Empty);
            }
        }

        private void onSettingsMenuSelected(object sender, EventArgs args)
        {
            currentMenu = settingsMenu;
            settingsMenu.Show();
            exitConfirmationMenu.Hide();

            if (StartClicked != null)
            {
                StartClicked(this, EventArgs.Empty);
            }
        }

        private void onMainMenuSelected(object sender, EventArgs args)
        {
            currentMenu = mainMenu;
            if (MenuChanged != null)
            {
                MenuChanged(this, EventArgs.Empty);
            }
        }
    }
}
