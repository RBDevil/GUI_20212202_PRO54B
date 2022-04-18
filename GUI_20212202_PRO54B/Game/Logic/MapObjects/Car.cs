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
    class Car : MapObject, ICollidable
    {
        public int Health { get; set; }
        public Rect Rect { get; private set; }

        public Car(Vector2 position, int widht, int height) 
            : base(position, widht, height)
        {
            Health = 5;
        }

        public override void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(Brush, null, Rect);
        }

        public override void Update(float playerSpeed)
        {
            base.Update(playerSpeed);
            // set rect after position changed to make sure it renders correctly
            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }
    }
}
