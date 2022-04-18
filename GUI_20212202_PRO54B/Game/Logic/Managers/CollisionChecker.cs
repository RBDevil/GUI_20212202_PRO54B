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
        public Player Player { get; set; }
        public ICollidable CollisionWith { get; set; }

        public CollisionEventArgs(Player player, ICollidable collisionWith)
        {
            Player = player;
            CollisionWith = collisionWith;
        }
    }

    static class CollisionChecker
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
                ICollidable collidableItem = item as ICollidable;
                if (collidableItem != null)
                {
                    if (player.Rect.IntersectsWith(collidableItem.Rect))
                    {
                        Collision?.Invoke(new CollisionEventArgs(player, collidableItem));
                    }
                }
            }
        }
    }
}
