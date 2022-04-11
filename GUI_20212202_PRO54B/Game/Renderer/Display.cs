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

        Brush carBrush = new SolidColorBrush(Color.FromRgb(100, 100, 100));

        public void SetupLogic(IGameLogic logic)
        {
            this.logic = logic;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (logic != null)
            {
                drawingContext.DrawRectangle(Brushes.Red, null, new Rect(logic.Position.X, logic.Position.Y, 10, 40));
            }
        }
    }
}
