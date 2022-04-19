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

        const float TURNING_SPEED = 3f;

        public Player(Vector2 position)
            : base(position, 0, 0)
        {
            BitmapImage image = new BitmapImage(new Uri(Path.Combine("Resources", "miata_na_green.png"), UriKind.RelativeOrAbsolute));
            Brush = new ImageBrush(image);
            Widht = (int)image.Width;
            Height = (int)image.Height;
            Speed = 4f;
            Health = 1;
            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }

        public void MoveRight()
        {
            // update position
            Position += new Vector2(TURNING_SPEED, 0);
            // update rect
            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }

        public void MoveLeft()
        {
            // update position
            Position += new Vector2(-TURNING_SPEED, 0);
            // update rect
            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }

        public void SpeedUp(float speed)
        {
            Speed += speed;
        }

        public override void Update(float playerSpeed)
        {
            // do NOT call base method
            if (Keyboard.IsKeyDown(Key.Right))
            {
                MoveRight();
            }
            if (Keyboard.IsKeyDown(Key.Left))
            {
                MoveLeft();
            }
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brush, null, Rect);
        }
    }
}
