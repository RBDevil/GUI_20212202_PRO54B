using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Logic.MapObjects
{
    public interface IRenderable
    {
        void Render(DrawingContext drawingContext);
    }

    public interface ICollidable
    {
        Rect Rect { get; }
    }
}
