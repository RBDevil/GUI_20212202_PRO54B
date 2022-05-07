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
        int ammoCount;

        const int RELOAD_TIME = 10;

        public Minigun()
        {
            ammoCount = 15;
        }

        public override void Update(List<MapObject> mapObjects, List<Bullet> bullets, Player player, ref int score, int timer, List<PickedUpPowerUp> toRemove)
        {
            base.Update(mapObjects, bullets, player, ref score, timer, toRemove);
            if (Keyboard.IsKeyDown(Key.Space) && nextShotAvailableTime < timer && ammoCount > 0)
            {
                nextShotAvailableTime = timer + RELOAD_TIME;
                ammoCount--;
                bullets.Add(new Bullet(player.Position));
                if (ammoCount == 0)
                {
                    toRemove.Add(this);
                }
            }
        }
    }
}
