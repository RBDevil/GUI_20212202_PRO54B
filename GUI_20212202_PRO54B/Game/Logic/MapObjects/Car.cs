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
    class Car : MapObject, ICollidable
    {
        public int Health { get; set; }
        public Rect Rect { get; private set; }

        const int MIRROR_WIDTH = 3;

        public Car(Vector2 position, int widht, int height) 
            : base(position, height, widht)
        {
            BitmapImage image = new BitmapImage(new Uri(Path.Combine("Resources", "lada.png"), UriKind.RelativeOrAbsolute));
            Brush = new ImageBrush(image);
            Widht = (int)image.Width;
            Height = (int)image.Height;
            Health = 3;
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(
                Brush, 
                null, 
                new Rect(Position.X, Position.Y, Widht, Height));
        }

        public override void Update(float playerSpeed)
        {
            base.Update(playerSpeed);
            // set rect after position changed to make sure it renders correctly
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }
    }
}
