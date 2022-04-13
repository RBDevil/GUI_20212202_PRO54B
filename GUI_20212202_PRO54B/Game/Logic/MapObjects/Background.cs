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
    class Background : MapObject
    {
        const int HEIGHT = 600;
        const int WIDTH = 600;

        public Background(Vector2 position) : base(position, HEIGHT, WIDTH)
        {
            Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Resources", "road.png"), UriKind.RelativeOrAbsolute)));
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brush, null, new Rect(Position.X, Position.Y, WIDTH, HEIGHT));
        }
    }
}
