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
    public class LevelData
    {
        public int Rows;
        public int Cols;
        public string[] TerrainPaths;
        public string[] EnemyPaths;
        public string Terrain;
        public string Enemies;
        public string PowerUps;
    }
}
