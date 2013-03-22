//using System;
//using System.Collections.Generic;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using PublicIterfaces;
//using UserInterface.Controls;

//namespace UserInterface.UI
//{
//    public class WrapPanel : MenuForm
//    {
//        private int rows;
//        private int itemCount;
//        private int lastUnlockedItem;

//        public WrapPanel()
//        {
//            menuEntries = new List<MenuEntry>();

//        }

//        public void Init(int lastUnlockedItem, int itemCount, Texture2D[] textures, int lineSpacing, int columnSpacing)
//        {
//            this.itemCount = itemCount;
//            this.lastUnlockedItem = lastUnlockedItem;
//            this.lineSpacing = lineSpacing;
//            this.columnSpacing = columnSpacing;

//            var row = 0;
//            var col = 0;

//            for(int i = 0; i < itemCount; i++)
//            {
//                if (origin.X + MarginLeft + col * ColumnSpacing + buttonTexture.Width > 800)
//                {
//                    row++;
//                    col = 0;
//                }
//                else
//                {
//                    col++;
//                }
//                menuEntries.Add(new ImageButton(Convert.ToString(i + 1), new Vector2(55, -2), row, col,
//                                                                          this, textures));

//            }
//        }

//        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
//        {
//            base.Draw(gameTime, spriteBatch);
//        }

//        public override IPresentation GetPresentation()
//        {
//            throw new NotImplementedException();
//        }

//        public override bool IsInUse()
//        {
//            throw new NotImplementedException();
//        }

//        public override void Init()
//        {
//            throw new NotImplementedException();
//        }

//        public override void Update(GameTime gameTime)
//        {
//            throw new NotImplementedException();
//        }

//        public override List<GameObjectBase> GetChildren()
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
