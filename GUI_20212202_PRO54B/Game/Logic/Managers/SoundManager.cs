using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Game.Logic.Managers
{
    static class SoundManager
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();

        public static void PlayMusic()
        {
            mediaPlayer.Open(new Uri(Path.Combine("Resources", "sing-street-drive-it-like-you-stole-it.mp3"), UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();
        }

        public static void StopMusic()
        {
            mediaPlayer.Stop();
        }
    }
}
