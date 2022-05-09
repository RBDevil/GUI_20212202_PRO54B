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
            // check player collision
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
            // check bullet collision
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
            // check ai collision
            foreach (var item in mapObjects)
            {
                ICollidable collidableItem = item as ICollidable;
                if (collidableItem != null)
                {
                    foreach (var item2 in mapObjects)
                    {
                        ICollidable collidableItem2 = item2 as ICollidable;
                        if (collidableItem2 != null)
                        {
                            if (!item.Equals(item2))
                            {
                                if (collidableItem is Car && collidableItem2 is Car)
                                {
                                    if ((collidableItem as Car).ExtendedRect.IntersectsWith((collidableItem2 as Car).ExtendedRect))
                                    {
                                        Collision?.Invoke(new CollisionEventArgs(collidableItem, collidableItem2));
                                    }
                                }
                            }
                        }                        
                    }                  
                }
            }
        }
    }
}
