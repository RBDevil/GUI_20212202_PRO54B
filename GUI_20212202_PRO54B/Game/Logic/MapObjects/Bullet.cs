using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Logic.MapObjects
{
    class Bullet : MapObject, ICollidable
    {
        public int Damage { get; }
        public Rect Rect { get => new Rect(Position.X, Position.Y, Widht, Height); }

        const float BULLET_SPEED = -6;
        
        public Bullet(Vector2 position) : base(position, 3, 3)
        {
            //BitmapImage image = new BitmapImage(new Uri(Path.Combine("Resources", "lada.png"), UriKind.RelativeOrAbsolute));
            Damage = 1;
            Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Resources", "ammo.png"), UriKind.RelativeOrAbsolute)));
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
