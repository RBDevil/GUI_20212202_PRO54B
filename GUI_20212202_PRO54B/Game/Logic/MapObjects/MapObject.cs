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
    // This is any kind of object on the map that has to be rendered, and checked by the collision manager
    public abstract class MapObject
    {
        public Vector2 Position { get; protected set; }
        public int Height { get; protected set; }
        public int Widht { get; protected set; }
        public Rect Rect { get; protected set; }
        public Brush Brush { get; protected set; }

        static Random rnd = new Random();

        protected MapObject(Vector2 position, int height, int widht)
        {
            Position = position;
            Height = height;
            Widht = widht;

            Brush = new SolidColorBrush(
                Color.FromRgb((byte)rnd.Next(0, 256), 
                (byte)rnd.Next(0, 256), 
                (byte)rnd.Next(0, 256)));

            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }

        public virtual void Update()
        {
            // everything except the player moves
            // update position
            Position += new Vector2(0, 1);
            // update rect
            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }
    }
}
