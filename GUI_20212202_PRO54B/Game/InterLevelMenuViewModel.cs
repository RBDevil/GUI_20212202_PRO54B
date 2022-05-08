﻿using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Game
{
    public class InterLevelMenuViewModel : ObservableRecipient
    {
        public int Score
        {
            get { return PlayerData.Score; }
            set
            {
                SetProperty(ref PlayerData.Score, value);
                (CoinMagnetUpdateCommand as RelayCommand).NotifyCanExecuteChanged();
                (MinigunUpdateCommand as RelayCommand).NotifyCanExecuteChanged();
                (PointMultiplierUpdateCommand as RelayCommand).NotifyCanExecuteChanged();
                (CarUpdateCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public int Level 
        { 
            get { return PlayerData.Level; } 
            set { SetProperty(ref PlayerData.Level, value); }
    }
        public int CoinMagnetLevel 
        {
            get { return PlayerData.CoinMagnetLevel; }
            set 
            { 
                SetProperty(ref PlayerData.CoinMagnetLevel, value);
                (CoinMagnetUpdateCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public int MinigunLevel
        {
            get { return PlayerData.MinigunLevel; }
            set 
            { 
                SetProperty(ref PlayerData.MinigunLevel, value);
                (MinigunUpdateCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public int PointMultiplierLevel
        {
            get { return PlayerData.PointMultiplierLevel; }
            set 
            { 
                SetProperty(ref PlayerData.PointMultiplierLevel, value);
                (PointMultiplierUpdateCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }
        public int CarLevel
        {
            get { return PlayerData.CarLevel; }
            set 
            { 
                SetProperty(ref PlayerData.CarLevel, value);
                (CarUpdateCommand as RelayCommand).NotifyCanExecuteChanged();
            }
        }

        public ICommand CoinMagnetUpdateCommand { get; set; }
        public ICommand MinigunUpdateCommand { get; set; }
        public ICommand PointMultiplierUpdateCommand { get; set; }
        public ICommand CarUpdateCommand { get; set; }
        public ICommand NextLevelCommand { get; set; }

        public InterLevelMenuViewModel()
        {
            CoinMagnetUpdateCommand = new RelayCommand(() =>
                {
                    Score -= 10;
                    CoinMagnetLevel++;
                },
                () => Score >= 10 && PlayerData.CoinMagnetLevel < LevelData.CoinMagnetLevel.Length - 1);

            MinigunUpdateCommand = new RelayCommand(() =>
            {
                Score -= 10;
                MinigunLevel++;
            },
            () => Score >= 10 && PlayerData.MinigunLevel < LevelData.MinigunLevel.Length - 1);

            PointMultiplierUpdateCommand = new RelayCommand(() =>
            {
                Score -= 10;
                PointMultiplierLevel++;
            },
            () => Score >= 10 && PlayerData.PointMultiplierLevel < LevelData.PointMultiplierLevel.Length - 1);

            CarUpdateCommand = new RelayCommand(() =>
            {
                Score -= 10;
                CarLevel++;
            },
            () => Score >= 10 && PlayerData.CarLevel < LevelData.CarLevel.Length -1);
        }

    }
}
