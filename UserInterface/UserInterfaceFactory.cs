using System;
using System.Collections.Generic;
using System.Linq;
using GameObjects.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using PublicIterfaces.BasicGameObjects;
using PublicIterfaces.Input;
using PublicIterfaces.UserInterface;
using UserInterface.Buttons;

namespace UserInterface
{
    class UserInterfaceFactory : GameObjectsFacoryBase, IUserInterfaceFactory
    {
        private IList<IButton> buttons = new List<IButton>();
        private IInputManager inputManager;


        public UserInterfaceFactory(IInputManager inputManager)
        {
            this.inputManager = inputManager;
        }

        public Drawable2DComposite CreateButton(Drawable2DComposite background, Drawable2DComposite font, float letTextMargin, EventHandler clickHandler)
        {
            var button = fetchObject<Button>();

            button.Click += clickHandler;
            if(!buttons.Contains(button))
            {
                buttons.Add(button);
            }

            inputManager.RegisterClickListener(button);
            button.Init(background, font, letTextMargin);
            return button;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
