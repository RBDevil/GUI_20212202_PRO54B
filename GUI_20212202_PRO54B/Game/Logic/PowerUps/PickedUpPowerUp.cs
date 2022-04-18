using Game.Logic.MapObjects;
using System.Collections.Generic;
using System.Numerics;

namespace Game.Logic.PowerUps
{
    abstract class PickedUpPowerUp
    {
        public int LifeTime { get; protected set; }

        public virtual void Update(List<MapObject> mapObjects, Player player)
        {
            LifeTime--;
        }
    }
}
