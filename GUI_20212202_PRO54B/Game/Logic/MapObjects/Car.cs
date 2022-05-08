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
        public Rect ExtendedRect { get { return new Rect(Rect.X, Rect.Y - 50, Rect.Width, Rect.Height + 100); } }

        protected ImageBrush texture, brakeTexture;

        const float MIN_SPEED = 1;
        protected float MAX_SPEED = 4;
        const int MIRROR_WIDTH = 4;
        float speed;

        public Car(Vector2 position, int widht, int height, int speed) 
            : base(position, height, widht)
        {
            BitmapImage image = new BitmapImage(new Uri(Path.Combine("Resources", "lada.png"), UriKind.RelativeOrAbsolute));
            texture = new ImageBrush(image);
            BitmapImage brakeImage = new BitmapImage(new Uri(Path.Combine("Resources", "lada_brake.png"), UriKind.RelativeOrAbsolute));
            brakeTexture = new ImageBrush(brakeImage);
            Brush = texture;
            Widht = (int)image.Width;
            Height = (int)image.Height;
            Health = 3;
            this.speed = speed;
        }

        public override void Render(DrawingContext drawingContext)
        {
            if (GameLogic.DebugMode)
            {
                drawingContext.DrawRectangle(Brushes.Blue, null, ExtendedRect);
                drawingContext.DrawRectangle(Brushes.Red, null, Rect);
            }
            drawingContext.DrawRectangle(
                Brush, 
                null, 
                new Rect(Position.X, Position.Y, Widht, Height));
        }

        public override void Update(float playerSpeed)
        {
            Brush = texture;
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
            else
            {
                Brush = brakeTexture;
            }
        }

        public void SetMinSpeed()
        {
            speed = MIN_SPEED;
        }
    }
}
