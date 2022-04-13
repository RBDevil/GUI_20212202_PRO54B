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
        // at what remaining number of objects will generate new objects
        const int GENERATION_LIMIT = 15;
        public static void Update(List<MapObject> mapObjects)
        {
            DeleteObjects(mapObjects);
            GenerateObjects(mapObjects);
        }

        static Size windowSize;

        public static void Init(Size windowSize)
        {
            MapObjectManager.windowSize = windowSize;
        }

        static void GenerateObjects(List<MapObject> mapObjects)
        {
            if (mapObjects.Count < GENERATION_LIMIT)
            {
                mapObjects.AddRange(MapObjectGenerator.Generate(windowSize));
            }
        }

        /// <summary>
        /// Deletes mapObjects that are no longer needed, past or picked up by the player
        /// </summary>
        static void DeleteObjects(List<MapObject> mapObjects)
        {
            // collect objects to remove
            List<MapObject> toRemove = new List<MapObject>();
            foreach (var item in mapObjects)
            {
                if (item.Position.Y > windowSize.Height - 100)
                {
                    toRemove.Add(item);
                }
            }

            // actually remove them
            foreach (var item in toRemove)
            {
                mapObjects.Remove(item);
            }
        }
    }
}
