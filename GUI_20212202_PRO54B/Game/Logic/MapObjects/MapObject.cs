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
    /// <summary>
    /// This is any kind of object on the map that has to be rendered
    /// </summary>
    public abstract class MapObject : IRenderable
    {
        public Vector2 Position { get; protected set; }
        public int Height { get; protected set; }
        public int Widht { get; protected set; }
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
        }

        public virtual void Update(float playerSpeed)
        {
            // everything except the player moves
            // update position
            Position += new Vector2(0, playerSpeed);
        }
        public abstract void Render(DrawingContext drawingContext);
    }
}
