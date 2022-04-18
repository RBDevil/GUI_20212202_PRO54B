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
using static System.Net.Mime.MediaTypeNames;

namespace Game.Logic.MapObjects
{
    class Coin : MapObject, ICollidable
    {
        public Rect Rect { get => new Rect(Position.X, Position.Y, Widht, Height); }

        const int RADIUS = 10;

        public Coin(Vector2 position) : base(position, RADIUS * 2, RADIUS * 2)
        {
            Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Resources", "coin.png"), UriKind.RelativeOrAbsolute)));
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brush, null, new Point((int)(Position.X + RADIUS), (int)(Position.Y + RADIUS)), RADIUS, RADIUS);
        }
    }
}
