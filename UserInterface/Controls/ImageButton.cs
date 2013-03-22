//using System;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Settings;
//using Sound;
//using UserInterface.UI;

//namespace UserInterface.Controls
//{
//    public class ImageButton : MenuButton
//    {
//        #region Fields

//        private Texture2D[] stateImages;
//        private int currentState;

//        #endregion

//        #region Properties

//        #endregion

//        #region Events

//        public event EventHandler<PlayerIndexEventArgs> Selected;

//        public override void OnSelectEntry()
//        {
//            SoundManager.PlaySound("menuclick");
//            propertyValue = !propertyValue;
//            GameSettings.SystemSettings[propertyId] = propertyValue;
//            if (Selected != null)
//                Selected(this, null);
//        }

//        #endregion

//        #region Initialization



//        public ImageButton(string text, Vector2 textOffset, int row, int column, MenuForm parentForm, Texture2D[] stateImages)
//            : base(text, textOffset, row, column, parentForm)
//        {
//            this.buttonTexture = parentForm.ButtonTexture;

//            this.parentForm = parentForm;
//            this.stateImages = stateImages;
//            this.Deselect();

//            position = new Vector2(-parentForm.MarginLeft - column * parentForm.ColumnSpacing, parentForm.MarginBottom - row * parentForm.LineSpacing);

//            bounds = new Rectangle((int)(parentForm.Origin - position).X, (int)(parentForm.Origin - position).Y, stateImages[0].Width, stateImages[0].Height);

//            this.textOffset = textOffset;
//        }

//        public void AttachProperty(int propertyId, string textOn, string textOff)
//        {
//            this.textOn = textOn;
//            this.textOff = textOff;
//            affectsSettings = true;
//            this.propertyId = propertyId;
//            this.propertyValue = Settings.GameSettings.SystemSettings[propertyId];
//        }

//        #endregion

//        public override void Select()
//        {
//            currentTextColor = parentForm.SelectedTextColor;
//        }

//        public override void Deselect()
//        {
//            currentTextColor = parentForm.NormalTextColor;
//        }

//        #region Update and Draw

//        public override void Update(bool isSelected, GameTime gameTime)
//        {
//            // When the menu selection changes, entries gradually fade between
//            // their selected and deselected appearance, rather than instantly
//            // popping to the new state.

//            float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

//            if (isSelected)
//                selectionFade = Math.Min(selectionFade + fadeSpeed, 1);
//            else
//                selectionFade = Math.Max(selectionFade - fadeSpeed, 0);
//        }

//        public override void Draw(GameTime gameTime,
//            SpriteBatch spriteBatch)
//        {


//            spriteBatch.Draw(stateImages[currentState], parentForm.Origin, null, Color.White, parentForm.Rotation, position, 1, SpriteEffects.None, LayerDepths.BUTTON);
            

//            if (affectsSettings)
//            {
//                if (GameSettings.SystemSettings[propertyId] == true)
//                {
//                    spriteBatch.DrawString(parentForm.Font, text + " " + textOn, parentForm.Origin, currentTextColor,
//                                           parentForm.Rotation,
//                                           position - textOffset, 1, SpriteEffects.None, LayerDepths.TEXT);
//                }
//                else
//                {
//                    spriteBatch.DrawString(parentForm.Font, text + " " + textOff, parentForm.Origin, currentTextColor, parentForm.Rotation,
//                                position - textOffset, 1, SpriteEffects.None, LayerDepths.TEXT);
//                }
//            }
//            else
//            {
//                spriteBatch.DrawString(parentForm.Font, text, parentForm.Origin, currentTextColor, parentForm.Rotation,
//                                   position - textOffset, 1, SpriteEffects.None, LayerDepths.TEXT);
//            }
//        }

//        #endregion
//    }
//}
