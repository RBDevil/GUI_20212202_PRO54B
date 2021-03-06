using Game.Logic.MapObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Logic.PowerUps
{
    class CoinMagnet : PickedUpPowerUp
    {
        int range, power;
        public CoinMagnet(int range, int power, int lifeTime)
        {
            this.range = range;
            this.power = power;
            LifeTime = lifeTime;
        }

        public override void Update(List<MapObject> mapObjects, List<Bullet> bullets, Player player, ref int score, int timer, List<PickedUpPowerUp> toRemove)
        {
            base.Update(mapObjects, bullets, player, ref score, timer, toRemove);

            foreach (var item in mapObjects)
            {
                Coin coin = item as Coin;
                if (coin != null)
                {
                    Vector2 vector = player.Position - coin.Position;
                    if (vector.Length() < range)
                    {
                        float length = vector.Length();
                        Vector2 normalizedVector = new Vector2(vector.X / length, vector.Y / length);
                        coin.Move(normalizedVector * power);
                    }
                }
            }
        }

        public static CoinMagnet Copy(CoinMagnet toCopy)
        {
            return new CoinMagnet(toCopy.range, toCopy.power, toCopy.LifeTime);
        }
    }
}
