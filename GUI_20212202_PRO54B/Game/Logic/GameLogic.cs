using Game.Logic.Managers;
using Game.Logic.MapObjects;
using Game.Logic.PowerUps;
using Game.Renderer;
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

        public List<MapObject> MapObjects { get; private set; }
        public List<MapObject> BackgroundObjects { get; private set; }
        public Player Player { get; private set; }

        List<PickedUpPowerUp> powerUps = new List<PickedUpPowerUp>();
        List<Bullet> bullets = new List<Bullet>();

        int score = 0;
        int timer = 0;
        bool gameOver = false;

        public GameLogic(Size windowSize)
        {
            InitMapObjects();
            InitPlayer();
            CollisionChecker.Collision += OnCollision;
            MapObjectManager.Init(windowSize);
            PoliceLights.Init(windowSize);
            InitSoundManager();
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
            List<MapObject> allMapObjects = new List<MapObject>(MapObjects);
            allMapObjects.AddRange(BackgroundObjects);

            if (!gameOver)
            {
                // update player
                Player.Update(Player.Speed);
                // update objects on the map
                foreach (var item in MapObjects)
                {
                    item.Update(Player.Speed);
                }
                foreach (var item in bullets)
                {
                    item.Update(Player.Speed);
                }
                foreach (var item in BackgroundObjects)
                {
                    item.Update(Player.Speed);
                }

                CollisionChecker.Update(Player, MapObjects, bullets);
                MapObjectManager.Update(MapObjects, BackgroundObjects, bullets);

                UpdatePickedUpPowerUps();

                // increase timer, and speed if needed
                timer++;
                if (timer % 60 == 0)
                    score += SCORE_PER_SECOND;

                Debug.WriteLine("Score: " + score + " Health: " + Player.Health);

                if (timer % 200 == 0)
                    Player.SpeedUp(0.1f);
            }
        }

        void OnPlayerCollision(ICollidable collisionWith)
        {
            if (collisionWith is Coin)
            {
                // delete object after picking it up
                Coin coin = (Coin)collisionWith;
                MapObjectManager.ObjectsToRemove.Add(coin);
                score += coin.Value;
            }
            else if (collisionWith is PowerUp)
            {
                PowerUp powerUp = (PowerUp)collisionWith;
                // delete object after picking it up
                MapObjectManager.ObjectsToRemove.Add(powerUp);
                // add new pickedUpPowerUp
                switch (powerUp.Type)
                {
                    case PowerUp.PowerUpType.CoinMagnet:
                        powerUps.Add(new CoinMagnet(100, 5));
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

        void OnBulletCollision(Bullet bullet, ICollidable collisionWith)
        {
            if (collisionWith is Car)
            {
                Car car = (Car)collisionWith;
                MapObjectManager.ObjectsToRemove.Add(bullet);
                car.Health -= bullet.Damage;
                if (car.Health < 1)
                {
                    MapObjectManager.ObjectsToRemove.Add(car);
                }
            }
        }

        void OnCollision(CollisionEventArgs eargs)
        {
            if (eargs.MapObject is Player)
            {
                OnPlayerCollision(eargs.CollisionWith);
            }
            else if (eargs.MapObject is Bullet)
            {
                OnBulletCollision((Bullet)eargs.MapObject, eargs.CollisionWith);
            }
        }

        void UpdatePickedUpPowerUps()
        {
            List<PickedUpPowerUp> toRemove = new List<PickedUpPowerUp>();
            
            foreach (var item in powerUps)
            {
                item.Update(MapObjects, bullets, Player, ref score, timer, toRemove);
                // if timer is 0, power up is no longer active so remove it

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
                new Vector2(200, 450));
        }

        void InitMapObjects()
        {
            MapObjects = new List<MapObject>();
            BackgroundObjects = new List<MapObject>();
            BackgroundObjects.Add(new Background(new Vector2(0, 0)));
        }

        void InitSoundManager()
        {
            SoundManager.PlayMusic();
            GameOver += SoundManager.StopMusic;
        }

        public void Render(DrawingContext drawingContext)
        {
            // render all the objects on the map
            foreach (var item in BackgroundObjects)
            {
                item.Render(drawingContext);
            }
            foreach (var item in MapObjects)
            {
                item.Render(drawingContext);
            }
            foreach (var item in bullets)
            {
                item.Render(drawingContext);
            }
            // render player
            Player.Render(drawingContext);

            PoliceLights.Render(drawingContext);
        }
    }
}
