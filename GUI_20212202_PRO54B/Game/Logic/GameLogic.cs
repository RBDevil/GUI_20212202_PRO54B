using Game.Logic.Managers;
using Game.Logic.MapObjects;
using Game.Logic.PowerUps;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Game.Logic
{
    public enum Controls
    {
        Right,
        Left
    }

    class GameLogic : IGameLogic
    {
        public delegate void GameEventHandler();
        public event GameEventHandler GameOver;

        public const int SCORE_PER_SECOND = 1;

        List<PickedUpPowerUp> powerUps = new List<PickedUpPowerUp>();
        public List<MapObject> MapObjects { get; private set; }
        public Player Player { get; private set; }

        int score = 0;
        int timer = 0;
        bool gameOver = false;

        public GameLogic(Size windowSize)
        {
            InitMapObjects();
            InitPlayer();
            CollisionChecker.Collision += OnCollision;
            MapObjectManager.Init(windowSize);
        }

        public void PlayerControl(Controls control)
        {
            if (!gameOver)
            {
                switch (control)
                {
                    case Controls.Right:
                        Player.MoveRight();
                        break;
                    case Controls.Left:
                        Player.MoveLeft();
                        break;
                    default:
                        break;
                }
            }
        }

        public void Update()
        {            
            if (!gameOver)
            {
                // update player
                Player.Update(Player.Speed);
                // update objects on the map
                foreach (var item in MapObjects)
                {
                    item.Update(Player.Speed);
                }

                CollisionChecker.Update(Player, MapObjects);
                MapObjectManager.Update(MapObjects);

                UpdatePickedUpPowerUps();

                // increase timer, and speed if needed
                timer++;
                if (timer % 60 == 0)
                    score += SCORE_PER_SECOND;

                Debug.WriteLine("Score: " + score);

                if (timer % 200 == 0)
                    Player.SpeedUp(0.1f);
            }
        }

        void OnCollision(CollisionEventArgs eargs)
        {
            if (eargs.CollisionWith is Coin)
            {
                // delete object after picking it up
                Coin coin = (Coin)eargs.CollisionWith;
                MapObjectManager.ObjectsToRemove.Add(coin);
                score += coin.Value;
            }
            else if (eargs.CollisionWith is PowerUp)
            {
                PowerUp powerUp = (PowerUp)eargs.CollisionWith;
                // delete object after picking it up
                MapObjectManager.ObjectsToRemove.Add(powerUp);
                // add new pickedUpPowerUp
                switch (powerUp.Type)
                {
                    case PowerUp.PowerUpType.CoinMagnet:
                        powerUps.Add(new CoinMagnet(150, 3));
                        break;
                    case PowerUp.PowerUpType.BonusHealth:
                        powerUps.Add(new BonusHealth());
                        break;
                    case PowerUp.PowerUpType.Minigun:
                        powerUps.Add(new Minigun());
                        break;
                    case PowerUp.PowerUpType.PointMultiplier:
                        PointMultiplier pointMultiplier = new PointMultiplier();
                        powerUps.Add(pointMultiplier);
                        CollisionChecker.Collision += pointMultiplier.OnCollision;
                        break;
                }
            }
            else if (Player.CollisionDisabledUntil > timer)
            {
            }
            else if (Player.Health > 1)
            {
                Player.Health--;
                Player.CollisionDisabledUntil = timer + 120;
            }
            else
            {
                gameOver = true;
                GameOver?.Invoke();
            }
        }

        void UpdatePickedUpPowerUps()
        {
            List<PickedUpPowerUp> toRemove = new List<PickedUpPowerUp>();
            
            foreach (var item in powerUps)
            {
                item.Update(MapObjects, Player, ref score);
                // if timer is 0, power up is no longer active so remove it
                if (item.LifeTime == 0)
                {
                    toRemove.Add(item);
                }
            }

            // actually remove items
            foreach (var item in toRemove)
            {
                powerUps.Remove(item);
            }
        }

        void InitPlayer()
        {
            Player = new Player(
                new Vector2(200, 300),
                40, 20);
        }

        void InitMapObjects()
        {
            MapObjects = new List<MapObject>();
        } 
    }
}
