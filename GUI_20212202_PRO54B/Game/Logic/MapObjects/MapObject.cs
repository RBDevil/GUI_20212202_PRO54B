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
    // This is any kind of object on the map
    public abstract class MapObject
    {
        public Vector2 Position { get; protected set; }
        public int Height { get; protected set; }
        public int Widht { get; protected set; }
        public Rect Rect { get; protected set; }
        public Brush Brush { get; protected set; }

        protected MapObject(Vector2 position, int height, int widht, Brush brush)
        {
            Position = position;
            Height = height;
            Widht = widht;
            Brush = brush;

            Rect = new Rect(Position.X, Position.Y, Widht, Height);
        }
    }
}
