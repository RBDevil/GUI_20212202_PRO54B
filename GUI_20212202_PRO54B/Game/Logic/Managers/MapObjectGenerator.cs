using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Game.Logic.Managers
{
    static class MapObjectGenerator
    {
        static Random rnd = new Random();

        const int GENERATE_NUMBER = 3;
        const int MIN_WIDHT = 50;
        const int MIN_HEIGHT = 90;
        const int MAX_WIDHT = 50;
        const int MAX_HEIGHT = 90;
        const int SPREAD = 1500;

        public static List<MapObject> Generate(Size windowSize)
        {
            List<MapObject> mapObjects = new List<MapObject>();

            for (int i = 0; i < GENERATE_NUMBER; i++)
            {
                if (rnd.Next(0, 100) > 5)
                {
                    mapObjects.Add(new Car(
                        new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100)),
                        rnd.Next(MIN_WIDHT, MAX_WIDHT),
                        rnd.Next(MIN_HEIGHT, MAX_HEIGHT)));
                }
                else
                {
                    mapObjects.Add(new Coin(new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100))));
                }
            }

            return mapObjects;
        }

        public static List<MapObject> GenerateBackground(List<MapObject> backgroundObjects)
        {
            List<MapObject> newBackgrounds = new List<MapObject>();

            // gets next background generation position's y value
            float yCoord = backgroundObjects.Max(x => x.Position.Y) - backgroundObjects[0].Height;

            newBackgrounds.Add(new Background(new Vector2(0, yCoord)));

            return newBackgrounds;
        }
    }
}
