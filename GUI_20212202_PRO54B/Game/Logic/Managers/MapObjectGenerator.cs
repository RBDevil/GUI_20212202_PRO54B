﻿using Game.Logic.MapObjects;
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

        const int GENERATE_NUMBER = 15;
        const int MIN_WIDHT = 30;
        const int MIN_HEIGHT = 10;
        const int MAX_WIDHT = 60;
        const int MAX_HEIGHT = 20;
        const int SPREAD = 1000;

        public static List<MapObject> Generate(Size windowSize)
        {
            List<MapObject> mapObjects = new List<MapObject>();

            for (int i = 0; i < GENERATE_NUMBER; i++)
            {
                mapObjects.Add(new Car(
                    new Vector2(rnd.Next(0, (int)windowSize.Width), rnd.Next(-100 - SPREAD, -100)), 
                    rnd.Next(MIN_WIDHT, MAX_WIDHT),
                    rnd.Next(MIN_HEIGHT, MAX_HEIGHT)));
            }

            return mapObjects;
        }
    }
}