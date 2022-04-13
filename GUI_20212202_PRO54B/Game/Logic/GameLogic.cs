using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
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
            MapObjects = new List<MapObject>();
            AddPlayer();
        }

        void AddPlayer()
        {
            Player = new Player(
                new Vector2(0,0),
                40, 20,
                new SolidColorBrush(Color.FromRgb(100,100,100)));
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
    }
}
