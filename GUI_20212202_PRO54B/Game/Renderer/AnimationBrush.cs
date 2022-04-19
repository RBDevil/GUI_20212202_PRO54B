using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Game.Renderer
{
    class AnimationBrush
    {
        Queue<Brush> frames = new Queue<Brush>();

        public AnimationBrush(string folderName)
        {
            string folderPath = Path.Combine("Resources", "Animations", folderName);
            foreach (string imageName in Directory.GetFiles(folderPath, "*.png"))
            {
                frames.Enqueue(new ImageBrush(new BitmapImage(new Uri(
                    imageName,
                    UriKind.RelativeOrAbsolute))));
            }       
        }

        public Brush NextBrush()
        {
            frames.Enqueue(frames.Peek());
            return frames.Dequeue();
        }
    }
}
