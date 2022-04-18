using Game.Logic.Managers;
using Game.Logic.MapObjects;
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

        public List<MapObject> MapObjects { get; private set; }
        public List<MapObject> BackgroundObjects { get; private set; }
        public Player Player { get; private set; }

        bool gameOver = false;

        int timer;
        int point;

        public GameLogic(Size windowSize)
        {
            InitMapObjects();
            InitPlayer();
            CollisionManager.Collision += OnCollision;
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
            List<MapObject> allMapObjects = new List<MapObject>(MapObjects);
            allMapObjects.AddRange(BackgroundObjects);

            if (!gameOver)
            {
                CheckInput();
                Player.Update(Player.Speed);
                foreach (var item in allMapObjects)
                {
                    item.Update(Player.Speed);
                }

                CollisionManager.Update(Player, MapObjects);
                MapObjectManager.Update(MapObjects, BackgroundObjects);
            }

            timer++;

            if (timer % 1500 == 0)
            {
                Player.SpeedUp(1f);
            }

            Debug.WriteLine(point);
        }

        void CheckInput()
        {
            if (Keyboard.IsKeyDown(Key.Left) || Keyboard.IsKeyDown(Key.A))
            {
                PlayerControl(Controls.Left);
            }
            if (Keyboard.IsKeyDown(Key.Right) || Keyboard.IsKeyDown(Key.D))
            {
                PlayerControl(Controls.Right);
            }
        }

        void OnCollision(CollisionEventArgs eargs)
        {
            Coin coin = eargs.CollisionWith as Coin;
            if (coin != null)
            {
                MapObjectManager.ObjectsToRemove.Add(coin);
                point += 3;
            }
            else
            {
                point += timer / 60;
                Debug.WriteLine(point);
                gameOver = true;
                GameOver?.Invoke();
            }
        }

        void InitPlayer()
        {
            Player = new Player(
                new Vector2(200, 470));
        }

        void InitMapObjects()
        {
            MapObjects = new List<MapObject>();
            BackgroundObjects = new List<MapObject>();
            BackgroundObjects.Add(new Background(new Vector2(0, 0)));
        }
    }
}
