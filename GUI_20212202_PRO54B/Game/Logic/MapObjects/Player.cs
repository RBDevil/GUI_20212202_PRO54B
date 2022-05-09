using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Logic.MapObjects
{
    public class Player : MapObject, ICollidable
    {
        public int Health { get; set; }
        public int CollisionDisabledUntil { get; set; }
        public float Speed { get; private set; }
        public Rect Rect { get; private set; }

        float acceleration;
        float brake;
        float turningSpeed;

        const int MIRROR_WIDTH = 4;

        ImageBrush texture, brakeTexture;

        public Player(Vector2 position, float startingSpeed, float acceleration, float brake, float turningSpeed)
            : base(position, 0, 0)
        {
            BitmapImage image = new BitmapImage(new Uri(Path.Combine("Resources", "miata_na_green.png"), UriKind.RelativeOrAbsolute));
            texture = new ImageBrush(image);
            BitmapImage brakeImage = new BitmapImage(new Uri(Path.Combine("Resources", "miata_na_green_brake.png"), UriKind.RelativeOrAbsolute));
            brakeTexture = new ImageBrush(brakeImage);
            Widht = (int)image.Width;
            Height = (int)image.Height;
            Speed = startingSpeed;
            this.acceleration = acceleration;
            this.brake = brake;
            this.turningSpeed = turningSpeed;
            Health = 1;
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }

        public void MoveRight()
        {
            if (Position.X < 438)
            {
                // update position
                Position += new Vector2(turningSpeed, 0);
                // update rect
                Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
            }
        }

        public void MoveLeft()
        {
            if (Position.X > -4)
            {
                // update position
                Position += new Vector2(-turningSpeed, 0);
                // update rect
                Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
            }
        }

        public void MoveForward()
        {
            // update position
            Position += new Vector2(0, -acceleration);
            // update rect
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
        }

        public void MoveBackward()
        {
            // update position
            Position += new Vector2(0, brake);
            // update rect
            Rect = new Rect(Position.X + MIRROR_WIDTH, Position.Y, Widht - MIRROR_WIDTH * 2, Height);
            // update texture
            Brush = brakeTexture;
        }

        public void SpeedUp(float speed)
        {
            Speed += speed;
        }

        public override void Update(float playerSpeed)
        {
            Brush = texture;
            // do NOT call base method
            if (Keyboard.IsKeyDown(Key.Right))
            {
                MoveRight();
            }
            if (Keyboard.IsKeyDown(Key.Left))
            {
                MoveLeft();
            }
            if (Keyboard.IsKeyDown(Key.Up))
            {
                MoveForward();
            }
            if (Keyboard.IsKeyDown(Key.Down))
            {
                MoveBackward();
            }
        }

        public override void Render(DrawingContext drawingContext)
        {
            if (GameLogic.DebugMode)
            {
                drawingContext.DrawRectangle(Brushes.Red, null, Rect);
            }
            drawingContext.DrawRectangle(Brush, null, new Rect(Position.X, Position.Y, Widht, Height));
        }
    }
}
