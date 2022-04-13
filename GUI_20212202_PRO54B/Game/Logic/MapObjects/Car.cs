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
    class Car : MapObject
    {
        public Car(Vector2 position, int widht, int height) 
            : base(position, widht, height)
        {
        }
    }
}
