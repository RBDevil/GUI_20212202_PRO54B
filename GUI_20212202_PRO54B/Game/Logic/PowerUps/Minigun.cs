using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game.Logic.PowerUps
{
    class Minigun : PickedUpPowerUp
    {
        int nextShotAvailableTime = 0;
        public int AmmoCount { get; private set; }

        int reloadTime;

        public Minigun(int ammoCount, int reloadTime )
        {
            this.AmmoCount = ammoCount;
            this.reloadTime = reloadTime;
        }

        public override void Update(List<MapObject> mapObjects, List<Bullet> bullets, Player player, ref int score, int timer, List<PickedUpPowerUp> toRemove)
        {
            base.Update(mapObjects, bullets, player, ref score, timer, toRemove);
            if (Keyboard.IsKeyDown(Key.Space) && nextShotAvailableTime < timer && AmmoCount > 0)
            {
                nextShotAvailableTime = timer + reloadTime;
                AmmoCount--;
                bullets.Add(new Bullet(player.Position));
                if (AmmoCount == 0)
                {
                    toRemove.Add(this);
                }
            }
        }
    
        public static Minigun Copy(Minigun toCopy)
        {
            return new Minigun(toCopy.AmmoCount, toCopy.reloadTime);
        }

        public void AddAmmo(int ammoCountAtCurrentLevel)
        {
            AmmoCount += ammoCountAtCurrentLevel;
        }
    }
}
