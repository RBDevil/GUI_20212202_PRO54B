using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game.Logic.Managers
{
    static class MapObjectManager
    {
        /// <summary>
        /// Typically coins and power-ups that the player collided with and will have to be removed
        /// </summary>
        public static List<MapObject> ObjectsToRemove { get; private set; }

        // at what remaining number of objects will generate new objects
        const int GENERATION_LIMIT = 15;

        public static void Update(List<MapObject> mapObjects, List<MapObject> backgroundObjects, List<Bullet> bullets)
        {
            DeleteObjects(mapObjects, backgroundObjects, bullets);
            GenerateObjects(mapObjects, backgroundObjects);
        }

        static Size windowSize;

        public static void Init(Size windowSize)
        {
            MapObjectManager.windowSize = windowSize;
            ObjectsToRemove = new List<MapObject>();
        }

        static void GenerateObjects(List<MapObject> mapObjects, List<MapObject> backgroundObjects)
        {
            if (backgroundObjects.Count < 2)
            {
                backgroundObjects.AddRange(MapObjectGenerator.GenerateBackground(backgroundObjects));
            }

            if (mapObjects.Count < GENERATION_LIMIT)
            {
                mapObjects.AddRange(MapObjectGenerator.Generate(windowSize));
            }
        }

        /// <summary>
        /// Deletes mapObjects that are no longer needed, past or picked up by the player
        /// </summary>
        static void DeleteObjects(List<MapObject> mapObjects, List<MapObject> backgroundObjects, List<Bullet> bullets)
        {
            // collect objects to remove
            foreach (var item in mapObjects)
            {
                if (item.Position.Y > windowSize.Height)
                {
                    ObjectsToRemove.Add(item);
                }
            }

            List<MapObject> backgroundsToRemove = new List<MapObject>();

            foreach (var item in backgroundObjects)
            {
                if (item.Position.Y > windowSize.Height)
                {
                    backgroundsToRemove.Add(item);
                }
            }

            // actually remove them
            foreach (var item in ObjectsToRemove)
            {
                if (item is Bullet)
                {
                    bullets.Remove((Bullet)item);
                }
                else
                {
                    mapObjects.Remove(item);
                }
            }

            // TOOO
            // put background objects into the same list as the rest
            foreach (var item in backgroundsToRemove)
            {
                backgroundObjects.Remove(item);
            }

            ObjectsToRemove.Clear();
        }
    }
}
