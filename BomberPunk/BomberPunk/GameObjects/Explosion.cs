using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameStructs;
using BomberPunk.Managers;
using Core.Resources;
using GameEntities.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using PhantomEngine.GameObjects;
using SpriteSheetRuntime;

namespace BomberPunk.GameObjects
{
    public class Explosion
    {
        private static Explosion instance = new Explosion();
        private SpriteSheet[] blowMiddle;
        private SpriteSheet blowCenter;
        private SpriteSheet[] blowOuter;
        private byte[] textureOriginal;
        private byte[] textureFlipped;

        private const int BLOW_OFFSET = 6;

        private const int PIXEL_SIZE = 4;
        private int size;
        private ResourcePool<Blow> blows = new ResourcePool<Blow>();

        //DO POPRAWY:
        private int frames = 9;


        public static Explosion Instance
        {
            get { return instance; }
        }

        public void Initialize()
        {
            const string MIDDLEBLOW_PATH = "Sprites/Blow/Middle/Sprites";
            const string CENTERBLOW_PATH = "Sprites/Blow/Center/Sprites";
            const string OUTERBLOW_PATH = "Sprites/Blow/Outer/Sprites";
            
           
            blowMiddle = new SpriteSheet[2];
            blowOuter = new SpriteSheet[4];

            blowCenter = GameResources.Content.Load<SpriteSheet>(CENTERBLOW_PATH);
            blowMiddle[0] = GameResources.Content.Load<SpriteSheet>(MIDDLEBLOW_PATH);
            blowMiddle[1] = blowMiddle[0].Copy();


            blowOuter[0] = GameResources.Content.Load<SpriteSheet>(OUTERBLOW_PATH);
            for (int i = 1; i < 4; i++)
            {
                blowOuter[i] = blowOuter[0].Copy();
            }

            size = blowCenter.SourceRectangle(0).Width;

            textureOriginal = new byte[size * size * PIXEL_SIZE];
            textureFlipped = new byte[size * size * PIXEL_SIZE];


            Rotate(2, blowMiddle[0], ref blowMiddle[1] );
            Rotate(1, blowOuter[0], ref blowOuter[1]);
            Rotate(1, blowOuter[1], ref blowOuter[2]);
            Rotate(1, blowOuter[2], ref blowOuter[3]);

        }

        public void Explode(Vector2 position)
        {
            const int INITIAL_RANGE = 1;

            var tileX = (int)position.X / Board.TILE_SIZE;
            var tileY = (int)position.Y / Board.TILE_SIZE;
            var blowPosition = new Vector2(tileX, tileY);
            var rectangles = new Rectangle[2];


            blows.UnusedObject.Restore(new Vector2(blowPosition.X * Board.TILE_SIZE, blowPosition.Y * Board.TILE_SIZE + BLOW_OFFSET), blowCenter, null);
            Vector2[] directionVectors = {
                                             new Vector2(1, 0), new Vector2(0, 1),
                                             new Vector2(-1, 0), new Vector2(0, -1)
                                         };

            for (int i = 3; i >= 0; i--)
            {
                blowPosition = new Vector2(tileX, tileY);
                blowPosition += directionVectors[i];
                int index;
                if (i == 0 || i == 2)
                {
                    index = 0;
                }
                else
                {
                    index = 1;
                }
                bool warning = false;
                var currentRange = 0;
                while (Board.Instance.TerrainMap[(int)blowPosition.X, (int)blowPosition.Y] == TerrainIdentifiers.Empty
                    && currentRange < INITIAL_RANGE + GameSettings.GaugeValues[(int)Gauges.Range])
                {
                    currentRange++;
                    blows.UnusedObject.Restore(new Vector2(blowPosition.X * Board.TILE_SIZE, blowPosition.Y * Board.TILE_SIZE + BLOW_OFFSET), blowMiddle[index], null);
                    blowPosition += directionVectors[i];
                }

                if (Board.Instance.TerrainMap[(int)blowPosition.X, (int)blowPosition.Y] == TerrainIdentifiers.WeakWall ||
                    Board.Instance.TerrainMap[(int)blowPosition.X, (int)blowPosition.Y] == TerrainIdentifiers.Barrel)
                {
                    Board.Instance.ReleaseTerrain((int)blowPosition.X, (int)blowPosition.Y);
                }
                if (Board.Instance.TerrainMap[(int)blowPosition.X, (int)blowPosition.Y] != TerrainIdentifiers.StrongWall)
                {
                    blows.UnusedObject.Restore(
                        new Vector2(blowPosition.X*Board.TILE_SIZE, blowPosition.Y*Board.TILE_SIZE + BLOW_OFFSET),
                        blowOuter[i], null);
                }

            }

        }

        private void Rotate(int flips, SpriteSheet input, ref SpriteSheet output)
        {
            
            for (int i = 0; i < frames; i++)
            {
                input.Texture.GetData(0, input.SourceRectangle(i), textureOriginal, 0, textureOriginal.Length);

                for (int j = 0; j < flips; j++)
                {
                    for (int x = 0; x < size; x++)
                    {
                        for (int y = 0; y < size; y++)
                        {
                            textureFlipped[(x*size + size - y - 1)*PIXEL_SIZE + 0] = textureOriginal[(x + y*size)*PIXEL_SIZE + 0];
                            textureFlipped[(x*size + size -  y - 1)*PIXEL_SIZE + 1] = textureOriginal[(x + y*size)*PIXEL_SIZE + 1];
                            textureFlipped[(x*size + size -  y - 1)*PIXEL_SIZE + 2] = textureOriginal[(x + y*size)*PIXEL_SIZE + 2];
                            textureFlipped[(x*size + size -  y - 1)*PIXEL_SIZE + 3] = textureOriginal[(x + y*size)*PIXEL_SIZE + 3];
                        }
                    }
                }
                output.Texture.SetData(0, output.SourceRectangle(i), textureFlipped, 0, textureFlipped.Length);
            }
        }
    }
}
