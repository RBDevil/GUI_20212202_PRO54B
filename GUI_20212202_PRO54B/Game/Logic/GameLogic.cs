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

namespace Game.Logic
{
    public enum Controls
    {
        Right,
        Left
    }

    class GameLogic : IGameLogic
    {
        public List<MapObject> MapObjects { get; private set; }
        public Player Player { get; private set; }

        public GameLogic()
        {
            InitMapObjects();
            InitPlayer();
            CollisionManager.Collision += OnCollision; ;
        }

        public void PlayerControl(Controls control)
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

        public void Update()
        {
            Player.Update();
            foreach (var item in MapObjects)
            {
                item.Update();
            }

            CollisionManager.Update(Player, MapObjects);
        }

        void OnCollision(CollisionEventArgs eargs)
        {
            Debug.WriteLine("Collision: " + eargs.CollisionWith.ToString());
        }

        void InitPlayer()
        {
            Player = new Player(
                new Vector2(200, 300),
                40, 20,
                new SolidColorBrush(Color.FromRgb(100, 100, 100)));
        }

        void InitMapObjects()
        {
            MapObjects = new List<MapObject>();
            MapObjects.Add(new Car(new Vector2(200, 0), 40, 40, new SolidColorBrush(Color.FromRgb(0, 200, 0))));
        }
    }
}
