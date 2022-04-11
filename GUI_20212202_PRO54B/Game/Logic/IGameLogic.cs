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
        Vector Position { get; }

        void Control(Controls control);
    }
}
