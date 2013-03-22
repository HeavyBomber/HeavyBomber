using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BomberPunk.GameObjects;
using BomberPunk.GameStructs;
using Core.GameComponents;
using Core.Resources;
using GameEntities.Settings;
using Microsoft.Xna.Framework;
using PhantomEngine.Sound;
using XMLContent;

namespace BomberPunk.Managers
{
    class ComboManager : DrawableGameComponent, IGameComponent
    {
        protected static ComboManager instance = new ComboManager(GameResources.Game);
        private DynamicText dynamicText;
        private Combo[] combos;
        private double timeSpan;

        private string killString;
        private string explosionString;
        private string pointsString;
        private int kills;
        private int explosions;
        private bool isCountig;

        public static ComboManager Instance
        {
            get
            {
                return instance;
            }
        }

        private ComboManager(Game game)
            : base(game)
        {
            dynamicText = new DynamicText();
        }

        public void Initialize()
        {
            
        }

        public void EnemyKilled()
        {
            if (!isCountig)
            {
                kills = 1;
                isCountig = true;
            }
            else
            {
                kills++;
                timeSpan = 0;
            }
        }

        public void Explosion()
        {
            if (!isCountig)
            {
                explosions = 1;
                timeSpan = 0;
                isCountig = true;
            }
            else
            {
                explosions++;
                timeSpan = 0;
            }
        }
        protected override void LoadContent()
        {
            dynamicText.Load();

            combos = GameResources.Content.Load<ComboList>("Combos/Data").Combos;
        }

        public void DisplayText(string text)
        {
            dynamicText.trigFloat(text);
        }

        public override void Update(GameTime gameTime)
        {
            if (isCountig)
            {
                timeSpan += gameTime.ElapsedGameTime.Milliseconds;

                if (timeSpan > Bomb.BOMB_TIMEOUT * 1000)
                {
                    checkCombo();
                    timeSpan = 0;
                    kills = 0;
                    explosions = 0;
                    isCountig = false;
                }
            }
            dynamicText.Update(gameTime);
            base.Update(gameTime);
        }

        private void checkCombo()
        {
            if (kills < 2 && explosions < 2)
            {
                return;
            }
            var points = PointsIdentifiers.ADD_KILL_POINTS * kills + PointsIdentifiers.ADD_EXPLOSION_POINTS * explosions;

            killString = kills == 1 ? " kill " : " kills ";

            explosionString = explosions == 1 ? " explosion " : " explosions ";

            pointsString = " pts";

            for (int i = combos.Length - 1; i >= 0; i--)
            {
                if (combos[i].Kills == 0)
                {
                    if (combos[i].Explosions <= explosions)
                    {
                        GameSettings.SetGaugeValue((int)Gauges.Points, GameSettings.GaugeValues[(int)Gauges.Points] + points);
                        SoundManager.PlaySound("bell");
                        points += combos[i].Points;
                        dynamicText.trigFloat(String.Format("{0}{1}{2}{3}+{4}{5}", kills, killString, explosions, explosionString, points, pointsString),
                                              combos[i].Name);
                        return;
                    }
                }
                else if (combos[i].Explosions == 0)
                {
                    if (combos[i].Kills <= kills)
                    {
                        GameSettings.SetGaugeValue((int)Gauges.Points, GameSettings.GaugeValues[(int)Gauges.Points] + points);
                        SoundManager.PlaySound("bell");

                        dynamicText.trigFloat(String.Format("{0}{1}{2}{3}+{4}{5}", kills, killString, explosions, explosionString, points, pointsString),
                                              combos[i].Name);
                        return;
                    }
                }
                else if (combos[i].Kills <= kills && combos[i].Explosions >= explosions)
                {
                    GameSettings.SetGaugeValue((int)Gauges.Points, GameSettings.GaugeValues[(int)Gauges.Points] + points);
                    SoundManager.PlaySound("bell");

                    dynamicText.trigFloat(String.Format("{0}{1}{2}{3}+{4}{5}", kills, killString, explosions, explosionString, points, pointsString),
                                              combos[i].Name);
                    return;
                }
            }

            dynamicText.trigFloat(String.Format("{0}{1}{2}{3}", kills, killString, explosions, explosionString));
        }
        public override void Draw(GameTime gameTime)
        {
            dynamicText.Draw(gameTime, GameResources.SpriteBatch);
            base.Draw(gameTime);
        }
    }
}
