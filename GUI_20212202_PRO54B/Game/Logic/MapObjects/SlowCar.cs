using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic.MapObjects
{
    class SlowCar : Car
    {
        public SlowCar(Vector2 position, int widht, int height, int speed) : base(position, widht, height, speed)
        {
            MAX_SPEED = 1.2f;
        }
    }
}
