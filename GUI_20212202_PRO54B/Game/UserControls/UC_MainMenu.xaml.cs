﻿using Game.Logic;
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
        public UC_MainMenu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Play_Click(object sender, RoutedEventArgs e)
        {
            (Parent as Window).Content = new UC_MainGame();
        }
    }
}