using Game.Renderer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Game.Logic
{
    static class PoliceLights
    {
        static AnimationBrush animationBrush;
        static Size windowSize;
        const int HEIGHT = 100;
        const int WIDTH = 600;

        public static void Init(Size windowSize)
        {
            PoliceLights.windowSize = windowSize;
            animationBrush = new AnimationBrush("PoliceLightsAnimation");
        }

        public static void Render(DrawingContext drawingContext)
        {
            drawingContext.DrawRectangle(
                animationBrush.NextBrush(),
                null, 
                new Rect(0, windowSize.Height - HEIGHT, WIDTH, HEIGHT));
        }
    }
}
