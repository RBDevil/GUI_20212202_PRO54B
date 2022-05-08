using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Renderer
{
    class AnimationBrush
    {
        Queue<ImageBrush> frames = new Queue<ImageBrush>();
        ImageBrush current;

        int frameChangeFrequency, counter;

        public AnimationBrush(string folderName, int frameChangeFrequency)
        {
            this.frameChangeFrequency = frameChangeFrequency;

            string folderPath = Path.Combine("Resources", "Animations", folderName);
            foreach (string imageName in Directory.GetFiles(folderPath, "*.png"))
            {
                frames.Enqueue(new ImageBrush(new BitmapImage(new Uri(
                    imageName,
                    UriKind.RelativeOrAbsolute))));
            }
            current = NextBrush();
        }

        ImageBrush NextBrush()
        {
            frames.Enqueue(frames.Peek());
            return frames.Dequeue();
        }

        public void Render(DrawingContext drawingContext, Vector2 position)
        {
            counter++;
            if (counter == frameChangeFrequency)
            {
                current = NextBrush();
                counter = 0;
            }

            drawingContext.DrawRectangle(
                current,
                null,
                new Rect(position.X, position.Y, current.ImageSource.Width, current.ImageSource.Height));
        }
    }
}
