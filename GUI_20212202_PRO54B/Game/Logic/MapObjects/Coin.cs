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
    class Coin : MapObject, ICollidable
    {
        public Rect Rect { get => new Rect(Position.X, Position.Y, Widht, Height); }

        const int RADIUS = 10;

        public Coin(Vector2 position) : base(position, RADIUS * 2, RADIUS * 2)
        {
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brush, null, new Point((int)(Position.X + RADIUS), (int)(Position.Y + RADIUS)), RADIUS, RADIUS);
        }

        public void Move(Vector2 vector)
        {
            Position += vector;
        }
    }
}
