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
    public class Player : MapObject, ICollidable
    {
        public float Speed { get; private set; }
        public Rect Rect { get; private set; }

        const float TURNING_SPEED = 2f;

        public Player(Vector2 position, int widht, int height)
            : base(position, widht, height)
        {
            Speed = 2f;
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
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brush, null, Rect);
        }
    }
}
