using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Logic.MapObjects
{
    class PowerUp : MapObject, ICollidable
    {
        public enum PowerUpType
        {
            CoinMagnet,
            BonusHealth,
            Minigun,
            PointMultiplier
        }

        public Rect Rect { get => new Rect(Position.X, Position.Y, Widht, Height); }

        public PowerUpType Type { get; }

        const int RADIUS = 20;

        public PowerUp(Vector2 position, PowerUpType type) : base(position, RADIUS * 2, RADIUS * 2)
        {
            Type = type;
            // set texture (brush) here, depending on type
            switch (type)
            {
                case PowerUpType.CoinMagnet:
                    Brush = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                    break;
                case PowerUpType.BonusHealth:
                    Brush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    break;
                case PowerUpType.Minigun:
                    Brush = new SolidColorBrush(Color.FromRgb(0, 0, 255));
                    break;
                case PowerUpType.PointMultiplier:
                    Brush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    break;
            }
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brush, null, new Point((int)(Position.X + RADIUS), (int)(Position.Y + RADIUS)), RADIUS, RADIUS);
        }        
    }
}
