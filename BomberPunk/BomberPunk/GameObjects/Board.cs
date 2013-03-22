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
using Microsoft.Xna.Framework.Graphics;
using PhantomEngine.Camera;
using PhantomEngine.GameObjects;
using PhantomEngine.Interfaces;
using PhantomEngine.Managers;
using PhantomEngine.Sound;
using PhantomEngine.Textures;
using SpriteSheetRuntime;
using XMLContent;

namespace BomberPunk.GameObjects
{
    class Board : GameObject, ILockable
    {
        private ResourcePool<Enemy> enemies = new ResourcePool<Enemy>();
        private ResourcePool<Bomb> bombs = new ResourcePool<Bomb>();
        private bool isLocked;
        private bool bombPlanted;
        private int bombCount;
        private Player player;
        private int rows;
        private int cols;
        private Camera2D camera;
        private int[,] terrainMap;
        private int[,] powerUpMap;
        private TerrainTile[,] terrainTiles;
        private PowerUp[,] powerUpTiles;
        private int[,] monsterMap;
        private int[,] collisionMap;
        private static Board instance = new Board();
        private TextureFactory effectsFactory;
        private TextureFactory terrainFactory;
        private TextureFactory powerUpsFactory;
        



        public const int TILE_SIZE = 48;
        //public const int ENEMY_OFFSET = 74;
        private const int BOMB_PERMISSION = 1;
        private const string SPRITES_PATH = "/Sprites";
        private const int BOMB_OFFSET = 9;
        const string MONSTER_PATH = "Sprites/Enemies/Enemy";

        public bool BombPlanted
        {
            get { return bombPlanted; }
            set { bombPlanted = value; }
        }
        public int[,] TerrainMap
        {
            get { return terrainMap; }
        }

        public int Cols
        {
            get { return cols; }
        }


        public int Rows
        {
            get { return rows; }
        }

        public static Board Instance
        {
            get { return instance; }
        }

        public int[,] CollisionMap
        {
            get { return collisionMap; }
            set { collisionMap = value; }
        }

        public TextureFactory EffectsFactory
        {
            get { return effectsFactory; }
        }

        public TextureFactory TerrainFactory
        {
            get { return terrainFactory; }
        }

        public TextureFactory PowerUpsFactory
        {
            get { return powerUpsFactory; }
        }

        public int[,] PowerUpMap
        {
            get { return powerUpMap; }
        }

        private Board()
        {
           
        }

        public bool IsLocked()
        {
            return isLocked;
        }

        public void Lock()
        {
            
        }

        public void Unlock()
        {
            
        }

        public void Load(int rows, int cols)
        {
            bombCount = 0;
            camera = new Camera2D(new Rectangle(150, 0, 650, 480), new Rectangle(0,0,cols * TILE_SIZE, rows * TILE_SIZE));

            this.basePosition = new Vector2(0,0);

            this.startupGameObject();

            Explosion.Instance.Initialize();
            const int TERRAIN_COUNT = 4;
            Texture2D[] textures = new Texture2D[TERRAIN_COUNT];
            textures[(int)TerrainBehaviors.Ground] = GameResources.Content.Load<Texture2D>("Sprites/Terrain/background");
            textures[(int)TerrainBehaviors.Destroyable] = GameResources.Content.Load<Texture2D>("Sprites/Terrain/wall");
            textures[(int)TerrainBehaviors.Undestroyable] = GameResources.Content.Load<Texture2D>("Sprites/Terrain/hardwall");
            textures[(int)TerrainBehaviors.Blowable] = GameResources.Content.Load<Texture2D>("Sprites/Terrain/barrel");



            terrainFactory = new TextureFactory(textures);


            textures = new Texture2D[4];
            textures[0] = GameResources.Content.Load<Texture2D>("Sprites/Effects/shadow_small");
            textures[1] = GameResources.Content.Load<Texture2D>("Sprites/Effects/shadow_medium");
            textures[2] = GameResources.Content.Load<Texture2D>("Sprites/Effects/shadow_large");
            textures[3] = GameResources.Content.Load<Texture2D>("Sprites/Effects/smoke");



            effectsFactory = new TextureFactory(textures);


            textures = new Texture2D[1];
            textures[0] = GameResources.Content.Load<Texture2D>("Sprites/PowerUps/BERSERKER");

            powerUpsFactory = new TextureFactory(textures,1);
            player = new Player();

            this.rows = rows;
            this.cols = cols;
            terrainMap = new int[cols, rows];
            powerUpMap = new int[cols, rows];
            terrainTiles = new TerrainTile[cols,rows];
            powerUpTiles = new PowerUp[cols,rows];
            monsterMap = new int[cols, rows];
            CollisionMap = new int[cols, rows];
        }

        public void SetTerrain(string terrainData, string powerUpData)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    terrainMap[x, y] = terrainData[x * 2 + 1 + (y * (cols * 2))] - '0';
                    terrainTiles[x,y] = new TerrainTile();
                    terrainTiles[x,y].Initialize(terrainMap[x,y], x, y);

                    PowerUpMap[x, y] = powerUpData[x * 2 + 1 + (y * (cols * 2))] - '0';
                    powerUpTiles[x, y] = new PowerUp();
                    if (terrainMap[x, y] == 0)
                    {
                        powerUpTiles[x, y].Initialize(PowerUpMap[x, y], x, y);
                    }
                }
            }
        }

        public void ReleasePowerUp(int x, int y)
        {
            powerUpMap[x, y] = PowerUps.NONE;            
        }

        public void ReleaseTerrain(int x, int y)
        {
            terrainMap[x, y] = TerrainIdentifiers.Empty;

            terrainTiles[x,y].Destroy();
            if (monsterMap[x, y] > 0)
            {
                enemies.UnusedObject.Restore(new Vector2(x, y),
                                             GameResources.Content.Load<SpriteSheet>(MONSTER_PATH + monsterMap[x, y] + SPRITES_PATH),
                                             EnemyProcessor.Instance.LoadEnemy(monsterMap[x, y], GameResources.Content));
            }
            if(powerUpMap[x,y] > 0)
            {
                powerUpTiles[x,y].Initialize(powerUpMap[x,y],x, y);
            }
        }

        public void SetEnemies(string data)
        {
            const string PLAYER_PATH = "Sprites/Hero";

            for (int y = 0; y < rows ; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    monsterMap[x, y] = data[x * 2 + 1 + (y * (cols * 2))] - '0';
                    if (monsterMap[x, y] != 0 && monsterMap[x, y] != 9 && terrainMap[x, y] == 0)
                    {
                        enemies.UnusedObject.Restore(new Vector2(x, y),
                                                     GameResources.Content.Load<SpriteSheet>(MONSTER_PATH + monsterMap[x, y] + SPRITES_PATH),
                                                     EnemyProcessor.Instance.LoadEnemy(monsterMap[x, y], GameResources.Content));
                    }
                    else if (monsterMap[x, y] == 9)
                    {
                        player.Restore(new Vector2(x, y) * TILE_SIZE, GameResources.Content.Load<SpriteSheet>(PLAYER_PATH + SPRITES_PATH),
                                                     null);
                    }
                }
            }
           
        }

        public void PlantBomb()
        {
            const string BOMB_PATH = "Sprites/Bomb";

            SoundManager.PlaySound("click");
            BombPlanted = true;
            var positionVector = new Vector2(((int) ((player.BasePosition.X + 0.5*TILE_SIZE)/TILE_SIZE))*TILE_SIZE,
                                             ((int) ((player.BasePosition.Y + 0.5*TILE_SIZE)/TILE_SIZE))*TILE_SIZE + BOMB_OFFSET);

            terrainMap[(int)positionVector.X / TILE_SIZE, (int)positionVector.Y / TILE_SIZE] = TerrainIdentifiers.Bomb;

            bombs.UnusedObject.Restore(positionVector, GameResources.Content.Load<SpriteSheet>(BOMB_PATH + SPRITES_PATH), null);
            bombCount++;
        }

        public void Blow(Vector2 position, bool fromBomb)
        {
            if (fromBomb)
                bombCount--;

            ComboManager.Instance.Explosion();
            Explosion.Instance.Explode(position);
            Camera2D.Shake();

            terrainMap[(int)position.X / TILE_SIZE, (int)position.Y / TILE_SIZE] = TerrainIdentifiers.Empty;
            GameSettings.SetGaugeValue(6, 1);
        }

        public void Update(GameTime gameTime)
        {

            camera.Update(gameTime);
            camera.Align(player.BasePosition);            

            for (int i = 0; i < AnimatedObject.AnimatedObjects.Count; i++)
            {
                AnimatedObject.AnimatedObjects[i].Update(gameTime);
            }

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if(PowerUpMap[x,y] != 0)
                        powerUpTiles[x,y].Update(gameTime);
                    terrainTiles[x, y].Update(gameTime);
                }
            }
            CollisionManager.Instance.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var animatedObject in AnimatedObject.AnimatedObjects)
            {
                animatedObject.Draw(gameTime, spriteBatch);
            }

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    terrainTiles[x, y].Draw(gameTime, spriteBatch);
                    if (PowerUpMap[x, y] != 0 && terrainMap[x,y] == 0)
                        powerUpTiles[x, y].Draw(gameTime, spriteBatch);
                }
            }

        }
    }
}
