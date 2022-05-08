using Game.Logic.PowerUps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    static class LevelData
    {
        public static readonly float[] StartingSpeeds = new []{ 4f, 4.5f, 5f };

        // range, power, lifetime
        public static readonly CoinMagnet[] CoinMagnetLevel = new CoinMagnet[]
        {
            new CoinMagnet(100, 5, 600),
            new CoinMagnet(150, 5, 600),
            new CoinMagnet(150, 7, 600),
            new CoinMagnet(150, 7, 900)
        };

        public static readonly Minigun[] MinigunLevel = new Minigun[]
        {
            new Minigun(15, 10),
            new Minigun(25, 10),
            new Minigun(25, 6)
        };

        public static readonly PointMultiplier[] PointMultiplierLevel = new PointMultiplier[]
        {
            new PointMultiplier(1, 600),
            new PointMultiplier(2, 600),
            new PointMultiplier(2, 900)
        };

        public static readonly float[][] CarLevel = new float[][]
        {
            new float[] { 2f, 2f, 3f},
            new float[] { 2.5f, 2f, 3f},
            new float[] { 2.5f, 2.5f, 3f},
            new float[] { 2.5f, 2.5f, 3.5f}
        };
    }
}
