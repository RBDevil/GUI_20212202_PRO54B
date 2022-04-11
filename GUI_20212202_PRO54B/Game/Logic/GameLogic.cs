using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game.Logic
{
    public enum Controls
    {
        Right,
        Left
    }

    class GameLogic : IGameLogic
    {
        public Vector Position { get { return position; } }
        Vector position;

        public void Control(Controls control)
        {
            switch (control)
            {
                case Controls.Right:
                    position.X += 1;
                    break;
                case Controls.Left:
                    position.X -= 1;
                    break;
                default:
                    break;
            }
        }
    }
}
