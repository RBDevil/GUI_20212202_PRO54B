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
        public Rect ExtendedRect { get { return new Rect(Rect.X - 50, Rect.Y, Rect.Width, Rect.Height + 50); } }

        const int MIN_SPEED = 1;
        const int MAX_SPEED = 4;
        const int MIRROR_WIDTH = 3;
        float speed;

        public Car(Vector2 position, int widht, int height, int speed) 
            : base(position, height, widht)
        {
            BitmapImage image = new BitmapImage(new Uri(Path.Combine("Resources", "lada.png"), UriKind.RelativeOrAbsolute));
            Brush = new ImageBrush(image);
            Widht = (int)image.Width;
            Height = (int)image.Height;
            Health = 3;
            this.speed = speed;
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
            base.Update(playerSpeed - speed);
            // set rect after position changed to make sure it renders correctly
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }

        public void Accelerate(float amount)
        {
            speed += amount;
            if (speed > MAX_SPEED)
            {
                speed = MAX_SPEED;
            }
        }

        public void Decelerate(float amount)
        {
            speed -= amount;
            if (speed < MIN_SPEED)
            {
                speed = MIN_SPEED;
            }
        }
    }
}
