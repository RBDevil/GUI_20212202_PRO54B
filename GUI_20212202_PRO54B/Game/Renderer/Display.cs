using Game.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Renderer
{
    public class Display : FrameworkElement
    {
        IGameLogic logic;

        public void SetupLogic(IGameLogic logic)
        {
            this.logic = logic;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (logic != null)
            {
                // render all the objects on the map
                foreach (var item in logic.MapObjects)
                {
                    item.Render(drawingContext);
                }
                // render player
                logic.Player.Render(drawingContext);
            }
        }
    }
}
