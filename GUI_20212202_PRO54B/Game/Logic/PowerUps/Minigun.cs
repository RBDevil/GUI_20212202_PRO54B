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
        public override void Update(List<MapObject> mapObjects, Player player, ref int score)
        {
            base.Update(mapObjects, player, ref score);
            if (Keyboard.IsKeyDown(Key.Space))
            {
                mapObjects.Add(new Bullet(player.Position));
            }
        }
    }
}
