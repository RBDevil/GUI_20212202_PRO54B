using Game.Logic;
using Game.Renderer;
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

namespace Game.UserControls
{
    /// <summary>
    /// Interaction logic for UC_MainGame.xaml
    /// </summary>
    public partial class UC_MainGame : UserControl
    {
        GameLogic logic;
        Display display;

        public UC_MainGame()
        {
            ;
            InitializeComponent();
            //Content = new UC_MainMenu();
            //MainGameWindow.ResizeMode = ResizeMode.NoResize;
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            logic.Update();
            display.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Right:
                    logic.PlayerControl(Controls.Right);
                    break;
                case Key.Left:
                    logic.PlayerControl(Controls.Left);
                    break;
            }
        }

        private void MainGameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ;
            logic = new GameLogic(/*window.RenderSize*/);
            display.SetupLogic(logic);

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            dt.Tick += Dt_Tick;
            dt.Start();
        }
    }
}
