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
    public class Player : MapObject
    {
        public Player(Vector2 position, int widht, int height) 
            : base(position, widht, height)
        {
        }

        public void MoveRight()
        {
            // update position
            Position += new Vector2(1, 0);
            // update rect
            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }
        public void MoveLeft()
        {
            // update position
            Position += new Vector2(-1, 0);
            // update rect
            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }
        public override void Update()
        {

        }
    }
}
