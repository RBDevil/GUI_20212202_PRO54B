using Game.Logic.Managers;
using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic.PowerUps
{
    class PointMultiplier : PickedUpPowerUp
    {
        int points = 0;
        public void OnCollision(CollisionEventArgs eargs) 
        {
            Coin coin = eargs.CollisionWith as Coin;
            if (coin != null)
            {
                points += coin.Value;
            }
        }

        public override void Update(List<MapObject> mapObjects, Player player, ref int score)
        {
            base.Update(mapObjects, player, ref score);
            score += points;
            points = 0;
        }
    }  
}
