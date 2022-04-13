using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game.Logic
{
    public interface IGameLogic
    {
        List<MapObject> MapObjects { get; }
        Player Player { get; }

        void PlayerControl(Controls control);
    }
}
