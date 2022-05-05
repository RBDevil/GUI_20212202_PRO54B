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

namespace Game.UserControls
{
    /// <summary>
    /// Interaction logic for UC_MainMenu.xaml
    /// </summary>
    public partial class UC_MainMenu : UserControl
    {
        GameLogic logic;
        public UC_MainMenu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            logic = new GameLogic(/*window.RenderSize*/);
            display.SetupLogic(logic);

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(1000 / 60);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            //logic.Update();
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

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new UC_MainGame();
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
