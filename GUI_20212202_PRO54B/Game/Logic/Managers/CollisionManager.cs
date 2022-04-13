using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic.Managers
{
    class CollisionEventArgs
    {
        public MapObject MapObject { get; set; }
        public MapObject CollisionWith { get; set; }

        public CollisionEventArgs(MapObject mapObject, MapObject collisionWith)
        {
            MapObject = mapObject;
            CollisionWith = collisionWith;
        }
    }

    static class CollisionManager
    {
        public delegate void CollisionEventHandler(CollisionEventArgs eargs);
        public static event CollisionEventHandler Collision;

        /// <summary>
        /// This method checks for PLAYER collision ONLY
        /// </summary>
        /// <param name="mapObjects"></param>
        public static void Update(Player player, List<MapObject> mapObjects)
        {
            foreach (var item in mapObjects)
            {
                if(player.Rect.IntersectsWith(item.Rect))
                {
                    Collision?.Invoke(new CollisionEventArgs(player, item));
                }
            }
        }
    }
}
