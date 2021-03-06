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
        public static readonly float[] StartingSpeeds = new []{ 4f, 5.5f, 6.5f };

        // range, power, lifetime
        public static readonly CoinMagnet[] CoinMagnetLevel = new CoinMagnet[]
        {
            new CoinMagnet(100, 8, 600),
            new CoinMagnet(150, 12, 600),
            new CoinMagnet(150, 15, 600),
            new CoinMagnet(150, 15, 900)
        };

        public static readonly Minigun[] MinigunLevel = new Minigun[]
        {
            new Minigun(10, 10),
            new Minigun(12, 7),
            new Minigun(15, 4)
        };

        public static readonly PointMultiplier[] PointMultiplierLevel = new PointMultiplier[]
        {
            new PointMultiplier(1, 600),
            new PointMultiplier(2, 600),
            new PointMultiplier(2, 900)
        };

        public static readonly float[][] CarLevel = new float[][]
        {
            new float[] { 1f, 1f, 1.5f},
            new float[] { 1.2f, 1.2f, 1.7f},
            new float[] { 1.5f, 1.5f, 2f},
            new float[] { 1.7f, 1.7f, 2.7f}
        };
    }
}
