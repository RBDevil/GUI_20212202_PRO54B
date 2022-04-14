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

        public List<MapObject> MapObjects { get; private set; }
        public Player Player { get; private set; }

        int score = 0;
        int timer = 0;
        bool gameOver = false;

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
            if (!gameOver)
            {
                Player.Update(Player.Speed);
                foreach (var item in MapObjects)
                {
                    item.Update(Player.Speed);
                }

                CollisionManager.Update(Player, MapObjects);
                MapObjectManager.Update(MapObjects);

                timer++;

                if(timer % 500 == 0)
                {
                    Player.SpeedUp(0.5f);
                }
            }
        }

        void OnCollision(CollisionEventArgs eargs)
        {
            if (eargs.CollisionWith is Coin || eargs.CollisionWith is PowerUp)
            {
                MapObjectManager.ObjectsToRemove.Add((MapObject)eargs.CollisionWith);
            }
            else
            {
                gameOver = true;
                GameOver?.Invoke();
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
