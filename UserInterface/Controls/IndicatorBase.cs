using System;
using GameEntities.GameObjects;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface.Controls
{
    public abstract class IndicatorBase : GameObject
    {
        protected DigitBase[] digits;
        protected Texture2D backgroundTexture;
        protected bool isBinary;
        protected int key;
        protected string name;

        public string Name
        {
            get { return name; }
        }
        //public abstract void SetValue(string value);
        //public abstract void SetValue(float value);

        public virtual void SetValue(float value)
        {
            var stringValue = Convert.ToString(value);

            if (isBinary)
            {
                if (value > digits.Length)
                {
                    return;
                }
                for (int i = 0; i < value; i++)
                {
                    if ((int)Char.GetNumericValue(stringValue[i]) >= 1)
                    {
                        digits[i].SetValue(1);
                    }
                    else
                    {
                        digits[i].SetValue(0);
                    }
                }
            }
            else
            {

                if (stringValue.Length > digits.Length)
                {
                    return;
                }


                for (int i = 0; i < stringValue.Length; i++)
                {
                    digits[digits.Length - stringValue.Length + i].SetValue((int)Char.GetNumericValue(stringValue[i]));
                }
            }
        }

        public virtual void SetValue(string value)
        {

            if (value.Length > digits.Length)
            {
                return;
            }

            for (int i = 0; i < value.Length; i++)
            {
                if (value[i] == '-')
                {
                    digits[digits.Length - value.Length + i].SetValue(10);
                }
                digits[digits.Length - value.Length + i].SetValue((int)Char.GetNumericValue(value[i]));
            }

        }

    }
}
