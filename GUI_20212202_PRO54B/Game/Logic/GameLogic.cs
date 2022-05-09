using Game.Logic.Managers;
using Game.Logic.MapObjects;
using Game.Logic.PowerUps;
using Game.Renderer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    class GameLogic : IGameLogic
    {
        public static bool DebugMode { get; private set; } 

        Size windowSize = new Size(500, 600);

        public delegate void GameEventHandler(int score);
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

        int level;
        int coinMagnetLevel;
        int minigunLevel;
        int pointMultiplierLevel;
        int carLevel;

        public GameLogic(int level, int coinMagnetLevel, int minigunLevel, int pointMultiplierLevel, int carLevel, bool debugMode)
        {
            DebugMode = debugMode;
            this.level = level;
            this.coinMagnetLevel = coinMagnetLevel;
            this.minigunLevel = minigunLevel;
            this.pointMultiplierLevel = pointMultiplierLevel;
            this.carLevel = carLevel;
            InitMapObjects();
            InitPlayer(this.level, this.carLevel);
            CollisionChecker.Collision += OnCollision;
            MapObjectManager.Init(windowSize);
            InitSoundManager();
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
                        powerUps.Add(CoinMagnet.Copy(LevelData.CoinMagnetLevel[coinMagnetLevel]));
                        break;
                    case PowerUp.PowerUpType.BonusHealth:
                        powerUps.Add(new BonusHealth());
                        break;
                    case PowerUp.PowerUpType.Minigun:
                        Minigun gun = null;
                        foreach (var p in powerUps)
                        {
                            if (p is Minigun)
                            {
                                gun = p as Minigun;
                                break;
                            }
                        }
                        if (gun != null)
                        {
                            gun.AddAmmo(LevelData.MinigunLevel[minigunLevel].AmmoCount);
                        }
                        else
                        {
                            powerUps.Add(Minigun.Copy(LevelData.MinigunLevel[minigunLevel]));
                        }
                        break;
                    case PowerUp.PowerUpType.PointMultiplier:
                        PointMultiplier pointMultiplier = PointMultiplier.Copy(LevelData.PointMultiplierLevel[pointMultiplierLevel]);
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
                GameOver?.Invoke(score);
                score = 0;
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

        void OnAiCollision(CollisionEventArgs eargs)
        {
            Car car1 = (Car)eargs.MapObject;
            Car car2 = (Car)eargs.CollisionWith;

            if (car1.Position.Y < car2.Position.Y)
            {
                car1.Accelerate(0.02f);
                car2.Decelerate(0.02f);
            }
            else
            {
                car2.Accelerate(0.02f);
                car1.Decelerate(0.02f);
            }

            if (car1.Rect.IntersectsWith(car2.Rect))
            {
                if (car1.Position.Y > car2.Position.Y)
                {
                    car1.SetMinSpeed();
                }
                else
                {
                    car2.SetMinSpeed();
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
            else if (eargs.MapObject is Car && eargs.CollisionWith is Car)
            {
                OnAiCollision(eargs);
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

        void InitPlayer(int level, int carLevel)
        {
            Player = new Player(
                new Vector2(200, 450),
                LevelData.StartingSpeeds[level],
                LevelData.CarLevel[carLevel][1],
                LevelData.CarLevel[carLevel][0],
                LevelData.CarLevel[carLevel][2]
                );
        }

        void InitMapObjects()
        {
            MapObjects = new List<MapObject>();
            BackgroundObjects = new List<MapObject>();
            BackgroundObjects.Add(new Background(new Vector2(0, 0)));
        }

        void InitSoundManager()
        {
            SoundManager.Init();
            CollisionChecker.Collision += SoundManager.OnCollision;
            GameOver += SoundManager.StopMusic;
            SoundManager.PlayMusic();
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

            RenderHud(drawingContext);
        }

        void RenderHud(DrawingContext drawingContext)
        {
            drawingContext.DrawText(new FormattedText("Health: " + Player.Health,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                25,
                Brushes.Black),
                new Point(360,0));

            drawingContext.DrawText(new FormattedText("Score: " + score,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                new Typeface("Verdana"),
                25,
                Brushes.Black),
                new Point(10, 0));

            Minigun gun = null;
            foreach (var p in powerUps)
            {
                if (p is Minigun)
                {
                    gun = p as Minigun;
                    break;
                }
            }
            if (gun != null)
            {
                drawingContext.DrawText(new FormattedText("Ammo: " + gun.AmmoCount,
     CultureInfo.CurrentUICulture,
     FlowDirection.LeftToRight,
     new Typeface("Verdana"),
     25,
     Brushes.Black),
     new Point(10, 520));
            }
        }
    }
}
