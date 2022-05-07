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
        public PointMultiplier()
        {
            LifeTime = 600;
        }

        public void OnCollision(CollisionEventArgs eargs) 
        {
            Coin coin = eargs.CollisionWith as Coin;
            if (coin != null)
            {
                points += coin.Value;
            }
        }

        public override void Update(List<MapObject> mapObjects, List<Bullet> bullets, Player player, ref int score, int timer, List<PickedUpPowerUp> toRemove)
        {
            base.Update(mapObjects, bullets, player, ref score, timer, toRemove);
            score += points;
            points = 0;
        }
    }  
}
