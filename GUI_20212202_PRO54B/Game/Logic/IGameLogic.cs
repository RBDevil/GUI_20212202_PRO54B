using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Logic
{
    public interface IGameLogic
    {
        List<MapObject> MapObjects { get; }
        List<MapObject> BackgroundObjects { get; }

        Player Player { get; }

        void Render(DrawingContext drawingContext);
        void Update();
        void PlayerControl(Controls control);
    }
}
