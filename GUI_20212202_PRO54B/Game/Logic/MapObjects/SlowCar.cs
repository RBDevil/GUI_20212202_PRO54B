using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Logic.MapObjects
{
    class SlowCar : Car
    {
        public SlowCar(Vector2 position, int widht, int height, int speed) : base(position, 70, 110, speed)
        {
            var image = new BitmapImage(new Uri(Path.Combine("Resources", "tractor.png"), UriKind.RelativeOrAbsolute));
            texture = new ImageBrush(image);
            brakeTexture = new ImageBrush(image);
            MAX_SPEED = 1.2f;
        }
    }
}
