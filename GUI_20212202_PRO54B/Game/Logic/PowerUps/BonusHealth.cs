using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic.PowerUps
{
    class BonusHealth : PickedUpPowerUp
    {
        public BonusHealth()
        {
            LifeTime = 1;
        }

        public override void Update(List<MapObject> mapObjects, List<Bullet> bullets, Player player, ref int score, int timer, List<PickedUpPowerUp> toRemove)
        {
            base.Update(mapObjects, bullets, player, ref score, timer, toRemove);
            player.Health += 1;
        }
    }
}
