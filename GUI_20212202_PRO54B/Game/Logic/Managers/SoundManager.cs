using Game.Logic.MapObjects;
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
        private static MediaPlayer backgroundSong = new MediaPlayer();
        private static MediaPlayer coinSoundEffect = new MediaPlayer();

        public static void PlayMusic()
        {            
            backgroundSong.Play();
        }

        public static void Init()
        {
            backgroundSong.Open(new Uri(Path.Combine("Resources", "sing-street-drive-it-like-you-stole-it.mp3"), UriKind.RelativeOrAbsolute));
            backgroundSong.Volume = 0.5;
            coinSoundEffect.Open(new Uri(Path.Combine("Resources", "coinSoundEffect.mp3"), UriKind.RelativeOrAbsolute));
            coinSoundEffect.Volume = 0.1;
        }

        public static void OnCollision(CollisionEventArgs eargs)
        {
            if (eargs.MapObject is Player)
            {
                if (eargs.CollisionWith is Coin)
                {
                    MediaPlayer clone = (MediaPlayer)coinSoundEffect.Clone();
                    clone.Position = TimeSpan.Zero;
                    clone.Play();
                }
            }
        }

        public static void StopMusic()
        {
            backgroundSong.Stop();
        }
    }
}
