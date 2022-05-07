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
        public ICollidable MapObject { get; set; }
        public ICollidable CollisionWith { get; set; }

        public CollisionEventArgs(ICollidable mapObject, ICollidable collisionWith)
        {
            MapObject = mapObject;
            CollisionWith = collisionWith;
        }
    }

    static class CollisionChecker
    {
        public delegate void CollisionEventHandler(CollisionEventArgs eargs);
        public static event CollisionEventHandler Collision;

        public static void Update(Player player, List<MapObject> mapObjects, List<Bullet> bullets)
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

            foreach (var bullet in bullets)
            {
                foreach (var mapObject in mapObjects)
                {
                    ICollidable collidableItem = mapObject as ICollidable;
                    if (collidableItem != null)
                    {
                        if (bullet.Rect.IntersectsWith(collidableItem.Rect)) 
                        {
                            Collision?.Invoke(new CollisionEventArgs(bullet, collidableItem));
                        }
                    }
                } 
            }
        }
    }
}
