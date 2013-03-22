using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XMLContent
{
        /// <summary>
        /// Klasa definiuje zmienne zawarte w plikach xml u�ywanych przez program.
        /// Aktualnie plik�w xml u�ywamy do zapisania ile kolumn i wierszy
        /// zawartych jest w adekfatnym do pliku spricie
        /// W plikach xml b�dziemy mogli te� zapisywa� inne dane, np. dialogi postaci
        /// wtedy te� rozszerzymy t� klas�.
        /// </summary>

        public class XMLFile
        {
            public int Rows;
            public int Cols;
        }
   
}
