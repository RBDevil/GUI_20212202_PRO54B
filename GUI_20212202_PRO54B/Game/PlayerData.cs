﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class PlayerData : ObservableRecipient
    {
        public static int Level = 0;
        public static int Score = 0;
        public static int CoinMagnetLevel = 0;
        public static int MinigunLevel = 0;
        public static int PointMultiplierLevel = 0;
        public static int CarLevel = 0;

        public PlayerData()
        {

        }
    }
}
