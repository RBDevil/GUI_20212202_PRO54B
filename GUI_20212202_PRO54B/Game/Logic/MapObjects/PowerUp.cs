using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            // Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Resources", "mycoin.png"), UriKind.RelativeOrAbsolute)));
            switch (type)
            {
                case PowerUpType.CoinMagnet:
                    Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Resources", "powerup_magnet.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case PowerUpType.BonusHealth:
                    Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Resources", "powerup_heart.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case PowerUpType.Minigun:
                    Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Resources", "powerup_minigun.png"), UriKind.RelativeOrAbsolute)));
                    break;
                case PowerUpType.PointMultiplier:
                    Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Resources", "powerup_2x.png"), UriKind.RelativeOrAbsolute)));
                    break;
            }
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brush, null, new Point((int)(Position.X + RADIUS), (int)(Position.Y + RADIUS)), RADIUS, RADIUS);
        }        
    }
}
