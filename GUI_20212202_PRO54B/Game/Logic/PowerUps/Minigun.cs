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
        public Minigun()
        {
            LifeTime = 600;
        }

        public override void Update(List<MapObject> mapObjects, List<Bullet> bullets, Player player, ref int score)
        {
            base.Update(mapObjects, bullets, player, ref score);
            if (Keyboard.IsKeyDown(Key.Space))
            {
                bullets.Add(new Bullet(player.Position));
            }
        }
    }
}
