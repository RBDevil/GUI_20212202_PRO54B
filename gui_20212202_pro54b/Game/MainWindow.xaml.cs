using Game.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameLogic logic;

        public MainWindow()
        {
            InitializeComponent();
            window.ResizeMode = ResizeMode.NoResize;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GameLogic(PlayerData.Level, PlayerData.CoinMagnetLevel, PlayerData.MinigunLevel,
                PlayerData.PointMultiplierLevel, PlayerData.CarLevel);
            display.SetupLogic(logic);

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            logic.Update();
            display.InvalidateVisual();
        }
    }
}
