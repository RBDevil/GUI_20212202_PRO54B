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
    class Bullet : MapObject, ICollidable
    {
        public int Damage { get; }
        public Rect Rect { get => new Rect(Position.X, Position.Y, Widht, Height); }

        const float BULLET_SPEED = -6;
        
        public Bullet(Vector2 position) : base(position, 3, 3)
        {
            Damage = 1;
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawEllipse(Brush, null, new Point((int)(Position.X), (int)(Position.Y)), 3, 3); ;
        }

        public override void Update(float playerSpeed)
        {
            Position += new Vector2(0, BULLET_SPEED);
        }
    }
}
