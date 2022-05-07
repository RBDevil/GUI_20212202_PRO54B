using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Game.Logic.MapObjects.PowerUp;

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
                int num = rnd.Next(0, 100);
                if (num > 10)
                {
                    mapObjects.Add(new Car(
                        new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100)),
                        rnd.Next(MIN_WIDHT, MAX_WIDHT),
                        rnd.Next(MIN_HEIGHT, MAX_HEIGHT)));
                }
                else if (num > 5)
                {
                    mapObjects.Add(new Coin(new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100))));
                }
                else
                {
                    int num2 = rnd.Next(0, 100);
                    if (num2 < 25)
                    {
                        mapObjects.Add(new PowerUp(new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100)), PowerUpType.BonusHealth));
                    }
                    else if (num2 < 50)
                    {
                        mapObjects.Add(new PowerUp(new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100)), PowerUpType.CoinMagnet));
                    }
                    else if (num2 < 75)
                    {
                        mapObjects.Add(new PowerUp(new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100)), PowerUpType.PointMultiplier));
                    }
                    else
                    {
                        mapObjects.Add(new PowerUp(new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100)), PowerUpType.Minigun));
                    }
                }
            }

            return mapObjects;
        }

        public static List<MapObject> GenerateBackground(List<MapObject> backgroundObjects)
        {
            List<MapObject> newBackgrounds = new List<MapObject>();

            // gets next background generation position's y value
            float yCoord = backgroundObjects.Max(x => x.Position.Y) - backgroundObjects[0].Height + 1;

            newBackgrounds.Add(new Background(new Vector2(0, yCoord)));

            return newBackgrounds;
        }
    }
}
